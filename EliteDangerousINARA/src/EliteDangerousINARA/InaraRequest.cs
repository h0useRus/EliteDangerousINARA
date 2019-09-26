using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NSW.EliteDangerous.INARA.Commands;

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

            //Request.Events.Add(new RequestBody<Command>(_inara.Clock, command));
            return this;
        }

        public InaraRequest Validate(out bool valid)
        {
            if (!string.IsNullOrWhiteSpace(Request.Header.Commander))
            {
                valid = true;
                return this;
            }

            if (_commands.Any(command => command.Value.Any(@event => @event.RequireCommanderName)))
            {
                valid = false;
                return this;
            }

            valid = true;
            return this;
        }

        public async Task SendAsync()
        {
            Validate(out var valid);
            if(!valid)
                return;

            using (var response = await _inara.Client.PostAsync(string.Empty,  new StringContent(GetJson(), Encoding.UTF8, "application/json")).ConfigureAwait(false))
            {
                var json = await response
                            .EnsureSuccessStatusCode()
                            .Content.ReadAsStringAsync().ConfigureAwait(false);
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
    }
}