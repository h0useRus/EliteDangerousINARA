using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA.Commands
{
    /// <summary>
    /// Sets community goal progress for individual commander.
    /// <remarks>
    /// Note that CommunityGoal event in the journals may contain multiple goals at once and those needs to be set separately.
    /// See also 'setCommunityGoal' event which is recommended to set before this event and Journal to JSON example for details.
    /// </remarks>
    /// </summary>
    public class SetCommanderCommunityGoalProgress : Command
    {
        internal override string CommandName => "setCommanderCommunityGoalProgress";

        [JsonProperty("communitygoalGameID")]
        public int GoalId { get; internal set; }

        [JsonProperty("contribution")]
        public int Contribution { get; internal set; }

        [JsonProperty("percentileBand")]
        public int PercentileBand { get; internal set; }

        [JsonProperty("percentileBandReward")]
        public int PercentileBandReward { get; internal set; }

        [JsonProperty("isTopRank")]
        public bool? IsTopRank { get; set; }

        public SetCommanderCommunityGoalProgress(int goalId, int contribution, int percentileBand, int percentileBandReward)
        {
            GoalId = goalId;
            Contribution = contribution;
            PercentileBand = percentileBand;
            PercentileBandReward = percentileBandReward;
        }
    }
}