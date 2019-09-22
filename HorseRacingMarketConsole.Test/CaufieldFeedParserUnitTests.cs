using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BetEasy.HorseRacingMarketConsole.Models;
using BetEasy.HorseRacingMarketConsole.FeedDataParser;
using System.Threading.Tasks;

namespace BetEasy.HorseRacingMarketConsole.Test
{
    [TestClass]
    public class CaulfieldFeedParserUnitTests
    {
        [DataTestMethod]
        [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method)]
        public async Task ShouldReturnExpectedData(FeedParserTestData testData)
        {
            var mockLogger = new Mock<ILogger<CaulfieldFeedParser>>();
            var sut = new CaulfieldFeedParser(mockLogger.Object);

            var result = await sut.Parse(testData.FeedDataFile);

            Assert.IsTrue(testData.ExpectedData.SequenceEqual(result), testData.TestCaseName);
        }

        public static IEnumerable<object[]> GetTestData()
        {
            yield return GetTestDataForMissingFeedFile();
            yield return GetTestDataForInvalidFeedFile();
            yield return GetTestDataForSampleFeed();
            yield return GetTestDataForMultipleOdds();
            yield return GetTestDataForMultipleRaces();
        }

        private static object[] GetTestDataForMissingFeedFile()
        {
            return new object[] { 
                new FeedParserTestData{
                    FeedDataFile = "FileNotFound.xml",
                    ExpectedData = new List<RacingFixture>(),
                    TestCaseName = "ShouldReturnExpectedData for missing feed file"
                }
            };
        }

        private static object[] GetTestDataForInvalidFeedFile()
        {
            return new object[] { 
                new FeedParserTestData{
                    FeedDataFile = "FeedData/Caulfield_Race1.json",
                    ExpectedData = new List<RacingFixture>(),
                    TestCaseName = "ShouldReturnExpectedData for invalid feed file"
                }
            };
        }

        private static object[] GetTestDataForSampleFeed()
        {
            return new object[] { 
                new FeedParserTestData{
                    FeedDataFile = "FeedData/Caulfield_Race1.xml",
                    ExpectedData = new List<RacingFixture>{
                        new RacingFixture(
                        fixtureName: "Evergreen Turf Plate",
                        fixtureDate: new DateTime(2017,12,16,11,30,0),
                        odds: new List<RacingOdds>{
                                new RacingOdds(
                                    horseName: "Advancing",
                                    price: 4.2M,
                                    oddType: "WinFixedOdds"
                                ),
                                new RacingOdds(
                                    horseName: "Coronel",
                                    price: 12M,
                                    oddType: "WinFixedOdds"
                                )
                        }
                    )},
                    TestCaseName = "ShouldReturnExpectedData for sample feed"
                }
            };
        }

         private static object[] GetTestDataForMultipleOdds()
        {
            return new object[] { 
                new FeedParserTestData{
                    FeedDataFile = "FeedData/Caulfield_Race1_multiple_odds.xml",
                    ExpectedData = new List<RacingFixture>{
                        new RacingFixture(
                        fixtureName: "Evergreen Turf Plate",
                        fixtureDate: new DateTime(2017,12,16,11,30,0),
                        odds: new List<RacingOdds>{
                                new RacingOdds(
                                    horseName: "Advancing",
                                    price: 4.2M,
                                    oddType: "WinFixedOdds"
                                ),
                                new RacingOdds(
                                    horseName: "Coronel",
                                    price: 12M,
                                    oddType: "WinFixedOdds"
                                ),
                                new RacingOdds(
                                    horseName: "Advancing",
                                    price: 1.2M,
                                    oddType: "First place"
                                ),
                                new RacingOdds(
                                    horseName: "Coronel",
                                    price: 100M,
                                    oddType: "First place"
                                )
                        }
                    )},
                    TestCaseName = "ShouldReturnExpectedData for multiple odds"
                }
            };
        }

        private static object[] GetTestDataForMultipleRaces()
        {
            return new object[] { 
                new FeedParserTestData{
                    FeedDataFile = "FeedData/Caulfield_Race1_multiple_races.xml",
                    ExpectedData = new List<RacingFixture>{
                        new RacingFixture(
                        fixtureName: "Evergreen Turf Plate",
                        fixtureDate: new DateTime(2017,12,16,11,30,0),
                        odds: new List<RacingOdds>{
                                new RacingOdds(
                                    horseName: "Advancing",
                                    price: 4.2M,
                                    oddType: "WinFixedOdds"
                                ),
                                new RacingOdds(
                                    horseName: "Coronel",
                                    price: 12M,
                                    oddType: "WinFixedOdds"
                                )
                        }),
                        new RacingFixture(
                        fixtureName: "Super Turf",
                        fixtureDate: new DateTime(2017,12,16,14,30,0),
                        odds: new List<RacingOdds>{
                                new RacingOdds(
                                    horseName: "Advancing",
                                    price: 4.2M,
                                    oddType: "WinFixedOdds"
                                ),
                                new RacingOdds(
                                    horseName: "Coronel",
                                    price: 12M,
                                    oddType: "WinFixedOdds"
                                )
                        }
                        )},
                    TestCaseName = "ShouldReturnExpectedData for multiple races"
                }
            };
        }
    }
}
