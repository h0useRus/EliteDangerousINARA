using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA
{
    internal class RequestHeader
    {
        [JsonProperty("appName")]
        public string ApplicationName { get; set; }
        [JsonProperty("appVersion")]
        public string ApplicationVersion { get; set; }
        [JsonProperty("isDeveloped")]
        public bool IsDevelopment { get; set; }
        [JsonProperty("APIkey")]
        public string ApiKey { get; set; }
        [JsonProperty("commanderName")]
        public string Commander { get; set; }
        [JsonProperty("commanderFrontierID")]
        public string FrontierId { get; set; }

        internal RequestHeader() {}

        internal RequestHeader(InaraOptions options)
        {
            ApplicationName = options.ApplicationName;
            ApplicationVersion = options.ApplicationVersion;
            IsDevelopment = options.IsDevelopment;
            ApiKey = options.ApiKey;
            Commander = options.Commander;
            FrontierId = options.FrontierId;
        }
    }
}