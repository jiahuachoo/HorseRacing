using System;
using System.Globalization;

namespace BetEasy.HorseRacingMarketConsole.Models
{
    public class RacingOdds : IEquatable<RacingOdds>
    {
        public RacingOdds(
            string horseName, 
            decimal price,
            string oddType="fixed")
        {
            this.OddType = oddType;
            this.HorseName = horseName;
            this.Price = price;
        }
        
        public string HorseName { get; private set; }

        public string OddType { get; private set; }

        public decimal Price { get; private set; }

        public bool Equals(RacingOdds other)
        {
            return 
                string.Compare(this.HorseName, other.HorseName, false, CultureInfo.InvariantCulture) == 0
                && string.Compare(this.OddType, other.OddType, false, CultureInfo.InvariantCulture) == 0
                && this.Price == other.Price;
        }
    }
}