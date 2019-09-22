using System.Threading.Tasks;
using BetEasy.HorseRacingMarketConsole.Models;

namespace BetEasy.HorseRacingMarketConsole
{
    public interface IConsolePrinter
    {
        Task Print(FeedSource source, string feedFile);
    }
}