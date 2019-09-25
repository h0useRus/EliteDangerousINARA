using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("EliteDangerousINARA.UnitTests")]

namespace NSW.EliteDangerous.INARA
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEliteDangerousINARA(this IServiceCollection services, Action<InaraOptions> configure)
        {
            services.AddSingleton<ISystemClock, DefaultSystemClock>();
            services.Configure(configure);
            return services.AddSingleton<IEliteDangerousINARA, EliteDangerousINARA>();
        }
    }
}