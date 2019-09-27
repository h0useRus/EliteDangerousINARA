using System;
using System.Threading.Tasks;
using NSW.EliteDangerous.API;
using NSW.EliteDangerous.INARA;
using NSW.EliteDangerous.INARA.Commands;

namespace TestConsole
{
    public class App
    {
        private readonly IEliteDangerousAPI _api;
        private readonly IEliteDangerousINARA _inara;
        public App(IEliteDangerousAPI api, IEliteDangerousINARA inara)
        {
            _api = api;
            _inara = inara;

            _api.StatusChanged += (sender, status) => Console.WriteLine($"API status {status}");
        }
        public async Task RunAsync()
        {
            Console.WriteLine("INARA Integration started");
            Console.WriteLine($"API version: {_api.Version}");
            Console.WriteLine($"API attached: {_inara.IsApiAttached}");

            var result = await _inara.AddCommand(new GetCommanderProfile("Den McConan")).SendAsync();

            Console.ReadKey();
        }
    }
}