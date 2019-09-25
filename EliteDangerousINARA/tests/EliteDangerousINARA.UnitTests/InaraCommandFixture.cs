using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace NSW.EliteDangerous.INARA
{
    public class InaraCommandFixture : IDisposable
    {
        internal EliteDangerousINARA INARA { get; }

        public InaraCommandFixture()
        {
            INARA = (EliteDangerousINARA)new ServiceCollection()
                .AddLogging(cfg => cfg.AddDebug())
                .Configure<LoggerFilterOptions>(cfg => cfg.MinLevel = LogLevel.Warning)
                .AddEliteDangerousINARA(o =>
                {
                    o.ApiKey = "your_apikey_from_inara";
                    o.ApplicationName = "your_application_name";
                    o.ApplicationVersion = "1.2.3";
                    o.Commander = "Your Commander";
                    o.FrontierId = "F1234567";
                    o.IsDevelopment = true;
                })
                .AddSingleton<ISystemClock, TestSystemClock>()
                .BuildServiceProvider()
                .GetService<IEliteDangerousINARA>();
        }

        public void Dispose()
        {
        }
    }
}