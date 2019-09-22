using System.Collections.Generic;
using System.Threading.Tasks;
using BetEasy.HorseRacingMarketConsole.Models;

namespace BetEasy.HorseRacingMarketConsole.FeedDataParser
{
    public interface IFeedDataParser
    {
        Task<List<RacingFixture>> Parse(string feedDataFile);
    }
}