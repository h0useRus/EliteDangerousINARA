using System.Collections.Generic;
using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA
{
    public class Blueprint
    {
        [JsonProperty("blueprintName")]
        public string BlueprintName { get; set; }

        [JsonProperty("blueprintLevel")]
        public short Level { get; set; }

        [JsonProperty("blueprintQuality")]
        public double Quality { get; set; }

        [JsonProperty("experimentalEffect")]
        public string ExperimentalEffect { get; set; }

        [JsonProperty("modifiers")]
        public IEnumerable<BlueprintModifier> Modifiers { get; set; }
    }
}