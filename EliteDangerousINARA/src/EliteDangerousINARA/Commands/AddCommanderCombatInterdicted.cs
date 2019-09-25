using System;
using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA.Commands
{
    /// <summary>
    /// Adds an 'interdicted' record to commander's combat log (when he was interdicted and didn't managed to escape).
    /// <remarks>
    /// This event won't work without a 'commanderName' in the header set.
    /// </remarks>
    /// </summary>
    public class AddCommanderCombatInterdicted : Command
    {
        internal override string CommandName => "addCommanderCombatInterdicted";
        internal override bool RequireCommanderName => true;

        [JsonProperty("starsystemName")]
        public string StarSystem { get; internal set; }

        [JsonProperty("opponentName")]
        public string OpponentName { get; internal set; }

        [JsonProperty("isPlayer")]
        public bool IsPlayer { get; internal set; }

        [JsonProperty("isSubmit")]
        public bool? IsSubmit { get; internal set; }

        public AddCommanderCombatInterdicted(string starSystem, string opponentName, bool isPlayer)
        {
            if(string.IsNullOrWhiteSpace(starSystem)) throw new ArgumentNullException(nameof(starSystem));
            if(string.IsNullOrWhiteSpace(opponentName)) throw new ArgumentNullException(nameof(opponentName));
            StarSystem = starSystem;
            OpponentName = opponentName;
            IsPlayer = isPlayer;
        }
    }
}