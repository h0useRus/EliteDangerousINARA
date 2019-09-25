using System;
using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA.Commands
{
    /// <summary>
    /// Adds an 'interdiction' record to commander's combat log (when he tried to interdict someone).
    /// <remarks>
    /// This event won't work without a 'commanderName' in the header set.
    /// </remarks>
    /// </summary>
    public class AddCommanderCombatInterdiction : Command
    {
        internal override string CommandName => "addCommanderCombatInterdiction";
        internal override bool RequireCommanderName => true;

        [JsonProperty("starsystemName")]
        public string StarSystem { get; internal set; }

        [JsonProperty("opponentName")]
        public string OpponentName { get; internal set; }

        [JsonProperty("isPlayer")]
        public bool IsPlayer { get; internal set; }

        [JsonProperty("isSuccess")]
        public bool? IsSuccess { get; internal set; }

        public AddCommanderCombatInterdiction(string starSystem, string opponentName, bool isPlayer)
        {
            if(string.IsNullOrWhiteSpace(starSystem)) throw new ArgumentNullException(nameof(starSystem));
            if(string.IsNullOrWhiteSpace(opponentName)) throw new ArgumentNullException(nameof(opponentName));
            StarSystem = starSystem;
            OpponentName = opponentName;
            IsPlayer = isPlayer;
        }
    }
}