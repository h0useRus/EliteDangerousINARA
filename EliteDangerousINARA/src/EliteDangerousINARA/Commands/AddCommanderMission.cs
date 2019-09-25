using System;
using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA.Commands
{
    /// <summary>
    /// Adds a mission to the commander's mission log.
    /// <remarks>
    /// Please note that any mission items (courier/hauling missions) and rewards are not handled by this event and aren't added/removed to/from commander's inventory.
    /// These needs to be handled separately with events like 'addCommanderInventoryCargoItem' as the respective properties in this event are purely for the mission details displaying.
    /// Handling the cargo for courier (and similar) missions isn't probably necessary, as it may be complicated sometimes (see Journal to JSON example), but it is up to you.
    /// </remarks>
    /// </summary>
    public class AddCommanderMission : Command
    {
        internal override string CommandName => "addCommanderMission";

        [JsonProperty("missionName")]
        public string MissionName { get; internal set; }

        [JsonProperty("missionGameID")]
        public string MissionId { get; internal set; }

        [JsonProperty("starsystemNameOrigin")]
        public string OriginStarSystem { get; internal set; }

        [JsonProperty("missionExpiry")]
        public DateTime? Expiry { get; set; }

        [JsonProperty("influenceGain")]
        public string InfluenceGain { get; set; }

        [JsonProperty("reputationGain")]
        public string ReputationGain { get; set; }

        [JsonProperty("stationNameOrigin")]
        public string OriginStation { get; set; }

        [JsonProperty("minorfactionNameOrigin")]
        public string OriginMinorFaction { get; set; }

        [JsonProperty("starsystemNameTarget")]
        public string TargetStarSystem { get; set; }

        [JsonProperty("stationNameTarget")]
        public string TargetStation { get; set; }

        [JsonProperty("minorfactionNameTarget")]
        public string TargetMinorFaction { get; set; }

        [JsonProperty("commodityName")]
        public string CommodityName { get; set; }

        [JsonProperty("commodityCount")]
        public int? CommodityCount { get; set; }

        [JsonProperty("targetName")]
        public string TargetName { get; set; }

        [JsonProperty("targetType")]
        public string TargetType { get; set; }

        [JsonProperty("killCount")]
        public int? KillCount { get; set; }

        [JsonProperty("passengerType")]
        public string PassengerType { get; set; }

        [JsonProperty("passengerCount")]
        public int? PassengerCount { get; set; }

        [JsonProperty("passengerIsVIP")]
        public bool? PassengerIsVIP { get; set; }

        [JsonProperty("passengerIsWanted")]
        public bool? PassengerIsWanted { get; set; }

        public AddCommanderMission(string missionName, string missionId, string originStarSystem)
        {
            if(string.IsNullOrWhiteSpace(missionName)) throw new ArgumentNullException(nameof(missionName));
            if(string.IsNullOrWhiteSpace(missionId)) throw new ArgumentNullException(nameof(missionId));
            if(string.IsNullOrWhiteSpace(originStarSystem)) throw new ArgumentNullException(nameof(originStarSystem));

            MissionName = missionName;
            MissionId = missionId;
            OriginStarSystem = originStarSystem;
        }
    }
}