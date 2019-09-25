using System;
using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA.Commands
{
    /// <summary>
    /// Sets a specified count of the individual item in the commander's cargo.
    /// If no item is present in the cargo, it is added. When count is set to zero, the item is removed.
    /// </summary>
    public class SetCommanderInventoryCargoItem : Command
    {
        internal override string CommandName => "setCommanderInventoryCargoItem";

        [JsonProperty("itemName")]
        public string ItemName { get; internal set; }

        [JsonProperty("itemCount")]
        public int ItemCount { get; internal set; }

        [JsonProperty("isStolen")]
        public bool IsStolen { get; set; }

        [JsonProperty("missionGameID")]
        public int? MissionID { get; set; }

        public SetCommanderInventoryCargoItem(string itemName, int itemCount)
        {
            if(string.IsNullOrWhiteSpace(itemName)) throw new ArgumentNullException(nameof(itemName));
            ItemName = itemName;
            ItemCount = itemCount;
        }
    }
}