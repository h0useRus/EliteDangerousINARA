using System;
using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA.Commands
{
    /// <summary>
    /// Returns basic information about commander from Inara like ranks, squadron, etc.
    /// </summary>
    public class GetCommanderProfile : Command
    {
        internal override string CommandName => "getCommanderProfile";

        [JsonProperty("searchName")]
        public string SearchName { get; set; }

        public GetCommanderProfile(string searchName)
        {
            if(string.IsNullOrWhiteSpace(searchName)) throw new ArgumentNullException(nameof(searchName));
            SearchName = searchName;
        }
    }
}