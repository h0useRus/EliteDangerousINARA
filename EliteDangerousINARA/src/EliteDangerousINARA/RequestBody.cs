using System;
using Newtonsoft.Json;
using NSW.EliteDangerous.INARA.Commands;

namespace NSW.EliteDangerous.INARA
{
    internal class RequestBody
    {
        [JsonProperty("eventName")]
        public string Name { get; set; }
        [JsonProperty("eventTimestamp")]
        public DateTime Timestamp { get; set; }

        public RequestBody(string name)
        {
            Name = name;
            Timestamp = DateTime.UtcNow;
        }
    }
    internal class RequestBody<TEvent> : RequestBody where TEvent: Command
    {
        [JsonProperty("eventData")]
        public TEvent Data { get; set; }

        public RequestBody(TEvent data) : base(data.CommandName)
        {
            Data = data;
        }
    }
}