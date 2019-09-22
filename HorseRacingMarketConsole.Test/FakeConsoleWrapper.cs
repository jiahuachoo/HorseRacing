using System.Collections.Generic;
namespace BetEasy.HorseRacingMarketConsole.Test
{
    public class FakeConsoleWrapper : IConsoleWrapper
    {
        private List<string> outputs = new List<string>();

        public void WriteLine(string message)
        {
            this.outputs.Add(message);
        }

        public List<string> Outputs
        {
            get
            {
                return this.outputs;
            }
        }
    }
}