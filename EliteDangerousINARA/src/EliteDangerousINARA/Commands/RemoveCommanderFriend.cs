using System;
using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA.Commands
{
    /// <summary>
    /// Removes a target commander from the friends list on Inara. The request may not be performed
    /// when such commander is not found under his/her in-game name.
    /// </summary>
    public class RemoveCommanderFriend : Command
    {
        internal override string CommandName => "delCommanderFriend";

        [JsonProperty("commanderName")]
        public string CommanderName { get; internal set; }
        [JsonProperty("gamePlatform")]
        public string GamePlatform { get; internal set; }

        public RemoveCommanderFriend(string commanderName, GamePlatform platform)
        {
            if(string.IsNullOrWhiteSpace(commanderName)) throw new ArgumentNullException(nameof(commanderName));
            CommanderName = commanderName;
            GamePlatform = platform.ToString().ToLower();
        }
    }
}