using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA
{
    public class RewardPermits
    {
        [JsonProperty("starsystemName")]
        public string StarSystem { get; set; }
    }
}