using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA.Commands
{
    public abstract class Command
    {
        [JsonIgnore]
        internal virtual bool RequireCommanderName => false;

        [JsonIgnore]
        internal abstract string CommandName { get; }
    }
}