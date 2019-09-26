using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NSW.EliteDangerous.INARA.Commands;
using NSW.EliteDangerous.INARA.Events;

namespace NSW.EliteDangerous.INARA
{
    public class InaraRequest
    {
        private readonly EliteDangerousINARA _inara;
        private readonly Dictionary<Type, List<Command>> _commands = new Dictionary<Type, List<Command>>();

        private Request _request;
        internal Request Request => _request ?? (_request = new Request
        {
            Header = new RequestHeader(_inara.Options)
        });

        internal InaraRequest(EliteDangerousINARA inara)
        {
            _inara = inara;
        }

        public InaraRequest SetCommander(string commander, string frontierId = null)
        {
            Request.Header.Commander = commander;
            Request.Header.FrontierId = frontierId;
            return this;
        }

        public InaraRequest AddCommand<TCommand>(TCommand command) where TCommand : Command
        {
            if(command==null) throw new ArgumentNullException(nameof(command));

            var type = command.GetType();

            if(!_commands.ContainsKey(type))
                _commands[type] = new List<Command>();
            _commands[type].Add(command);

            return this;
        }

        public bool Validate()
        {
            if (!string.IsNullOrWhiteSpace(Request.Header.Commander))
            {
                return true;
            }

            if (_commands.Any(command => command.Value.Any(@event => @event.RequireCommanderName)))
            {
                return false;
            }

            return true;
        }

        public async Task<InaraResponse> SendAsync()
        {
            if(!Validate())
                return _inara.HandleResponse(new InaraResponse { Status = ResponseStatus.NotValid });

            try
            {
                using (var response = await _inara.Client.PostAsync(string.Empty, new StringContent(GetJson(), Encoding.UTF8, "application/json")).ConfigureAwait(false))
                {
                    var json = await response
                        .EnsureSuccessStatusCode()
                        .Content.ReadAsStringAsync().ConfigureAwait(false);

                    return _inara.HandleResponse(ProcessResponse(Json.FromJson<Response>(json)));
                }
            }
            catch(Exception exception)
            {
                _inara.Log.LogError(exception, exception.Message);
                return _inara.HandleResponse(new InaraResponse { Status = ResponseStatus.Unprocessed, StatusText = exception.Message });
            }
        }
       
        internal string GetJson()
        {
            Compile();
            return Json.ToJson(Request);
        }

        private void Compile()
        {
            int counter = 0;
            foreach (var commandPair in _commands)
            {
                var firstCommand = commandPair.Value[0];

                Request.Events.Add(commandPair.Value.Count == 1
                    ? new RequestBody(_inara.Clock, firstCommand.CommandName, firstCommand, counter)
                    : new RequestBody(_inara.Clock, firstCommand.CommandName, commandPair.Value, counter));

                counter++;
            }
        }

        private InaraResponse ProcessResponse(Response response)
        {
            if(response?.Header == null)
                return new InaraResponse { Status = ResponseStatus.Unprocessed };

            var result = new InaraResponse
            {
                Status = response.Header.Status,
                StatusText = response.Header.StatusText,
                User = response.Header.Data
            };

            if (response.Events == null || response.Events.Length == 0)
                return result;

            foreach (var requestEvent in Request.Events)
            {
                // try match request and response
                var responseEvent = response.Events.FirstOrDefault(e => e.Id == requestEvent.Id);

                switch (requestEvent.Name)
                {
                    case "getCommanderProfile":
                        result.Events.Add(CommanderResult.Process(responseEvent, _inara));
                        break;
                    case "addCommanderShip":
                    case "setCommanderShip":
                    case "setCommanderShipTransfer":
                        result.Events.Add(ShipResult.Process(responseEvent, _inara));
                        break;
                    case "addCommanderTravelDock":
                    case "addCommanderTravelFSDJump":
                    case "setCommanderTravelLocation":
                        result.Events.Add(TravelResult.Process(responseEvent, _inara));
                        break;
                    case "getCommunityGoalsRecent":
                        break;
                    default:
                        result.Events.Add(new EventResult
                        {
                            Name = requestEvent.Name,
                            Status = responseEvent?.Status ?? ResponseStatus.OK,
                            StatusText = responseEvent?.StatusText
                        });
                        break;
                }
            }

            return result;
        }

    }
}