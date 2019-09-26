using System;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NSW.EliteDangerous.INARA.Events
{
    public class CommanderResult
    {
        [JsonProperty("userID")]
        public int UserId { get; internal set; }

        [JsonProperty("userName")]
        public string UserName { get; internal set; }

        [JsonProperty("commanderName")]
        public string Commander { get; internal set; }

        [JsonProperty("commanderRanksPilot")]
        public CommanderRank[] Ranks { get; internal set; }

        [JsonProperty("commanderSquadron")]
        public CommanderSquadron Squadron { get; internal set; }

        [JsonProperty("preferredAllegianceName")]
        public string PreferredAllegianceName { get; internal set; }

        [JsonProperty("preferredPowerName")]
        public string PreferredPowerName { get; internal set; }

        [JsonProperty("preferredGameRole")]
        public string PreferredGameRole { get; internal set; }

        [JsonProperty("avatarImageURL")]
        public string AvatarImageUrl { get; internal set; }

        [JsonProperty("inaraURL")]
        public string InaraUrl { get; internal set; }

        [JsonProperty("otherNamesFound")]
        public string[] OtherNamesFound { get; internal set; }

        internal static EventResult<CommanderResult> Process(ResponseData<JObject> response, EliteDangerousINARA inara)
        {
            var result = new EventResult<CommanderResult>
            {
                Name = "getCommanderProfileResult",
                Status = response?.Status ?? ResponseStatus.OK,
                StatusText = response?.StatusText
            };

            if(response?.Data != null)
                try
                {
                    result.Data = response.Data.ToObject<CommanderResult>();
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

    public class CommanderRank
    {
        [JsonProperty("rankName")]
        public RankType Rank { get; internal set; }

        [JsonProperty("rankValue")]
        public byte Level { get; internal set; }

        [JsonProperty("rankProgress")]
        public double Progress { get; internal set; }
    }

    public class CommanderSquadron
    {
        [JsonProperty("SquadronID")]
        public int Id { get; internal set; }

        [JsonProperty("SquadronName")]
        public string Name { get; internal set; }

        [JsonProperty("SquadronMembersCount")]
        public int MembersCount { get; internal set; }

        [JsonProperty("SquadronMemberRank")]
        public string MemberRank { get; internal set; }

        [JsonProperty("inaraURL")]
        public string InaraUrl { get; internal set; }
    }
}