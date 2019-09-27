using System;
using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA.Commands
{
    /// <summary>
    /// Sets commander's stored modules. The storage content is always erased and set anew, so when no modules list is provided, the storage will be just emptied.
    /// </summary>
    public class SetCommanderStorageModules : Command
    {
        internal override string CommandName => "setCommanderStorageModules";

        [JsonProperty("itemName")]
        public string ItemName { get; internal set; }

        [JsonProperty("itemValue")]
        public int? ItemValue { get; set; }

        [JsonProperty("isHot")]
        public bool? IsHot { get; set; }

        [JsonProperty("starsystemName")]
        public string StarSystem { get; set; }

        [JsonProperty("stationName")]
        public string Station { get; set; }

        [JsonProperty("marketID")]
        public long? MarketId { get; set; }

        [JsonProperty("engineering")]
        public Blueprint Engineering { get; set; }

        public SetCommanderStorageModules(string itemName)
        {
            if(string.IsNullOrWhiteSpace(itemName)) throw new ArgumentNullException(nameof(itemName));
            ItemName = itemName;
        }
    }
}