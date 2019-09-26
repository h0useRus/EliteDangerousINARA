using System;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NSW.EliteDangerous.API;
using NSW.EliteDangerous.INARA.Commands;

namespace NSW.EliteDangerous.INARA
{
    internal partial class EliteDangerousINARA : IEliteDangerousINARA
    {
        internal HttpClient Client { get; }
        internal ILogger Log { get; }
        internal InaraOptions Options { get; }
        internal ISystemClock Clock { get; }

        public EliteDangerousINARA(IOptions<InaraOptions> options, ISystemClock clock, IEliteDangerousAPI eliteDangerousAPI, ILoggerFactory loggerFactory)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            Clock = clock ?? throw new ArgumentNullException(nameof(clock));
            Log = (loggerFactory ?? new NullLoggerFactory()).CreateLogger<EliteDangerousINARA>();
            Options = options.Value;
            if (string.IsNullOrWhiteSpace(Options.ApplicationName)) throw new ArgumentNullException(nameof(Options.ApplicationName));
            if (string.IsNullOrWhiteSpace(Options.ApplicationVersion)) throw new ArgumentNullException(nameof(Options.ApplicationVersion));
            if (string.IsNullOrWhiteSpace(Options.ApiKey)) throw new ArgumentNullException(nameof(Options.ApiKey));
            if (string.IsNullOrWhiteSpace(Options.Url)) throw new ArgumentNullException(nameof(Options.Url));
            Client = new HttpClient {BaseAddress = new Uri(Options.Url)};
            _eliteDangerousAPI = eliteDangerousAPI;
        }

        public void SetCommander(string commander, string frontierId = null)
        {
            if (string.IsNullOrWhiteSpace(commander)) throw new ArgumentNullException(nameof(commander));

            Options.Commander = commander;
            Options.FrontierId = frontierId;
        }

        public InaraRequest AddCommand<TCommand>(TCommand command) where TCommand : Command
        {
            if(command==null) throw new ArgumentNullException(nameof(command));
            return new InaraRequest(this).AddCommand(command);
        }

        public InaraRequest StartRequest() => new InaraRequest(this);

        internal InaraResponse HandleResponse(InaraResponse response)
        {
            Responses?.Invoke(this, response);
            return response;
        }

        public event EventHandler<InaraResponse> Responses;

        #region IDisposable

        private bool _disposed;

        public void Dispose()
        {
            if (!_disposed)
            {
                Client.Dispose();
            }

            _disposed = true;
        }

        #endregion
    }
}