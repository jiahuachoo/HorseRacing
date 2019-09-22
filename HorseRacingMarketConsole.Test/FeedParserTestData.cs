using System.Collections.Generic;
using BetEasy.HorseRacingMarketConsole.Models;

namespace BetEasy.HorseRacingMarketConsole.Test
{
    public class FeedParserTestData
    {
        public string FeedDataFile { get; set; }

        public List<RacingFixture> ExpectedData { get; set; }

        public string TestCaseName { get; set; }
    }
}