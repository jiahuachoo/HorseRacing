using System.Threading.Tasks;
using BetEasy.HorseRacingMarketConsole.Models;

namespace BetEasy.HorseRacingMarketConsole.FeedDataParser
{
    public interface IFeedDataParser
    {
        Task<RacingFixture> Parse(string feedDataFile);
    }
}