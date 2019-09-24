namespace NSW.EliteDangerous.INARA.Commands
{
    /// <summary>
    /// Sets commander's in-game statistics. Please note that the statistics are always overridden as a whole,
    /// so any partial updates will cause erasing of the rest.
    /// </summary>
    public class SetCommanderGameStatistics : Command
    {
        internal override string CommandName => "setCommanderGameStatistics";
    }
}