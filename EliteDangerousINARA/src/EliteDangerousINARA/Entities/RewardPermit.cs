using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA
{
    public class RewardPermit
    {
        [JsonProperty("starsystemName")]
        public string StarSystem { get; set; }
    }
}