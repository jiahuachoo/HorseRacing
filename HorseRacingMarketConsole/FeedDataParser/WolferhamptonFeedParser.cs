using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using BetEasy.HorseRacingMarketConsole.Models;
using System.IO;

namespace BetEasy.HorseRacingMarketConsole.FeedDataParser
{
    public class WolferhamptonFeedParser : FeedDataParserBase, IWolferhamptonFeedParser
    {
        public WolferhamptonFeedParser(ILogger<WolferhamptonFeedParser> logger)
            : base(acceptableExtension: ".json", logger: logger)
        {
        }

        protected override Task<List<RacingFixture>> OnParse(string feedDataFile)
        {
            return Task.Run( () => {
                
                var fixtureList = new List<RacingFixture>();
                WolferhamptonFeedData feedData = null;

                using (var file = File.OpenText(feedDataFile))
                {
                    var serializer = new JsonSerializer();
                    feedData = (WolferhamptonFeedData)serializer.Deserialize(file, typeof(WolferhamptonFeedData));
                }

                if(feedData != null)
                {
                    var odds = new List<RacingOdds>();
                    foreach(var market in feedData.RawData.Markets)
                    {
                        foreach(var selection in market.Selections)
                        {
                            odds.Add(new RacingOdds(
                                        horseName: selection.Tags.name,
                                        price: selection.Price,
                                        oddType: market.Tags.type
                                    ));
                        }
                    }
                    
                    fixtureList.Add(new RacingFixture(
                                    fixtureName: feedData.RawData.FixtureName,
                                    fixtureDate: feedData.RawData.StartTime,
                                    odds: odds
                                ));
                }
 
                return fixtureList;
            });
        }
    }
}