using System;
using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA.Commands
{
    /// <summary>
    /// Adds a specified count of the individual material to the commander's inventory.
    /// <remarks>
    /// Don't forget to correctly remove/add cargo commodities or materials also on journal events like MiningRefined, ScientificResearch, SearchAndRescue, Synthesis, EngineerCraft, EngineerContribution, etc.
    /// </remarks>
    /// </summary>
    public class AddCommanderInventoryMaterialsItem : Command
    {
        internal override string CommandName => "addCommanderInventoryMaterialsItem";

        [JsonProperty("itemName")]
        public string ItemName { get; internal set; }

        [JsonProperty("itemCount")]
        public int ItemCount { get; internal set; }

        public AddCommanderInventoryMaterialsItem(string itemName, int itemCount)
        {
            if(string.IsNullOrWhiteSpace(itemName)) throw new ArgumentNullException(nameof(itemName));
            ItemName = itemName;
            ItemCount = itemCount;
        }
    }
}