using System;
using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA.Commands
{
    /// <summary>
    /// Sets the ship properties like name, ident, hull and modules value, etc.
    /// When no ship is found with the 'shipType' and 'shipGameID' set, it is automatically added to commander's fleet.
    /// The ship's configuration/loadout is handled by the separate event. The ship ID on Inara is returned as a result.
    /// <remarks>
    /// This event may handle multiple journal events or require multiple journal events. Please see Journal to JSON example for the details.
    /// </remarks>
    /// </summary>
    public class SetCommanderShip : Command
    {
        internal override string CommandName => "setCommanderShip";

        [JsonProperty("shipType")]
        public string ShipType { get; internal set; }

        [JsonProperty("shipGameID")]
        public long ShipId { get; internal set; }

        [JsonProperty("shipName")]
        public string Name { get; set; }

        [JsonProperty("shipIdent")]
        public string Identifier { get; set; }

        [JsonProperty("shipRole")]
        public string Role { get; set; }

        [JsonProperty("isCurrentShip")]
        public bool? IsCurrent { get; set; }

        [JsonProperty("isMainShip")]
        public bool? IsMain { get; set; }

        [JsonProperty("isHot")]
        public bool? IsHot { get; set; }

        [JsonProperty("shipHullValue")]
        public int? HullValue { get; set; }

        [JsonProperty("shipModulesValue")]
        public int? ModulesValue { get; set; }

        [JsonProperty("shipRebuyCost")]
        public int? RebuyCost { get; set; }

        [JsonProperty("starsystemName")]
        public string StarSystem { get; set; }

        [JsonProperty("stationName")]
        public string Station { get; set; }

        [JsonProperty("marketID")]
        public long? MarketId { get; set; }

        public SetCommanderShip(string shipType, long shipId)
        {
            if(string.IsNullOrWhiteSpace(shipType)) throw new ArgumentNullException(nameof(shipType));
            if(shipId<0) throw new ArgumentOutOfRangeException(nameof(shipId));
            ShipType = shipType;
            ShipId = shipId;
        }
    }
}