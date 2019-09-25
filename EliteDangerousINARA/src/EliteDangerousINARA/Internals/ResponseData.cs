using System.Net;
using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA
{
    internal class ResponseData
    {
        [JsonProperty("eventStatus")]
        public HttpStatusCode Status { get; internal set; }
        [JsonProperty("eventStatusText")]
        public string Message { get; internal set; }
    }
    internal class ResponseData<T> : ResponseData
    {
        [JsonProperty("eventData")]
        public T Data { get; internal set; }
    }
}