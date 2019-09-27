using System;
using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA.Commands
{
    /// <summary>
    /// Adds 'Docked' event to the flight log and sets commander's current location. The starsystem ID and station ID on Inara is returned in the result, when known.
    /// <remarks>
    /// Warning: There is recommended to NOT set Dock on the session start (after the 'Location' event in the journal),
    /// as it may generate a ton of 'docked' events and mess commander's flight log. There should be a proper 'dock' event already
    /// recorded from the previous session.
    /// </remarks>
    /// </summary>
    public class AddCommanderTravelDock : Command
    {
        internal override string CommandName => "addCommanderTravelDock";

        [JsonProperty("starsystemName")]
        public string StarSystem { get; internal set; }

        [JsonProperty("stationName")]
        public string Station { get; internal set; }

        [JsonProperty("marketID")]
        public long? MarketId { get; set; }

        [JsonProperty("shipType")]
        public string ShipType { get; set; }

        [JsonProperty("shipGameID")]
        public long ShipId { get; set; }

        public AddCommanderTravelDock(string starSystem, string station)
        {
            if(string.IsNullOrWhiteSpace(starSystem)) throw new ArgumentNullException(nameof(starSystem));
            if(string.IsNullOrWhiteSpace(station)) throw new ArgumentNullException(nameof(station));
            StarSystem = starSystem;
            Station = station;
        }
    }
}