using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA
{
    public class FactionEffect
    {
        [JsonProperty("minorfactionName")]
        public string FactionName { get; set; }

        [JsonProperty("influenceGain")]
        public string InfluenceGain { get; set; }

        [JsonProperty("reputationGain")]
        public string ReputationGain { get; set; }
    }
}