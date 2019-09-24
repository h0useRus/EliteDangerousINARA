using System;
using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA.Commands
{
    /// <summary>
    /// Sets commander's reputation with the minor faction. The values can be found in journal's FSDJump and Location events, as a MyReputation property.
    /// </summary>
    public class SetCommanderReputationMinorFaction : Command
    {
        internal override string CommandName => "setCommanderReputationMinorFaction";

        [JsonProperty("minorfactionName")]
        public string Faction { get; internal set; }
        [JsonProperty("minorfactionReputation")]
        public double Reputation { get; internal set; }

        public SetCommanderReputationMinorFaction(string faction, double reputation)
        {
            if(string.IsNullOrWhiteSpace(faction)) throw new ArgumentNullException(nameof(faction));
            Faction = faction;
            Reputation = reputation;
        }
    }
}