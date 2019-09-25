using System;
using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA.Commands
{
    /// <summary>
    /// Removes a ship from the commander's fleet. A log record is added.
    /// <remarks>
    /// This event may handle multiple journal events or require multiple journal events.
    /// Also, it doesn't handle credit changes and you should handle this separately.
    /// Please see Journal to JSON example for the details.
    /// </remarks>
    /// <remarks>
    /// Please note that the ship may not be removed when it was previously entered manually on Inara and has gameID missing (unless it was updated by 'setCommanderShip' before).
    /// It is just a measure to prevent wrong ship removal.
    /// </remarks>
    /// </summary>
    public class RemoveCommanderShip : Command
    {
        internal override string CommandName => "delCommanderShip";

        [JsonProperty("shipType")]
        public string ShipType { get; internal set; }

        [JsonProperty("shipGameID")]
        public int ShipId { get; internal set; }

        public RemoveCommanderShip(string shipType, int shipId)
        {
            if(string.IsNullOrWhiteSpace(shipType)) throw new ArgumentNullException(nameof(shipType));
            if(shipId<0) throw new ArgumentOutOfRangeException(nameof(shipId));
            ShipType = shipType;
            ShipId = shipId;
        }
    }
}