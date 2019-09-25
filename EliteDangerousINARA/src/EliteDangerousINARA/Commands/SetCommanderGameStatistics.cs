using System;

namespace NSW.EliteDangerous.INARA.Commands
{
    /// <summary>
    /// Sets commander's in-game statistics. Please note that the statistics are always overridden as a whole,
    /// so any partial updates will cause erasing of the rest.
    /// </summary>
    public class SetCommanderGameStatistics<TGameStatistics> : Command where TGameStatistics : class
    {
        internal override string CommandName => "setCommanderGameStatistics";

        public TGameStatistics Statistics { get; internal set; }

        public SetCommanderGameStatistics(TGameStatistics statistics)
        {
            Statistics = statistics ?? throw new ArgumentNullException(nameof(statistics));
        }
    }
}