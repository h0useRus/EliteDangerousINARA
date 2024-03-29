using System;
using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA.Commands
{
    /// <summary>
    /// Removes a specified count of the individual item from the commander's cargo.
    /// <remarks>
    /// Don't forget to correctly remove/add cargo commodities or materials also on journal events like MiningRefined, ScientificResearch, SearchAndRescue, Synthesis, EngineerCraft, EngineerContribution, etc.
    /// </remarks>
    /// </summary>
    public class RemoveCommanderInventoryCargoItem : Command
    {
        internal override string CommandName => "delCommanderInventoryCargoItem";

        [JsonProperty("itemName")]
        public string ItemName { get; internal set; }

        [JsonProperty("itemCount")]
        public int ItemCount { get; internal set; }

        [JsonProperty("isStolen")]
        public bool? IsStolen { get; set; }

        [JsonProperty("missionGameID")]
        public long? MissionID { get; set; }

        public RemoveCommanderInventoryCargoItem(string itemName, int itemCount)
        {
            if(string.IsNullOrWhiteSpace(itemName)) throw new ArgumentNullException(nameof(itemName));
            ItemName = itemName;
            ItemCount = itemCount;
        }
    }
}