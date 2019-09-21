using BetEasy.HorseRacingMarketConsole.Models;

namespace BetEasy.HorseRacingMarketConsole.Test
{
    public class FeedParserTestData
    {
        public string FeedDataFile { get; set; }

        public RacingFixture ExpectedData { get; set; }

        public string TestCaseName { get; set; }
    }
}