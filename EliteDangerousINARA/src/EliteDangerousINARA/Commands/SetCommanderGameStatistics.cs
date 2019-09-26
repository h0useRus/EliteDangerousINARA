using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NSW.EliteDangerous.API;
using NSW.EliteDangerous.API.Events;

namespace NSW.EliteDangerous.INARA.Commands
{
    /// <summary>
    /// Sets commander's in-game statistics. Please note that the statistics are always overridden as a whole,
    /// so any partial updates will cause erasing of the rest.
    /// </summary>
    public class SetCommanderGameStatistics : Command
    {
        internal override string CommandName => "setCommanderGameStatistics";

        [JsonProperty("Bank_Account")]
        public BankStatistics BankAccount { get; internal set; }

        [JsonProperty("Combat")]
        public CombatStatistics Combat { get; internal set; }

        [JsonProperty("Crime")]
        public CrimeStatistics Crime { get; internal set; }

        [JsonProperty("Smuggling")]
        public SmugglingStatistics Smuggling { get; internal set; }

        [JsonProperty("Trading")]
        public TradingStatistics Trading { get; internal set; }

        [JsonProperty("Mining")]
        public MiningStatistics Mining { get; internal set; }

        [JsonProperty("Exploration")]
        public ExplorationStatistics Exploration { get; internal set; }

        [JsonProperty("Passengers")]
        public PassengersStatistics Passengers { get; internal set; }

        [JsonProperty("Search_And_Rescue")]
        public SearchAndRescueStatistics SearchAndRescue { get; internal set; }

        [JsonProperty("TG_ENCOUNTERS")]
        public ThargoidEncountersStatistics TgEncounters { get; internal set; }

        [JsonProperty("Crafting")]
        public CraftingStatistics Crafting { get; internal set; }

        [JsonProperty("Crew")]
        public NpcCrewStatistics NpcCrew { get; internal set; }

        [JsonProperty("Multicrew")]
        public MultiCrewStatistics MultiCrew { get; internal set; }

        [JsonProperty("Material_Trader_Stats")]
        public MaterialTraderStatistics MaterialTraderStats { get; internal set; }

        [JsonProperty("CQC")]
        public Dictionary<string, double> Cqc { get; internal set; }

        public SetCommanderGameStatistics(StatisticsEvent @event)
        {
            BankAccount = @event.BankAccount;
            Combat = @event.Combat;
            Crime = @event.Crime;
            Smuggling = @event.Smuggling;
            Trading = @event.Trading;
            Mining = @event.Mining;
            Exploration = @event.Exploration;
            Passengers = @event.Passengers;
            SearchAndRescue = @event.SearchAndRescue;
            TgEncounters = @event.TgEncounters;
            Crafting = @event.Crafting;
            NpcCrew = @event.NpcCrew;
            MultiCrew = @event.MultiCrew;
            MaterialTraderStats = @event.MaterialTraderStats;
            Cqc = @event.Cqc;
        }
    }
}