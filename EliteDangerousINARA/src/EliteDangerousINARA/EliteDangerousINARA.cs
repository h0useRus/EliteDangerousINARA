using System;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace NSW.EliteDangerous.INARA
{
    internal class EliteDangerousINARA : IEliteDangerousINARA
    {
        public readonly HttpClient _client;
        public readonly InaraOptions _options;
        public readonly ILogger _log;

        public EliteDangerousINARA(IOptions<InaraOptions> options, ILoggerFactory loggerFactory)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (loggerFactory==null) throw new ArgumentNullException(nameof(loggerFactory));

            _options = options.Value;
            if (string.IsNullOrWhiteSpace(_options.ApplicationName)) throw new ArgumentNullException(nameof(_options.ApplicationName));
            if (string.IsNullOrWhiteSpace(_options.ApplicationVersion)) throw new ArgumentNullException(nameof(_options.ApplicationVersion));
            if (string.IsNullOrWhiteSpace(_options.ApiKey)) throw new ArgumentNullException(nameof(_options.ApiKey));
            if (string.IsNullOrWhiteSpace(_options.Url)) throw new ArgumentNullException(nameof(_options.Url));

            _client = new HttpClient {BaseAddress = new Uri(_options.Url)};

            _log = loggerFactory.CreateLogger<EliteDangerousINARA>();
        }

        public void SetCommander(string commander, string frontierId = null)
        {
            if (string.IsNullOrWhiteSpace(commander)) throw new ArgumentNullException(nameof(commander));

            _options.Commander = commander;
            _options.FrontierId = frontierId;
        }

        #region Implementation of IDisposable

        private bool _disposed;

        public void Dispose()
        {
            if (!_disposed)
            {
                _client.Dispose();
            }

            _disposed = true;
        }

        #endregion
    }
}