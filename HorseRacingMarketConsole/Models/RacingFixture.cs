using System;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;

namespace BetEasy.HorseRacingMarketConsole.Models
{
    public class RacingFixture : IEquatable<RacingFixture>
    {
        public RacingFixture(
            string fixtureName,
            DateTime fixtureDate,
            IEnumerable<RacingOdds> odds
        )
        { 
            this.FixtureName = fixtureName;
            this.FixtureDate = fixtureDate;
            this.FixtureOdds = odds;
        }

        public string FixtureName { get; private set; }

        public IEnumerable<RacingOdds> FixtureOdds { get; private set; }

        public DateTime FixtureDate { get; private set; }

        public bool Equals(RacingFixture other)
        {
            return 
                string.Compare(this.FixtureName, other.FixtureName, false, CultureInfo.InvariantCulture) == 0
                && this.FixtureDate == other.FixtureDate
                && this.FixtureOdds.SequenceEqual(other.FixtureOdds);
        }
    }
}