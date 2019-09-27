using System;
using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA.Commands
{
    /// <summary>
    /// Adds 'FSDJump' event to the flight log and sets commander's current location.
    /// The starsystem ID on Inara is returned in the result, when known.
    /// </summary>
    public class AddCommanderTravelFsdJump : Command
    {
        internal override string CommandName => "addCommanderTravelFSDJump";

        [JsonProperty("starsystemName")]
        public string StarSystem { get; internal set; }

        [JsonProperty("jumpDistance")]
        public double? JumpDistance { get; set; }

        [JsonProperty("shipType")]
        public string ShipType { get; set; }

        [JsonProperty("shipGameID")]
        public long? ShipId { get; set; }

        public AddCommanderTravelFsdJump(string starSystem)
        {
            if(string.IsNullOrWhiteSpace(starSystem)) throw new ArgumentNullException(nameof(starSystem));
            StarSystem = starSystem;
        }
    }
}