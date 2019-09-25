using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA.Commands
{
    /// <summary>
    /// Adds a 'death/died' record to commander's combat log.
    /// <remarks>
    /// This event won't work without a 'commanderName' in the header set.
    /// </remarks>
    /// </summary>
    public class AddCommanderCombatDeath : Command
    {
        internal override string CommandName => "addCommanderCombatDeath";
        internal override bool RequireCommanderName => true;

        [JsonProperty("starsystemName")]
        public string StarSystem { get; internal set; }

        [JsonProperty("opponentName")]
        public string OpponentName { get; set; }

        [JsonProperty("wingOpponentNames")]
        public List<string> WingOpponentNames { get; set; }

        [JsonProperty("isPlayer")]
        public bool? IsPlayer { get; set; }

        public AddCommanderCombatDeath(string starSystem)
        {
            if(string.IsNullOrWhiteSpace(starSystem)) throw new ArgumentNullException(nameof(starSystem));
            StarSystem = starSystem;
        }
    }
}