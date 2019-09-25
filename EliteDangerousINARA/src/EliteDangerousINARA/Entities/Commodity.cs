using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA
{
    public class Commodity
    {
        [JsonProperty("itemName")]
        public string Name { get; set; }
        [JsonProperty("itemCount")]
        public int Count { get; set; }
    }
}