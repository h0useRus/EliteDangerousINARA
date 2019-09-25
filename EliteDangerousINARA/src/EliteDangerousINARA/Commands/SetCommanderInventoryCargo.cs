using System;
using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA.Commands
{
    /// <summary>
    /// Sets commander's cargo (commodities in the ship). This event will erase whole cargo and sets it anew.
    /// The limpets/drones are also counted as cargo, the passengers are not (well, except slaves ;) ).
    /// </summary>
    public class SetCommanderInventoryCargo : Command
    {
        internal override string CommandName => "setCommanderInventoryCargo";

        [JsonProperty("itemName")]
        public string ItemName { get; internal set; }

        [JsonProperty("itemCount")]
        public int ItemCount { get; internal set; }

        [JsonProperty("isStolen")]
        public bool IsStolen { get; set; }

        [JsonProperty("missionGameID")]
        public int? MissionID { get; set; }

        public SetCommanderInventoryCargo(string itemName, int itemCount)
        {
            if(string.IsNullOrWhiteSpace(itemName)) throw new ArgumentNullException(nameof(itemName));
            ItemName = itemName;
            ItemCount = itemCount;
        }
    }
}