using System;

namespace BetEasy.HorseRacingMarketConsole
{
    public class ConsoleWrapper : IConsoleWrapper
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}