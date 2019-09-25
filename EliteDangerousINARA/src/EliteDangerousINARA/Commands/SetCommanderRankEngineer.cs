using System;
using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA.Commands
{
    /// <summary>
    /// Sets rank progress with the individual Engineer. If there is a newer value already stored (compared by timestamp),
    /// the update is ignored.
    /// </summary>
    public class SetCommanderRankEngineer : Command
    {
        internal override string CommandName => "setCommanderRankEngineer";

        [JsonProperty("engineerName")]
        public string EngineerName { get; internal set; }

        [JsonProperty("rankStage")]
        public string RankStage { get; internal set; }

        [JsonProperty("rankValue")]
        public int? RankValue { get; internal set; }

        public SetCommanderRankEngineer(string engineerName, string rankStage)
        {
            if(string.IsNullOrWhiteSpace(engineerName)) throw new ArgumentNullException(nameof(engineerName));
            if(string.IsNullOrWhiteSpace(rankStage)) throw new ArgumentNullException(nameof(rankStage));
            EngineerName = engineerName;
            RankStage = rankStage;
        }

        public SetCommanderRankEngineer(string engineerName, int rankValue)
        {
            if(string.IsNullOrWhiteSpace(engineerName)) throw new ArgumentNullException(nameof(engineerName));
            EngineerName = engineerName;
            RankValue = rankValue;
        }

        public SetCommanderRankEngineer(string engineerName, string rankStage, int rankValue)
        {
            if(string.IsNullOrWhiteSpace(engineerName)) throw new ArgumentNullException(nameof(engineerName));
            if(string.IsNullOrWhiteSpace(rankStage)) throw new ArgumentNullException(nameof(rankStage));
            EngineerName = engineerName;
            RankStage = rankStage;
            RankValue = rankValue;
        }
    }
}