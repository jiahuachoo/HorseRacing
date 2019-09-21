using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace BetEasy.HorseRacingMarketConsole
{
    class Program
    {
        private static void Startup()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(configure => configure.AddConsole())                    
                    .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Information);                  
        }

        static void Main(string[] args)
        {
            Startup();
            Console.WriteLine("Hello World!");
        }
    }
}
