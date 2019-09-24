using System.Collections.Generic;
using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA
{
    internal class Request
    {
        [JsonProperty("header")]
        public RequestHeader Header { get; set; }
        [JsonProperty("events")]
        public List<RequestBody> Events { get; set; } = new List<RequestBody>();
    }
}