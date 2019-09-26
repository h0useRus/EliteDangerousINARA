using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NSW.EliteDangerous.INARA
{
    internal class Response
    {
        [JsonProperty("header")]
        public ResponseData<InaraUser> Header { get; internal set; }
        [JsonProperty("events")]
        public ResponseData<JObject>[] Events { get; internal set; }
    }
}