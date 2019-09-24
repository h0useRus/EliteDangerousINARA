using System;
using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA.Commands
{
    /// <summary>
    /// Sets commander pledged power rank, related to Powerplay.
    /// If there is a newer value already stored (compared by timestamp), the update is ignored.v
    /// </summary>
    public class SetCommanderRankPower : Command
    {
        internal override string CommandName => "setCommanderRankPower";

        [JsonProperty("powerName")]
        public string PowerName { get; internal set; }

        [JsonProperty("rankValue")]
        public byte RankValue { get; internal set; }

        public SetCommanderRankPower(string powerName, byte rankValue)
        {
            if(string.IsNullOrWhiteSpace(powerName)) throw new ArgumentNullException(nameof(powerName));
            PowerName = powerName;
            RankValue = rankValue;
        }
    }
}