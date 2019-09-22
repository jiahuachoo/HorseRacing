using CommandLine;
using BetEasy.HorseRacingMarketConsole.Models;

namespace BetEasy.HorseRacingMarketConsole
{
    public class ConsoleOptions
    {
        [Option('f', "feedfile", Required = true, HelpText = "Set the file path of the feedfile.")]
        public string FeedFile { get; set; }

        [Option('s', "source", Required = true, HelpText = "Set the feed source. Caulfield or Wolferhampton supported.")]
        public FeedSource FeedSource { get; set; }
    }
}