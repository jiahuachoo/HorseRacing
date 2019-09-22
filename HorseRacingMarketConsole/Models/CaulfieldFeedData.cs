using System;
using System.Xml;
using System.Xml.Serialization;

namespace BetEasy.HorseRacingMarketConsole.Models
{
    [XmlRoot("meeting")]
    public class CaulfieldFeedData
    {
        [XmlArray("races")]
        [XmlArrayItem("race")]
        public CaulfieldRace[] Races { get; set; }
    }

    public class CaulfieldRace
    {
        [XmlAttribute("name")]
        public string Name { get; set;}

        [XmlArray("horses")]
        [XmlArrayItem("horse")]
        public CaulfieldRaceHorse[] Horses { get; set; }

        [XmlArray("prices")]
        [XmlArrayItem("price")]
        public CaulfieldRacePrice[] Prices { get; set; }

        [XmlElement("start_time")]
        public string StartTimeString { get; set;}

         [XmlIgnore]
        public DateTime StartTime
        {
            get
            {
                DateTime date;
                if(DateTime.TryParse(this.StartTimeString, out date))
                {
                    return date;
                }
                return DateTime.MinValue;
            }
        }
    }

    public class CaulfieldRaceHorse
    {
        [XmlAttribute("name")]
        public string Name { get; set;}

        [XmlElement("number")]
        public int Number { get; set; }
    }

    public class CaulfieldRacePrice
    {
        [XmlArray("horses")]
        [XmlArrayItem("horse")]
        public CaulfieldRacePriceHorse[] Horses { get; set; }

        [XmlElement("priceType")]
        public string Type { get; set; }
    }

    public class CaulfieldRacePriceHorse
    {
        [XmlAttribute("number")]
        public int Number { get; set;}

        [XmlAttribute("Price")]
        public decimal Price { get; set;}
    }
}