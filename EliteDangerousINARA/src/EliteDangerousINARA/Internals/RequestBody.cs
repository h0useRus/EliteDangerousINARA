using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NSW.EliteDangerous.INARA.Commands;

namespace NSW.EliteDangerous.INARA
{
    internal class RequestBody
    {
        [JsonProperty("eventName")]
        public string Name { get; set; }
        [JsonProperty("eventTimestamp")]
        public DateTime Timestamp { get; set; }
        [JsonProperty("eventData")]
        public object Data { get; set; }

        public RequestBody(ISystemClock clock, string name, object data)
        {
            Name = name;
            Timestamp = clock.Now;
            Data = data;
        }
    }

    //internal class RequestBody<TEvent> : RequestBody where TEvent: Command
    //{
    //    [JsonProperty("eventData")]
    //    public TEvent Data { get; set; }

    //    public RequestBody(ISystemClock clock, TEvent data) : base(clock, data.CommandName)
    //    {
    //        Data = data;
    //    }
    //}
    
}