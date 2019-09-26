using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA
{
    internal class Request
    {
        [JsonProperty("header")]
        public RequestHeader Header { get; set; }
        [JsonProperty("events")]
        public List<RequestBody> Events { get; set; } = new List<RequestBody>();

        public HttpContent GetContent() => new StringContent(Json.ToJson(this), Encoding.UTF8, "application/json");
    }
}