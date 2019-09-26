using System;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NSW.EliteDangerous.INARA.Events
{
    public class CommunityGoalResult
    {
        [JsonProperty("communitygoalName")]
        public string Name { get; internal set; }

        [JsonProperty("starsystemName")]
        public string StarSystem { get; internal set; }

        [JsonProperty("stationName")]
        public string Station { get; internal set; }

        [JsonProperty("goalExpiry")]
        public DateTime GoalExpiry { get; internal set; }

        [JsonProperty("tierReached")]
        public int TierReached { get; internal set; }

        [JsonProperty("tierMax")]
        public int TierMax { get; internal set; }

        [JsonProperty("contributorsNum")]
        public int ContributorsCount { get; internal set; }

        [JsonProperty("contributionsTotal")]
        public int ContributionsTotal { get; internal set; }

        [JsonProperty("isCompleted")]
        public bool IsCompleted { get; internal set; }

        [JsonProperty("lastUpdate")]
        public DateTime LastUpdate { get; internal set; }

        [JsonProperty("goalObjectiveText")]
        public string ObjectiveText { get; internal set; }

        [JsonProperty("goalRewardText")]
        public string RewardText { get; internal set; }

        [JsonProperty("goalDescriptionText")]
        public string DescriptionText { get; internal set; }

        [JsonProperty("inaraURL")]
        public string InaraUrl { get; internal set; }

        internal static EventResult<CommunityGoalResult[]> Process(ResponseData<JObject> response, EliteDangerousINARA inara)
        {
            var result = new EventResult<CommunityGoalResult[]>
            {
                Name = "getCommanderProfileResult",
                Status = response?.Status ?? ResponseStatus.OK,
                StatusText = response?.StatusText
            };

            if(response?.Data != null)
                try
                {
                    result.Data = response.Data.ToObject<CommunityGoalResult[]>();
                }
                catch (Exception exception)
                {
                    inara.Log.LogError(exception, exception.Message);
                    result.Status = ResponseStatus.Error;
                    result.StatusText = exception.Message;
                }

            return result;
        }
    }
}