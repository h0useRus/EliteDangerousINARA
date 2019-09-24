using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA
{
    public class InaraUser
    {
        [JsonProperty("userID")]
        public long UserId { get; internal set; }
        [JsonProperty("userName")]
        public string Name { get; internal set; }
    }
}