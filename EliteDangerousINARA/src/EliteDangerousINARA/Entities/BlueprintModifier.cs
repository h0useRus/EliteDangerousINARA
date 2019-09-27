using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA
{
    public class BlueprintModifier
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("value")]
        public double Value { get; set; }
        [JsonProperty("originalValue")]
        public double OriginalValue { get; set; }
        [JsonProperty("lessIsGood")]
        public bool LessIsGood { get; set; }
    }
}