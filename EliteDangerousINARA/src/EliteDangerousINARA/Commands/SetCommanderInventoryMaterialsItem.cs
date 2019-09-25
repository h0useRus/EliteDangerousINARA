using System;
using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA.Commands
{
    /// <summary>
    /// Sets a specified count of the individual material in the commander's inventory. If no material is present in the inventory, it is added. When count is set to zero, the material is removed.
    /// </summary>
    public class SetCommanderInventoryMaterialsItem : Command
    {
        internal override string CommandName => "setCommanderInventoryMaterialsItem";

        [JsonProperty("itemName")]
        public string ItemName { get; internal set; }

        [JsonProperty("itemCount")]
        public int ItemCount { get; internal set; }

        public SetCommanderInventoryMaterialsItem(string itemName, int itemCount)
        {
            if(string.IsNullOrWhiteSpace(itemName)) throw new ArgumentNullException(nameof(itemName));
            ItemName = itemName;
            ItemCount = itemCount;
        }
    }
}