using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using NSW.EliteDangerous.API;

namespace NSW.EliteDangerous.INARA
{
    public class ShipModule
    {
        [JsonProperty("slotName")]
        public string Slot { get; set; }

        [JsonProperty("itemName")]
        public string Item { get; set; }

        [JsonProperty("itemValue")]
        public long Value { get; set; }

        [JsonProperty("itemHealth")]
        public double Health { get; set; }

        [JsonProperty("isOn")]
        public bool IsOn { get; set; }

        [JsonProperty("isOn")]
        public bool IsHot { get; set; }

        [JsonProperty("itemPriority")]
        public int Priority { get; set; }

        [JsonProperty("engineering")]
        public Blueprint Engineering { get; set; }

        [JsonProperty("itemAmmoClip")]
        public long? AmmoInClip { get; set; }

        [JsonProperty("itemAmmoHopper")]
        public long? AmmoInHopper { get; set; }

        public static ShipModule FromApi(Module module)
        {
            var result = new ShipModule
            {
                Slot = module.Slot,
                Item = module.Item,
                Value = module.Value ?? 0,
                Health = module.Health,
                IsOn = module.On,
                IsHot = false, // TODO: add hot
                Priority = module.Priority,
                AmmoInClip = module.AmmoInClip,
                AmmoInHopper = module.AmmoInHopper
            };

            if (module.Engineering != null)
            {
                result.Engineering = new Blueprint
                {
                    BlueprintName = module.Engineering.BlueprintName,
                    Level = module.Engineering.Level,
                    Quality = module.Engineering.Quality,
                    ExperimentalEffect = module.Engineering.ExperimentalEffect
                };

                if (module.Engineering.Modifications != null && module.Engineering.Modifications.Length > 0)
                {
                    result.Engineering.Modifiers
                        = module.Engineering.Modifications
                            .Select(modification => new BlueprintModifier
                            {
                                Name = modification.Label.ToString(),
                                Value = modification.Value,
                                OriginalValue = modification.OriginalValue,
                                LessIsGood = modification.LessIsGood
                            })
                            .ToList();
                }
            }

            return result;
        }
    }
}