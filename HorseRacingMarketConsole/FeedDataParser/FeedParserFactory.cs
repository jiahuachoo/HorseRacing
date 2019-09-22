using Microsoft.Extensions.Logging;
using BetEasy.HorseRacingMarketConsole.Models;

namespace BetEasy.HorseRacingMarketConsole.FeedDataParser
{
    public class FeedParserFactory : IFeedParserFactory
    {
        private readonly ICaulfieldFeedParser caulfieldFeedParser;

        private readonly IWolferhamptonFeedParser wolferhamptonFeedParser;

        private readonly ILogger logger;

        public FeedParserFactory(
            ICaulfieldFeedParser caulfieldFeedParser,
            IWolferhamptonFeedParser wolferhamptonFeedParser,
            ILogger<FeedParserFactory> logger
        )
        {
            this.caulfieldFeedParser = caulfieldFeedParser;
            this.wolferhamptonFeedParser = wolferhamptonFeedParser;
            this.logger = logger;
        }
        public IFeedDataParser Create(FeedSource source)
        {
            switch(source)
            {
                case FeedSource.Caulfield:
                    return this.caulfieldFeedParser;
                
                case FeedSource.Wolferhampton:
                    return this.wolferhamptonFeedParser;
                
                default:
                    this.logger.LogError("Invalid feed source specified.");
                    return null;
            }
        }
    }
}