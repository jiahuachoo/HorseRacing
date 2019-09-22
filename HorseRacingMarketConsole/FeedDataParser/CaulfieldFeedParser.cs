using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using BetEasy.HorseRacingMarketConsole.Models;
using System.IO;
using System.Xml.Serialization;

namespace BetEasy.HorseRacingMarketConsole.FeedDataParser
{
    public class CaulfieldFeedParser : FeedDataParserBase, ICaulfieldFeedParser
    {
        public CaulfieldFeedParser(ILogger<CaulfieldFeedParser> logger)
            : base(acceptableExtension: ".xml", logger: logger)
        {
        }

        protected override Task<List<RacingFixture>> OnParse(string feedDataFile)
        {
            return Task.Run( () => {
                
                var fixtureList = new List<RacingFixture>();
                CaulfieldFeedData feedData = null;

                using (var file = File.OpenText(feedDataFile))
                {
                    var serializer = new XmlSerializer(typeof(CaulfieldFeedData));
                    feedData = (CaulfieldFeedData)serializer.Deserialize(file);          
                }

                if(feedData != null)
                {
                    foreach(var race in feedData.Races)
                    {
                        var odds = new List<RacingOdds>();
                        foreach(var price in race.Prices)
                        {
                            foreach(var horse in race.Horses)
                            {
                                var priceHorse = price.Horses.FirstOrDefault(h => h.Number == horse.Number);
                                if(priceHorse != null)
                                {
                                    odds.Add(new RacingOdds(
                                        horseName: horse.Name,
                                        price: priceHorse.Price,
                                        oddType: price.Type
                                    ));
                                } 
                            }
                        }
                        
                        fixtureList.Add(new RacingFixture(
                            fixtureName: race.Name,
                            fixtureDate: race.StartTime,
                            odds: odds
                        ));
                    }
                }
                else
                {
                    this.logger.LogWarning("Unable to retrieve pricing because no market data was found.");
                }
                return fixtureList;
            });
        }
    }
}