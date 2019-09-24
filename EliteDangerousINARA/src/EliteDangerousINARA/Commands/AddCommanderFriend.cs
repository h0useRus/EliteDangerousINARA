using System;
using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA.Commands
{
    /// <summary>
    /// Adds a friend request to the target commander on Inara. The request may not be performed
    /// when such commander is not found under his/her in-game name.
    /// </summary>
    public class AddCommanderFriend : Command
    {
        internal override string CommandName => "addCommanderFriend";

        [JsonProperty("commanderName")]
        public string CommanderName { get; internal set; }
        [JsonProperty("gamePlatform")]
        public string GamePlatform { get; internal set; }

        public AddCommanderFriend(string commanderName, GamePlatform platform)
        {
            if(string.IsNullOrWhiteSpace(commanderName)) throw new ArgumentNullException(nameof(commanderName));
            CommanderName = commanderName;
            GamePlatform = platform.ToString().ToLower();
        }
    }
}