using System;
using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA.Commands
{
    /// <summary>
    /// Adds a 'kill' record to commander's combat log (when he killed someone).
    /// Currently, it records just player/PvP kills, so ensure there are just PvP kills sent ('PVPKill' event in the journals).
    /// <remarks>
    /// This event won't work without a 'commanderName' in the header set.
    /// </remarks>
    /// </summary>
    public class AddCommanderCombatKill : Command
    {
        internal override string CommandName => "addCommanderCombatKill";
        internal override bool RequireCommanderName => true;

        [JsonProperty("starsystemName")]
        public string StarSystem { get; internal set; }

        [JsonProperty("opponentName")]
        public string OpponentName { get; internal set; }

        public AddCommanderCombatKill(string starSystem, string opponentName)
        {
            if(string.IsNullOrWhiteSpace(starSystem)) throw new ArgumentNullException(nameof(starSystem));
            if(string.IsNullOrWhiteSpace(opponentName)) throw new ArgumentNullException(nameof(opponentName));
            StarSystem = starSystem;
            OpponentName = opponentName;
        }
    }
}