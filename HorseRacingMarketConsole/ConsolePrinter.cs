using System.Linq;
using System.Threading.Tasks;
using BetEasy.HorseRacingMarketConsole.Models;
using BetEasy.HorseRacingMarketConsole.FeedDataParser;

namespace BetEasy.HorseRacingMarketConsole
{
    public class ConsolePrinter : IConsolePrinter
    {
        const string seperator = "------------------------------------------------------------";
        private readonly IFeedParserFactory feedParserFactory;

        private readonly IConsoleWrapper consoleWrapper;

        public ConsolePrinter(
            IConsoleWrapper consoleWrapper,
            IFeedParserFactory feedParserFactory
        )
        {
            this.consoleWrapper = consoleWrapper;
            this.feedParserFactory = feedParserFactory;
        }

        public async Task Print(FeedSource source, string feedFile)
        {
            var feedParser = this.feedParserFactory.Create(source);
            if(feedParser != null)
            {
                var fixtures = await feedParser.Parse(feedFile);
                foreach(var fixture in fixtures)
                {
                    this.consoleWrapper.WriteLine(seperator);
                    this.consoleWrapper.WriteLine($"{fixture.FixtureName} - {fixture.FixtureDate.ToString("dd-MM-yyyy hh:mm:ss tt")}");
                    this.consoleWrapper.WriteLine(seperator);
                    this.consoleWrapper.WriteLine(string.Format("{0, -20}{1, -20}{2, -20}", "Type", "Horse", "Price"));
                    
                    var orderedOdds = fixture.FixtureOdds.OrderBy(o => o.Price);

                    foreach(var odd in orderedOdds)
                    {
                        this.consoleWrapper.WriteLine($"{odd.OddType, -20}{odd.HorseName, -20}{odd.Price, -20}");
                    }
                }

                if(!fixtures.Any())
                {
                    this.consoleWrapper.WriteLine("No race fixture found.");
                }
            }
        }
    }
}