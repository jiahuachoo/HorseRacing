using BetEasy.HorseRacingMarketConsole.Models;
namespace BetEasy.HorseRacingMarketConsole.FeedDataParser
{
    public interface IFeedParserFactory
    {
        IFeedDataParser Create(FeedSource source);
    }
}