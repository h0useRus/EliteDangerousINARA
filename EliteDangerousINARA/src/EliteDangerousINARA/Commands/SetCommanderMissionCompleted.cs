using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA.Commands
{
    /// <summary>
    /// Sets the mission as completed.
    /// <remarks>
    /// Please note that any mission items (courier/hauling missions) and rewards are not handled by this event and are purely for the diplay purposes.
    /// You will eventually need to remove the mission items from the commander's cargo and also properly adjust credits, awarded commodities
    /// and permits to the commander in the separate events (setCommanderInventoryCargoItem, setCommanderCredits, etc.).
    /// </remarks>
    /// </summary>
    public class SetCommanderMissionCompleted : Command
    {
        internal override string CommandName => "setCommanderMissionCompleted";

        [JsonProperty("missionGameID")]
        public string MissionId { get; internal set; }

        [JsonProperty("donationCredits")]
        public int? DonationCredits { get; set; }

        [JsonProperty("rewardCredits")]
        public int? RewardCredits { get; set; }

        [JsonProperty("rewardCommodities")]
        public List<Commodity> RewardCommodities { get; set; }

        [JsonProperty("rewardMaterials")]
        public List<Commodity> RewardMaterials { get; set; }

        [JsonProperty("minorfactionEffects")]
        public List<FactionEffect> FactionEffects { get; set; }

        public SetCommanderMissionCompleted(string missionId)
        {
            if(string.IsNullOrWhiteSpace(missionId)) throw new ArgumentNullException(nameof(missionId));
            MissionId = missionId;
        }
    }
}