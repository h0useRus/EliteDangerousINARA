using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA
{
    public class ModuleEngineering
    {
        [JsonProperty("blueprintName")]
        public string BlueprintName { get; set; }

        [JsonProperty("blueprintLevel")]
        public short Level { get; set; }

        [JsonProperty("blueprintQuality")]
        public double Quality { get; set; }

        [JsonProperty("experimentalEffect")]
        public string ExperimentalEffect { get; set; }
    }
}