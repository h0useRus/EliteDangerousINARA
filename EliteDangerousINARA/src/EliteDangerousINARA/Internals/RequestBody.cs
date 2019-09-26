using System;
using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA
{
    internal class RequestBody
    {
        [JsonProperty("eventCustomID")]
        public int? Id { get; set; }
        [JsonProperty("eventName")]
        public string Name { get; set; }
        [JsonProperty("eventTimestamp")]
        public DateTime Timestamp { get; set; }
        [JsonProperty("eventData")]
        public object Data { get; set; }

        public RequestBody(ISystemClock clock, string name, object data, int? id = null)
        {
            Id = id;
            Name = name;
            Timestamp = clock.Now;
            Data = data;
        }
    }
}