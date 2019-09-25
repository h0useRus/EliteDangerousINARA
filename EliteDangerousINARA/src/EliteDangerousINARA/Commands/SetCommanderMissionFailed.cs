using System;
using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA.Commands
{
    /// <summary>
    /// Sets the mission as failed.
    /// <remarks>
    /// Please note that any mission items (courier/hauling missions) are not handled by this event and you will eventually need to remove the mission items from the commander's cargo and re-add them as stolen.
    /// </remarks>
    /// </summary>
    public class SetCommanderMissionFailed : Command
    {
        internal override string CommandName => "setCommanderMissionFailed";

        [JsonProperty("missionGameID")]
        public long MissionId { get; internal set; }

        public SetCommanderMissionFailed(long missionId)
        {
            MissionId = missionId;
        }
    }
}