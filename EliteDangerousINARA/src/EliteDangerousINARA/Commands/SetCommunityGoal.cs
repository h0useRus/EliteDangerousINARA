using System;
using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA.Commands
{
    /// <summary>
    /// Sets community goal properties. If no goal is found, it is automatically added.
    /// <remarks>
    /// Please note that CommunityGoal event in the journals may contain multiple goals at once and those needs to be set separately.
    /// Also, the journal entry contains the player's contributions and these must be added separately in the 'setCommanderCommunityGoalProgress' event, see Journal to JSON example for details.
    /// </remarks>
    /// </summary>
    public class SetCommunityGoal : Command
    {
        internal override string CommandName => "setCommunityGoal";

        [JsonProperty("communitygoalGameID")]
        public int GoalId { get; internal set; }

        [JsonProperty("communitygoalName")]
        public string GoalName { get; internal set; }

        [JsonProperty("starsystemName")]
        public string StarSystem { get; internal set; }

        [JsonProperty("stationName")]
        public string Station { get; internal set; }

        [JsonProperty("goalExpiry")]
        public DateTime GoalExpiry { get; internal set; }

        [JsonProperty("tierReached")]
        public int? TierReached { get; set; }

        [JsonProperty("tierMax")]
        public int? TierMax { get; set; }

        [JsonProperty("topRankSize")]
        public int? TopRankSize { get; set; }

        [JsonProperty("isCompleted")]
        public bool? IsCompleted { get; set; }

        [JsonProperty("contributorsNum")]
        public int? ContributorsCount { get; set; }

        [JsonProperty("contributionsTotal")]
        public int? ContributionsTotal { get; set; }

        [JsonProperty("completionBonus")]
        public string CompletionBonus { get; set; }

        public SetCommunityGoal(int goalId, string goalName, string starSystem, string station, DateTime goalExpiry)
        {
            if(string.IsNullOrWhiteSpace(goalName)) throw new ArgumentNullException(nameof(goalName));
            if(string.IsNullOrWhiteSpace(starSystem)) throw new ArgumentNullException(nameof(starSystem));
            if(string.IsNullOrWhiteSpace(station)) throw new ArgumentNullException(nameof(station));

            GoalId = goalId;
            GoalName = goalName;
            StarSystem = starSystem;
            Station = station;
            GoalExpiry = goalExpiry;
        }
    }
}