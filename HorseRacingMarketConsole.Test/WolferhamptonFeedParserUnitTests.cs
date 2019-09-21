using System;
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

            if(testData.ExpectedData != null)
            {
                Assert.IsTrue(testData.ExpectedData.Equals(result), testData.TestCaseName);
            }
            else
            {
                Assert.IsNull(result, testData.TestCaseName);
            }
        }

        public static IEnumerable<object[]> GetTestData()
        {
            yield return GetTestDataForMissingFeedFile();
            yield return GetTestDataForInvalidFeedFile();
            yield return GetTestDataForSampleFeed();
        }

        private static object[] GetTestDataForMissingFeedFile()
        {
            return new object[] { 
                new FeedParserTestData{
                    FeedDataFile = "FileNotFound.json",
                    ExpectedData = null,
                    TestCaseName = "ShouldReturnExpectedData for missing feed file"
                }
            };
        }

        private static object[] GetTestDataForInvalidFeedFile()
        {
            return new object[] { 
                new FeedParserTestData{
                    FeedDataFile = "FeedData/Caulfield_Race1.xml",
                    ExpectedData = null,
                    TestCaseName = "ShouldReturnExpectedData for invalid feed file"
                }
            };
        }

        private static object[] GetTestDataForSampleFeed()
        {
            return new object[] { 
                new FeedParserTestData{
                    FeedDataFile = "FeedData/Wolferhampton_Race1.json",
                    ExpectedData = new RacingFixture(
                        fixtureName: "13:45 @ Wolverhampton",
                        fixtureDate: new DateTime(2017,12,13,13,45,0),
                        odds: new List<RacingOdds>{
                                new RacingOdds(
                                    horseName: "Toolatetodelegate",
                                    price: 10.0M
                                ),
                                new RacingOdds(
                                    horseName: "Fikhaar",
                                    price: 4.4M
                                )
                        }
                    ),
                    TestCaseName = "ShouldReturnExpectedData for sample feed"
                }
            };
        }
    }
}
