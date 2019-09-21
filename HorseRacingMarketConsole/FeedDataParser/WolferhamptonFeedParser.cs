using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using BetEasy.HorseRacingMarketConsole.Models;
using System.IO;

namespace BetEasy.HorseRacingMarketConsole.FeedDataParser
{
    public class WolferhamptonFeedParser : FeedDataParserBase
    {
        public WolferhamptonFeedParser(ILogger<WolferhamptonFeedParser> logger)
            : base(acceptableExtension: ".json", logger: logger)
        {
        }

        protected override Task<RacingFixture> OnParse(string feedDataFile)
        {
            return Task.Run( () => {
                
                RacingFixture fixture = null;
                WolferhamptonFeedData feedData = null;

                using (var file = File.OpenText(feedDataFile))
                {
                    var serializer = new JsonSerializer();
                    feedData = (WolferhamptonFeedData)serializer.Deserialize(file, typeof(WolferhamptonFeedData));
                }

                if(feedData != null)
                {
                    var odds = new List<RacingOdds>();
                    var market = feedData.RawData.Markets.FirstOrDefault();
                    if(market != null)
                    {
                        foreach(var selection in market.Selections)
                        {
                            odds.Add(new RacingOdds(
                                        horseName: selection.Tags.name,
                                        price: selection.Price
                                    ));
                        }

                        fixture = new RacingFixture(
                                    fixtureName: feedData.RawData.FixtureName,
                                    fixtureDate: feedData.RawData.StartTime,
                                    odds: odds
                                );
                    }
                    else
                    {
                        this.logger.LogWarning("Unable to retrieve pricing because no market data was found.");
                    }
                }
 
                return fixture;
            });
        }
    }
}