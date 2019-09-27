using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSW.EliteDangerous.API;
using NSW.EliteDangerous.INARA;

namespace TestConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging(cfg => cfg.AddConsole())
                .Configure<LoggerFilterOptions>(cfg => cfg.MinLevel=LogLevel.Debug)
                .AddEliteDangerousAPI(Path.Combine(Directory.GetCurrentDirectory(), "logs"))
                .AddEliteDangerousINARA(o =>
                {
                    o.ApplicationName = "NSW Inara Tests";
                    o.ApplicationVersion = "1.1.0";
                    o.ApiKey = "36fjl8iq2j0gow0880cksscogswsoooosggg4ko";
                    o.IsDevelopment = true;
                })
                .AddSingleton<App>()
                .BuildServiceProvider();

            await serviceProvider.GetService<App>().RunAsync();
        }
    }
}
