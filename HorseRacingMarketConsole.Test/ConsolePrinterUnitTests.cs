using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BetEasy.HorseRacingMarketConsole.Models;
using BetEasy.HorseRacingMarketConsole.FeedDataParser;
using System.Threading.Tasks;

namespace BetEasy.HorseRacingMarketConsole.Test
{
    [TestClass]
    public class ConsolePrinterUnitTests
    {
        [DataTestMethod]
        [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method)]
        public async Task ShouldPrintExpectedOutput(ConsolePrinterTestModel testData)
        {
            var mockFeedParser = new Mock<IFeedDataParser>();
            var mockFeedParserFactory = new Mock<IFeedParserFactory>();
            var fakeConsoleWrapper = new FakeConsoleWrapper();

            mockFeedParser
                .Setup(m => m.Parse(It.IsAny<string>()))
                .Returns(Task.FromResult(testData.StubFixtures));

            mockFeedParserFactory
                .Setup(m => m.Create(It.IsAny<FeedSource>()))
                .Returns(mockFeedParser.Object);

            var sut = new ConsolePrinter(fakeConsoleWrapper, mockFeedParserFactory.Object);

            await sut.Print(FeedSource.Caulfield, string.Empty);

            Assert.IsTrue(
                testData.ExpectedOutput.SequenceEqual(fakeConsoleWrapper.Outputs),
                testData.TestCaseName);
        }

        public static IEnumerable<object[]> GetTestData()
        {
            yield return GetTestDataForEmptyFixture();
            yield return GetTestDataForSingleOddType();
            yield return GetTestDataForDescendingOddPrice();
            yield return GetTestDataForMultipleRaces();
        }

        private static object[] GetTestDataForEmptyFixture()
        {
            return new object[] { 
                new ConsolePrinterTestModel{
                    StubFixtures = new List<RacingFixture>(),
                    ExpectedOutput = new List<string>{
                        "No race fixture found."
                    },
                    TestCaseName = "ShouldPrintExpectedOutput for no fixture"
                }
            };
        }

        private static object[] GetTestDataForSingleOddType()
        {
            return new object[] { 
                new ConsolePrinterTestModel{
                    StubFixtures = new List<RacingFixture>{
                        new RacingFixture(
                            fixtureName:"testFixture1", 
                            fixtureDate: new DateTime(2019,09,22,11,30,0), 
                            odds: new List<RacingOdds>{
                                new RacingOdds(
                                    horseName: "horse 1",
                                    price: 11.0M,
                                    oddType: "testodd"
                                ),
                                new RacingOdds(
                                    horseName: "horse 2",
                                    price: 12.0M,
                                    oddType: "testodd"
                                )
                            }
                        )
                    },
                    ExpectedOutput = new List<string>{
                        "------------------------------------------------------------",
                        "testFixture1 - 22-09-2019 11:30:00 AM",
                        "------------------------------------------------------------",
                        string.Format("{0, -20}{1, -20}{2, -20}", "Type", "Horse", "Price"),
                        string.Format("{0, -20}{1, -20}{2, -20}", "testodd", "horse 1", "11.0"),
                        string.Format("{0, -20}{1, -20}{2, -20}", "testodd", "horse 2", "12.0")
                    },
                    TestCaseName = "ShouldPrintExpectedOutput for single odd type"
                }
            };
        }

        private static object[] GetTestDataForDescendingOddPrice()
        {
            return new object[] { 
                new ConsolePrinterTestModel{
                    StubFixtures = new List<RacingFixture>{
                        new RacingFixture(
                            fixtureName:"testFixture1", 
                            fixtureDate: new DateTime(2019,09,22,11,30,0), 
                            odds: new List<RacingOdds>{
                                new RacingOdds(
                                    horseName: "horse 1",
                                    price: 21.0M,
                                    oddType: "testodd"
                                ),
                                new RacingOdds(
                                    horseName: "horse 2",
                                    price: 9.0M,
                                    oddType: "testodd"
                                )
                            }
                        )
                    },
                    ExpectedOutput = new List<string>{
                        "------------------------------------------------------------",
                        "testFixture1 - 22-09-2019 11:30:00 AM",
                        "------------------------------------------------------------",
                        string.Format("{0, -20}{1, -20}{2, -20}", "Type", "Horse", "Price"),
                        string.Format("{0, -20}{1, -20}{2, -20}", "testodd", "horse 2", "9.0"),
                        string.Format("{0, -20}{1, -20}{2, -20}", "testodd", "horse 1", "21.0")
                    },
                    TestCaseName = "ShouldPrintExpectedOutput for descending odd price"
                }
            };
        }

        private static object[] GetTestDataForMultipleRaces()
        {
            return new object[] { 
                new ConsolePrinterTestModel{
                    StubFixtures = new List<RacingFixture>{
                        new RacingFixture(
                            fixtureName:"testFixture1", 
                            fixtureDate: new DateTime(2019,09,22,11,30,0), 
                            odds: new List<RacingOdds>{
                                new RacingOdds(
                                    horseName: "horse 1",
                                    price: 21.0M,
                                    oddType: "testodd"
                                ),
                                new RacingOdds(
                                    horseName: "horse 2",
                                    price: 9.0M,
                                    oddType: "testodd"
                                )
                            }
                        ),
                        new RacingFixture(
                            fixtureName:"testFixture2", 
                            fixtureDate: new DateTime(2019,09,22,15,30,0), 
                            odds: new List<RacingOdds>{
                                new RacingOdds(
                                    horseName: "horse 1",
                                    price: 7.0M,
                                    oddType: "testodd"
                                ),
                                new RacingOdds(
                                    horseName: "horse 2",
                                    price: 1.0M,
                                    oddType: "testodd"
                                )
                            }
                        )
                    },
                    ExpectedOutput = new List<string>{
                        "------------------------------------------------------------",
                        "testFixture1 - 22-09-2019 11:30:00 AM",
                        "------------------------------------------------------------",
                        string.Format("{0, -20}{1, -20}{2, -20}", "Type", "Horse", "Price"),
                        string.Format("{0, -20}{1, -20}{2, -20}", "testodd", "horse 2", "9.0"),
                        string.Format("{0, -20}{1, -20}{2, -20}", "testodd", "horse 1", "21.0"),
                        "------------------------------------------------------------",
                        "testFixture2 - 22-09-2019 03:30:00 PM",
                        "------------------------------------------------------------",
                        string.Format("{0, -20}{1, -20}{2, -20}", "Type", "Horse", "Price"),
                        string.Format("{0, -20}{1, -20}{2, -20}", "testodd", "horse 2", "1.0"),
                        string.Format("{0, -20}{1, -20}{2, -20}", "testodd", "horse 1", "7.0")
                    },
                    TestCaseName = "ShouldPrintExpectedOutput for descending odd price"
                }
            };
        }
    }
}
