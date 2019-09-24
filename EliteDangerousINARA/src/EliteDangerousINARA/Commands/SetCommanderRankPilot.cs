using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA.Commands
{
    /// <summary>
    /// Sets commander Elite and Navy ranks. You can set just rank or its progress or both at once.
    /// If there is a newer value already stored (compared by timestamp), the update is ignored.
    /// Possible star system permits tied to ranks are awarded to the commander automatically.
    /// </summary>
    public class SetCommanderRankPilot : Command
    {
        internal override string CommandName => "setCommanderRankPilot";

        [JsonProperty("rankName")]
        public string Name { get; internal set; }

        [JsonProperty("rankValue")]
        public byte? Value { get; internal set; }

        [JsonProperty("rankProgress")]
        public double? Progress { get; internal set; }

        public SetCommanderRankPilot(RankType rank, byte value)
        {
            Name = rank.ToString().ToLower();
            Value = value;
        }

        public SetCommanderRankPilot(RankType rank, double progress)
        {
            Name = rank.ToString().ToLower();
            Progress = progress;
        }

        public SetCommanderRankPilot(RankType rank, byte value, double progress)
        {
            Name = rank.ToString().ToLower();
            Value = value;
            Progress = progress;
        }
    }
}