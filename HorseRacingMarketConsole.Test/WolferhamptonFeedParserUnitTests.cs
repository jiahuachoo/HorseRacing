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
    public class WolferhamptonFeedParserUnitTests
    {
        [DataTestMethod]
        [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method)]
        public async Task ShouldReturnExpectedData(FeedParserTestData testData)
        {
            var mockLogger = new Mock<ILogger<WolferhamptonFeedParser>>();
            var sut = new WolferhamptonFeedParser(mockLogger.Object);

            var result = await sut.Parse(testData.FeedDataFile);

            Assert.IsTrue(testData.ExpectedData.SequenceEqual(result), testData.TestCaseName);
        }

        public static IEnumerable<object[]> GetTestData()
        {
            yield return GetTestDataForMissingFeedFile();
            yield return GetTestDataForInvalidFeedFile();
            yield return GetTestDataForSampleFeed();
            yield return GetTestDataForMultipleOdds();
        }

        private static object[] GetTestDataForMissingFeedFile()
        {
            return new object[] { 
                new FeedParserTestData{
                    FeedDataFile = "FileNotFound.json",
                    ExpectedData = new List<RacingFixture>(),
                    TestCaseName = "ShouldReturnExpectedData for missing feed file"
                }
            };
        }

        private static object[] GetTestDataForInvalidFeedFile()
        {
            return new object[] { 
                new FeedParserTestData{
                    FeedDataFile = "FeedData/Caulfield_Race1.xml",
                    ExpectedData = new List<RacingFixture>(),
                    TestCaseName = "ShouldReturnExpectedData for invalid feed file"
                }
            };
        }

        private static object[] GetTestDataForSampleFeed()
        {
            return new object[] { 
                new FeedParserTestData{
                    FeedDataFile = "FeedData/Wolferhampton_Race1.json",
                    ExpectedData = new List<RacingFixture>{
                        new RacingFixture(
                        fixtureName: "13:45 @ Wolverhampton",
                        fixtureDate: new DateTime(2017,12,13,13,45,0),
                        odds: new List<RacingOdds>{
                                new RacingOdds(
                                    horseName: "Toolatetodelegate",
                                    price: 10.0M,
                                    oddType: "winner"
                                ),
                                new RacingOdds(
                                    horseName: "Fikhaar",
                                    price: 4.4M,
                                    oddType: "winner"
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
                    FeedDataFile = "FeedData/Wolferhampton_Race1_multiple_odds.json",
                    ExpectedData = new List<RacingFixture>{
                        new RacingFixture(
                        fixtureName: "13:45 @ Wolverhampton",
                        fixtureDate: new DateTime(2017,12,13,13,45,0),
                        odds: new List<RacingOdds>{
                                new RacingOdds(
                                    horseName: "Toolatetodelegate",
                                    price: 10.0M,
                                    oddType: "winner"
                                ),
                                new RacingOdds(
                                    horseName: "Fikhaar",
                                    price: 4.4M,
                                    oddType: "winner"
                                ),
                                new RacingOdds(
                                    horseName: "Toolatetodelegate",
                                    price: 11.0M,
                                    oddType: "First place"
                                ),
                                new RacingOdds(
                                    horseName: "Fikhaar",
                                    price: 3.4M,
                                    oddType: "First place"
                                )
                        }
                    )},
                    TestCaseName = "ShouldReturnExpectedData for multiple odds"
                }
            };
        }
    }
}
