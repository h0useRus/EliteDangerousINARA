using System;
using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA.Commands
{
    public class RemoveCommanderInventoryMaterialsItem : Command
    {
        internal override string CommandName => "delCommanderInventoryMaterialsItem";

        [JsonProperty("itemName")]
        public string ItemName { get; internal set; }

        [JsonProperty("itemCount")]
        public int ItemCount { get; internal set; }

        public RemoveCommanderInventoryMaterialsItem(string itemName, int itemCount)
        {
            if(string.IsNullOrWhiteSpace(itemName)) throw new ArgumentNullException(nameof(itemName));
            ItemName = itemName;
            ItemCount = itemCount;
        }
    }
}