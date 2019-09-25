using System;
using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA.Commands
{
    /// <summary>
    /// Sets the ship loadout/configuration, including modules modifications. When no shipLoadout is set, it just resets the ship configuration.
    /// </summary>
    public class SetCommanderShipLoadout<TLoadout> : Command where TLoadout : class
    {
        internal override string CommandName => "setCommanderShipLoadout";

        [JsonProperty("shipType")]
        public string ShipType { get; internal set; }

        [JsonProperty("shipGameID")]
        public int ShipId { get; internal set; }

        public TLoadout Loadout { get; internal set; }

        public SetCommanderShipLoadout(string shipType, int shipId, TLoadout loadout)
        {
            if(string.IsNullOrWhiteSpace(shipType)) throw new ArgumentNullException(nameof(shipType));
            if(shipId<0) throw new ArgumentOutOfRangeException(nameof(shipId));
            ShipType = shipType;
            ShipId = shipId;
            Loadout = loadout ?? throw new ArgumentNullException(nameof(loadout));
        }
    }
}