using System;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NSW.EliteDangerous.INARA.Events
{
    public class ShipResult
    {
        [JsonProperty("shipInaraID")]
        public int ShipInaraId { get; internal set; }
        [JsonProperty("shipInaraID")]
        public string ShipInaraUrl { get; internal set; }

        internal static EventResult<ShipResult> Process(ResponseData<JObject> response, EliteDangerousINARA inara)
        {
            var result = new EventResult<ShipResult>
            {
                Name = "getCommanderProfileResult",
                Status = response?.Status ?? ResponseStatus.OK,
                StatusText = response?.StatusText
            };

            if(response?.Data != null)
                try
                {
                    result.Data = response.Data.ToObject<ShipResult>();
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