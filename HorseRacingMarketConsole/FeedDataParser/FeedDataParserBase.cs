using System.IO;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.Extensions.Logging;
using BetEasy.HorseRacingMarketConsole.Models;
using System;

namespace BetEasy.HorseRacingMarketConsole.FeedDataParser
{
    public abstract class FeedDataParserBase : IFeedDataParser
    {
        private readonly string acceptableExtension;

        protected readonly ILogger logger;
        
        public FeedDataParserBase(
            string acceptableExtension,
            ILogger logger
        )
        {
            this.acceptableExtension = acceptableExtension;
            this.logger = logger;
        }

        public async Task<RacingFixture> Parse(string feedDataFile)
        {

            if(!File.Exists(feedDataFile))
            {
                this.logger.LogError($"Unable to parse {feedDataFile} because it was not found.");
                return null;
            }

            if(string.Compare(
                Path.GetExtension(feedDataFile), 
                this.acceptableExtension, 
                true, 
                CultureInfo.InvariantCulture) != 0)
            {
                this.logger.LogError($"Unable to parse {feedDataFile} because it was in an unexpected format.");
                return null;
            }

            try
            {
                return await this.OnParse(feedDataFile);
            }
            catch(Exception e)
            {
                this.logger.LogError(e, $"Unable to parse {feedDataFile}");
            }
            return null;
        }

        protected abstract Task<RacingFixture> OnParse(string feedDataFile);
    }
}