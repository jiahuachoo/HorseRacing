using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using CommandLine;
using BetEasy.HorseRacingMarketConsole.FeedDataParser;

namespace BetEasy.HorseRacingMarketConsole
{
    class Program
    {
        private static ServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(configure => configure.AddConsole())                    
                    .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Information); 
            
            serviceCollection.AddTransient<ICaulfieldFeedParser, CaulfieldFeedParser>();
            serviceCollection.AddTransient<IWolferhamptonFeedParser, WolferhamptonFeedParser>();
            serviceCollection.AddTransient<IFeedParserFactory, FeedParserFactory>();
            serviceCollection.AddTransient<IConsoleWrapper, ConsoleWrapper>();
            serviceCollection.AddTransient<IConsolePrinter, ConsolePrinter>();

            return serviceCollection.BuildServiceProvider();
        }

        static async Task Main(string[] args)
        {
            var serviceProvider = ConfigureServices();
            var printer = serviceProvider.GetService<IConsolePrinter>();

            var result = Parser.Default.ParseArguments<ConsoleOptions>(args);
            await result.MapResult(async 
                opt =>
                    {                       
                        await printer.Print(opt.FeedSource, opt.FeedFile);
                    },
                errors => Task.FromResult(0)
            );
               
        }
    }
}
