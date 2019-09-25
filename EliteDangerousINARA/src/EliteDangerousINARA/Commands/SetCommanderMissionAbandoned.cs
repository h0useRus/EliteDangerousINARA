using System;
using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA.Commands
{
    /// <summary>
    /// Sets the mission as abandoned.
    /// <remarks>
    /// Please note that any mission items (courier/hauling missions) are not handled by this event
    /// and you will eventually need to remove the mission items from the commander's cargo and re-add them as stolen.
    /// </remarks>
    /// </summary>
    public class SetCommanderMissionAbandoned : Command
    {
        internal override string CommandName => "setCommanderMissionAbandoned";

        [JsonProperty("missionGameID")]
        public string MissionId { get; internal set; }

        public SetCommanderMissionAbandoned(string missionId)
        {
            if(string.IsNullOrWhiteSpace(missionId)) throw new ArgumentNullException(nameof(missionId));
            MissionId = missionId;
        }
    }
}