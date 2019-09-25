using System;
using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA.Commands
{
    public class SetCommanderTravelLocation : Command
    {
        internal override string CommandName => "setCommanderTravelLocation";

        [JsonProperty("starsystemName")]
        public string StarSystem { get; internal set; }

        [JsonProperty("stationName")]
        public string Station { get; set; }

        [JsonProperty("marketID")]
        public long? MarketId { get; set; }

        public SetCommanderTravelLocation(string starSystem)
        {
            if(string.IsNullOrWhiteSpace(starSystem)) throw new ArgumentNullException(nameof(starSystem));
            StarSystem = starSystem;
        }
    }
}