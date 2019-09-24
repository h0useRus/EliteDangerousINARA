using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA.Commands
{
    /// <summary>
    /// Sets commander's reputation with the major factions like Federation, Empire, etc.
    /// </summary>
    public class SetCommanderReputationMajorFaction : Command
    {
        internal override string CommandName => "setCommanderReputationMajorFaction";

        [JsonProperty("majorfactionName")]
        public string Faction { get; internal set; }
        [JsonProperty("majorfactionReputation")]
        public double Reputation { get; internal set; }

        public SetCommanderReputationMajorFaction(MajorFaction faction, double reputation)
        {
            Faction = faction.ToString().ToLower();
            Reputation = reputation;
        }
    }
}