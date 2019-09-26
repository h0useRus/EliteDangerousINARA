using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA
{
    internal class ResponseData
    {
        [JsonProperty("eventCustomID")]
        public int? Id { get; internal set; }
        [JsonProperty("eventStatus")]
        public ResponseStatus Status { get; internal set; }
        [JsonProperty("eventStatusText")]
        public string StatusText { get; internal set; }
    }
    internal class ResponseData<T> : ResponseData
    {
        [JsonProperty("eventData")]
        public T Data { get; internal set; }
    }
}