using System;
using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA.Commands
{
    /// <summary>
    /// Adds a specified count of the individual item to the commander's cargo.
    /// <remarks>
    /// </remarks>
    /// Don't forget to correctly remove/add cargo commodities or materials also on journal events like MiningRefined, ScientificResearch, SearchAndRescue, Synthesis, EngineerCraft, EngineerContribution, etc.
    /// </summary>
    public class AddCommanderInventoryCargoItem : Command
    {
        internal override string CommandName => "addCommanderInventoryCargoItem";

        [JsonProperty("itemName")]
        public string ItemName { get; internal set; }

        [JsonProperty("itemCount")]
        public int ItemCount { get; internal set; }

        [JsonProperty("isStolen")]
        public bool IsStolen { get; set; }

        [JsonProperty("missionGameID")]
        public int? MissionID { get; set; }

        public AddCommanderInventoryCargoItem(string itemName, int itemCount)
        {
            if(string.IsNullOrWhiteSpace(itemName)) throw new ArgumentNullException(nameof(itemName));
            ItemName = itemName;
            ItemCount = itemCount;
        }
    }
}