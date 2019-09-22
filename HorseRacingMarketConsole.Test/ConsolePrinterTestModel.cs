using System.Collections.Generic;
using BetEasy.HorseRacingMarketConsole.Models;

namespace BetEasy.HorseRacingMarketConsole.Test
{
    public class ConsolePrinterTestModel
    {
        public List<RacingFixture> StubFixtures { get; set; }

        public List<string> ExpectedOutput { get; set;}
        
        public string TestCaseName { get; set; }
    }
}