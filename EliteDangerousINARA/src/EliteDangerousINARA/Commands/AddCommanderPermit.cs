using System;
using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA.Commands
{
    /// <summary>
    /// Adds star system permit for the commander. You do not need to handle permits granted for the Pilots Federation or Navy rank promotion,
    /// but you should handle any other ways (like mission rewards).
    /// <remarks>
    /// Warning: The permits list is not implemented on Inara yet (will be available fairly soon), but you can handle this in advance.
    /// </remarks>
    /// </summary>
    public class AddCommanderPermit : Command
    {
        internal override string CommandName => "addCommanderPermit";

        [JsonProperty("starsystemName")]
        public string StarSystem { get; internal set; }

        public AddCommanderPermit(string starSystem)
        {
            if(string.IsNullOrWhiteSpace(starSystem)) throw new ArgumentNullException(nameof(starSystem));
            StarSystem = starSystem;
        }
    }
}