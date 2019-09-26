using System;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NSW.EliteDangerous.INARA.Events
{
    public class TravelResult
    {
        [JsonProperty("starsystemInaraID")]
        public int StarSystemInaraId { get; internal set; }

        [JsonProperty("starsystemInaraURL")]
        public string StarSystemInaraUrl { get; internal set; }

        [JsonProperty("stationInaraID")]
        public int? StationInaraId { get; internal set; }

        [JsonProperty("stationInaraURL")]
        public string StationInaraUrl { get; internal set; }

        [JsonProperty("marketInaraURL")]
        public string MarketInaraUrl { get; internal set; }

        internal static EventResult<TravelResult> Process(ResponseData<JObject> response, EliteDangerousINARA inara)
        {
            var result = new EventResult<TravelResult>
            {
                Name = "getCommanderProfileResult",
                Status = response?.Status ?? ResponseStatus.OK,
                StatusText = response?.StatusText
            };

            if(response?.Data != null)
                try
                {
                    result.Data = response.Data.ToObject<TravelResult>();
                }
                catch (Exception exception)
                {
                    inara.Log.LogError(exception, exception.Message);
                    result.Status = ResponseStatus.Error;
                    result.StatusText = exception.Message;
                }

            return result;
        }
    }
}