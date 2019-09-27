using System;
using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA.Commands
{
    /// <summary>
    /// Sets the ship's dock or transfer location. Set this just when the ship is not flown (the ship's dock location is not used otherwise).
    /// The ship Inara ID is returned.
    /// <remarks>
    /// This event may handle multiple journal events or require multiple journal events. Please see Journal to JSON example for the details.
    /// </remarks>
    /// </summary>
    public class SetCommanderShipTransfer : Command
    {
        internal override string CommandName => "setCommanderShipTransfer";

        [JsonProperty("shipType")]
        public string ShipType { get; internal set; }

        [JsonProperty("shipGameID")]
        public long ShipId { get; internal set; }

        [JsonProperty("starsystemName")]
        public string StarSystem { get; internal set; }

        [JsonProperty("stationName")]
        public string Station { get; internal set; }

        [JsonProperty("marketID")]
        public long? MarketId { get; set; }

        [JsonProperty("transferTime")]
        public long? TransferTime { get; set; }

        public SetCommanderShipTransfer(string shipType, long shipId, string starSystem, string station)
        {
            if(string.IsNullOrWhiteSpace(shipType)) throw new ArgumentNullException(nameof(shipType));
            if(string.IsNullOrWhiteSpace(starSystem)) throw new ArgumentNullException(nameof(starSystem));
            if(string.IsNullOrWhiteSpace(station)) throw new ArgumentNullException(nameof(station));
            if(shipId<0) throw new ArgumentOutOfRangeException(nameof(shipId));
            ShipType = shipType;
            ShipId = shipId;
            StarSystem = starSystem;
            Station = station;
        }
    }
}