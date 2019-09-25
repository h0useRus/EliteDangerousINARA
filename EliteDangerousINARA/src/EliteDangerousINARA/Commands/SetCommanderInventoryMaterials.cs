using System;
using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA.Commands
{
    /// <summary>
    /// Sets commander's materials (components for crafting). This event will erase whole materials inventory and sets it anew. 
    /// </summary>
    public class SetCommanderInventoryMaterials : Command
    {
        internal override string CommandName => "setCommanderInventoryMaterials";

        [JsonProperty("itemName")]
        public string ItemName { get; internal set; }

        [JsonProperty("itemCount")]
        public int ItemCount { get; internal set; }

        public SetCommanderInventoryMaterials(string itemName, int itemCount)
        {
            if(string.IsNullOrWhiteSpace(itemName)) throw new ArgumentNullException(nameof(itemName));
            ItemName = itemName;
            ItemCount = itemCount;
        }
    }
}