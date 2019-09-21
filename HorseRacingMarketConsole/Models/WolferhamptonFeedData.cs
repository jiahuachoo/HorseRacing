using System;
using System.Collections.Generic;

namespace BetEasy.HorseRacingMarketConsole.Models
{
    public class WolferhamptonFeedData
    {
        public DateTime Timestamp { get; set; }

        public WolferhamptonRawData RawData { get; set; }
    }

    public class WolferhamptonRawData
    {
        public WolferhamptonRawData()
        {
            this.Markets = new List<WolferhamptonRawMarket>();
        }
        
        public string FixtureName { get; set; }

        public DateTime StartTime { get; set; }

        public List<WolferhamptonRawMarket> Markets { get; set; }
    }

    public class WolferhamptonRawMarket
    {
        public WolferhamptonRawMarket()
        {
            this.Selections = new List<WolferhamptonRawMarketSelection>();
        }
        public List<WolferhamptonRawMarketSelection> Selections { get; set; }
    }

    public class WolferhamptonRawMarketSelection
    {
        public decimal Price { get; set; }

        public WolferhamptonRawMarketSelectionTag Tags { get; set; }
    }

    public class WolferhamptonRawMarketSelectionTag
    {
        public string name { get; set; }
    }
}