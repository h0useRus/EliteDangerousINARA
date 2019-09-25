using System;
using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA.Commands
{
    /// <summary>
    /// Adds a ship to the commander's fleet. A log record is added, the new ship bought is automatically set as current. The ship ID on Inara is returned.
    /// <remarks>
    /// This event may handle multiple journal events or require multiple journal events.
    /// Also, it doesn't handle old ship selling or storing and credit changes, you should handle this separately.
    /// Please see Journal to JSON example for the details.
    /// </remarks>
    /// </summary>
    public class AddCommanderShip : Command
    {
        internal override string CommandName => "addCommanderShip";

        [JsonProperty("shipType")]
        public string ShipType { get; internal set; }

        [JsonProperty("shipGameID")]
        public int ShipId { get; internal set; }

        public AddCommanderShip(string shipType, int shipId)
        {
            if(string.IsNullOrWhiteSpace(shipType)) throw new ArgumentNullException(nameof(shipType));
            if(shipId<0) throw new ArgumentOutOfRangeException(nameof(shipId));
            ShipType = shipType;
            ShipId = shipId;
        }
    }
}