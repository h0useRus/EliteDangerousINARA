using System.Linq;
using NSW.EliteDangerous.API;
using NSW.EliteDangerous.API.Events;
using NSW.EliteDangerous.INARA.Commands;

namespace NSW.EliteDangerous.INARA
{
    internal partial class EliteDangerousINARA
    {
        private readonly IEliteDangerousAPI _eliteAPI;

        public bool IsApiAttached => _eliteAPI != null;
        public SynchronizationStatus Synchronization { get; internal set; } = SynchronizationStatus.Stopped;

        public void StopSynchronization()
        {
            if (Synchronization == SynchronizationStatus.Stopped)
                return;

            _eliteAPI.StatusChanged -= OnStatusChanged;
            _eliteAPI.GameEvents.LoadGame -= GameOnLoadGameAsync;

            _eliteAPI.PlayerEvents.Commander -= PlayerOnCommander;
            _eliteAPI.PlayerEvents.NewCommander -= PlayerOnNewCommander;
            _eliteAPI.PlayerEvents.Friends -= PlayerOnFriendsAsync;
            _eliteAPI.PlayerEvents.Statistics -= PlayerOnStatisticsAsync;
            _eliteAPI.PlayerEvents.Rank -= PlayerOnRankAsync;
            _eliteAPI.PlayerEvents.Promotion -= PlayerOnPromotionAsync;
            _eliteAPI.PlayerEvents.Progress -= PlayerOnProgressAsync;
            _eliteAPI.PlayerEvents.Reputation -= PlayerOnReputationAsync;
            
            _eliteAPI.PowerplayEvents.Powerplay -= PowerplayOnPowerplayAsync;

            _eliteAPI.TravelEvents.FsdJump -= TravelOnFsdJumpAsync;
            _eliteAPI.TravelEvents.Location -= TravelOnLocationAsync;
            _eliteAPI.TravelEvents.Docked -= TravelOnDockedAsync;

            _eliteAPI.StationEvents.EngineerProgress -= StationOnEngineerProgressAsync;
            _eliteAPI.StationEvents.ShipyardBuy -= StationOnShipyardBuyAsync;
            _eliteAPI.StationEvents.ShipyardNew -= StationOnShipyardNewAsync;
            _eliteAPI.StationEvents.SetUserShipName -= StationOnSetUserShipNameAsync;
            _eliteAPI.StationEvents.ShipyardSwap -= StationOnShipyardSwapAsync;
            _eliteAPI.StationEvents.ShipyardTransfer -= StationEventsOnShipyardTransferAsync;

            _eliteAPI.ShipEvents.Loadout -= ShipEventsOnLoadoutAsync;

            _eliteAPI.CombatEvents.Died -= CombatEventsOnDiedAsync;
            _eliteAPI.CombatEvents.Interdicted -= CombatEventsOnInterdictedAsync;
            _eliteAPI.CombatEvents.Interdiction -= CombatEventsOnInterdictionAsync;
            _eliteAPI.CombatEvents.EscapeInterdiction -= CombatEventsOnEscapeInterdictionAsync;
            _eliteAPI.CombatEvents.PvpKill -= CombatEventsOnPvpKillAsync;

            Synchronization = SynchronizationStatus.Stopped;
        }

        public void StartSynchronization()
        {
            if (Synchronization != SynchronizationStatus.Stopped || !IsApiAttached)
                return;

            _eliteAPI.StatusChanged += OnStatusChanged;
            _eliteAPI.GameEvents.LoadGame += GameOnLoadGameAsync;

            _eliteAPI.PlayerEvents.Commander += PlayerOnCommander;
            _eliteAPI.PlayerEvents.NewCommander += PlayerOnNewCommander;
            _eliteAPI.PlayerEvents.Friends += PlayerOnFriendsAsync;
            _eliteAPI.PlayerEvents.Statistics += PlayerOnStatisticsAsync;
            _eliteAPI.PlayerEvents.Rank += PlayerOnRankAsync;
            _eliteAPI.PlayerEvents.Promotion += PlayerOnPromotionAsync;
            _eliteAPI.PlayerEvents.Progress += PlayerOnProgressAsync;
            _eliteAPI.PlayerEvents.Reputation += PlayerOnReputationAsync;
            
            _eliteAPI.PowerplayEvents.Powerplay += PowerplayOnPowerplayAsync;

            _eliteAPI.TravelEvents.FsdJump += TravelOnFsdJumpAsync;
            _eliteAPI.TravelEvents.Location += TravelOnLocationAsync;
            _eliteAPI.TravelEvents.Docked += TravelOnDockedAsync;

            _eliteAPI.StationEvents.EngineerProgress += StationOnEngineerProgressAsync;
            _eliteAPI.StationEvents.ShipyardBuy += StationOnShipyardBuyAsync;
            _eliteAPI.StationEvents.ShipyardNew += StationOnShipyardNewAsync;
            _eliteAPI.StationEvents.SetUserShipName += StationOnSetUserShipNameAsync;
            _eliteAPI.StationEvents.ShipyardSwap += StationOnShipyardSwapAsync;
            _eliteAPI.StationEvents.ShipyardTransfer += StationEventsOnShipyardTransferAsync;

            _eliteAPI.ShipEvents.Loadout += ShipEventsOnLoadoutAsync;

            _eliteAPI.CombatEvents.Died += CombatEventsOnDiedAsync;
            _eliteAPI.CombatEvents.Interdicted += CombatEventsOnInterdictedAsync;
            _eliteAPI.CombatEvents.Interdiction += CombatEventsOnInterdictionAsync;
            _eliteAPI.CombatEvents.EscapeInterdiction += CombatEventsOnEscapeInterdictionAsync;
            _eliteAPI.CombatEvents.PvpKill += CombatEventsOnPvpKillAsync;

            _eliteAPI.Start();

            Synchronization = _eliteAPI.Status == ApiStatus.Running
                ? SynchronizationStatus.Running
                : SynchronizationStatus.Pending;
        }

        private async void CombatEventsOnPvpKillAsync(object sender, PvpKillEvent e) =>
            await AddCommand(new AddCommanderCombatKill(_eliteAPI.Location?.StarSystem?.Name, e.Victim))
                .SendAsync()
                .ConfigureAwait(false);

        private async void CombatEventsOnEscapeInterdictionAsync(object sender, EscapeInterdictionEvent e) =>
            await AddCommand(new AddCommanderCombatInterdictionEscape(_eliteAPI.Location?.StarSystem?.Name, e.Interdictor, e.IsPlayer))
                .SendAsync()
                .ConfigureAwait(false);

        private async void CombatEventsOnInterdictionAsync(object sender, InterdictionEvent e) =>
            await AddCommand(new AddCommanderCombatInterdiction(_eliteAPI.Location?.StarSystem?.Name, e.Interdicted, e.IsPlayer) { IsSuccess = e.Success })
                .SendAsync()
                .ConfigureAwait(false);

        private async void CombatEventsOnInterdictedAsync(object sender, InterdictedEvent e)
        {
            if(e.Submitted)
                await AddCommand(new AddCommanderCombatInterdicted(_eliteAPI.Location?.StarSystem?.Name, e.Interdictor, e.IsPlayer))
                    .SendAsync()
                    .ConfigureAwait(false);
        }

        private async void CombatEventsOnDiedAsync(object sender, DiedEvent e)
        {
            var cmd = new AddCommanderCombatDeath(_eliteAPI.Location?.StarSystem?.Name);

            if (e.Killers != null && e.Killers.Length > 0)
            {
                cmd.WingOpponentNames = e.Killers.Select(k => k.Name);
            }
            else
            {
                cmd.OpponentName = e.KillerName;
                cmd.IsPlayer = e.KillerNameLocalised == null;
            }

            await AddCommand(cmd)
                .SendAsync()
                .ConfigureAwait(false);
        }

        private async void StationEventsOnShipyardTransferAsync(object sender, ShipyardTransferEvent e) =>
            await AddCommand(new SetCommanderShipTransfer(e.ShipType, e.ShipId, _eliteAPI.Location?.StarSystem?.Name, _eliteAPI.Location?.Station?.Name)
            {
                MarketId = _eliteAPI.Location?.Station?.MarketId,
                TransferTime = e.TransferTime > 0 ? e.TransferTime : (long?)null
            }).SendAsync()
                .ConfigureAwait(false);

        private async void ShipEventsOnLoadoutAsync(object sender, LoadoutEvent e)
        {
            if (e.Modules != null && e.Modules.Length > 0)
            {
                await AddCommand(new SetCommanderShipLoadout(e.Ship, e.ShipId, e.Modules.Select(ShipModule.FromApi).ToList()))
                    .SendAsync()
                    .ConfigureAwait(false);
            }
        }

        private async void StationOnShipyardSwapAsync(object sender, ShipyardSwapEvent e) =>
            await AddCommand(new SetCommanderShip(e.StoreOldShip, e.StoreShipId)
                {
                    StarSystem = _eliteAPI.Location?.StarSystem?.Name,
                    Station = _eliteAPI.Location?.Station?.Name
                })
                .AddCommand(new SetCommanderShip(e.ShipType, e.ShipId)
                {
                    IsCurrent = true
                })
                .SendAsync()
                .ConfigureAwait(false);

        private async void StationOnSetUserShipNameAsync(object sender, SetUserShipNameEvent e) =>
            await AddCommand(new SetCommanderShip(e.Ship, e.ShipId)
            {
                Name = e.UserShipName,
                Identifier = e.UserShipId
            }).SendAsync()
                .ConfigureAwait(false);

        private async void StationOnShipyardNewAsync(object sender, ShipyardNewEvent e) =>
            await AddCommand(new AddCommanderShip(e.ShipType, e.NewShipId))
                .SendAsync()
                .ConfigureAwait(false);

        private async void StationOnShipyardBuyAsync(object sender, ShipyardBuyEvent e)
        {
            var request = StartRequest();

            if (e.StoreShipId.HasValue)
                AddCommand(new SetCommanderShip(e.StoreOldShip, e.StoreShipId.Value) { StarSystem = _eliteAPI.Location?.StarSystem?.Name, Station = _eliteAPI.Location?.Station?.Name });
            else if (e.SellShipID.HasValue)
                AddCommand(new RemoveCommanderShip(e.SellOldShip, e.SellShipID.Value));

            await request.SendAsync().ConfigureAwait(false);
        }

        private async void TravelOnDockedAsync(object sender, DockedEvent e) =>
            await AddCommand(new AddCommanderTravelDock(e.StarSystem, e.StationName)
            {
                MarketId = e.MarketId != 0 ? e.MarketId : (long?)null,
                ShipId = _eliteAPI.Ship.ShipId,
                ShipType = _eliteAPI.Ship.ShipType
            }).SendAsync()
                .ConfigureAwait(false);

        private async void TravelOnLocationAsync(object sender, LocationEvent e)
        {
            var request = StartRequest();

            if (e.Factions != null && e.Factions.Length > 0)
                foreach (var faction in e.Factions)
                    request.AddCommand(new SetCommanderReputationMinorFaction(faction.Name, faction.MyReputation));

            request.AddCommand(new SetCommanderTravelLocation(e.StarSystem) { Station = e.StationName, MarketId = e.MarketId });

            await request.SendAsync()
                .ConfigureAwait(false);
        }

        private async void TravelOnFsdJumpAsync(object sender, FsdJumpEvent e)
        {
            var request = StartRequest();

            if (e.Factions != null && e.Factions.Length > 0)
                foreach (var faction in e.Factions)
                    request.AddCommand(new SetCommanderReputationMinorFaction(faction.Name, faction.MyReputation));

            request.AddCommand(new AddCommanderTravelFsdJump(e.StarSystem)
            {
                JumpDistance = e.JumpDist,
                ShipType = _eliteAPI.Ship?.ShipType,
                ShipId = _eliteAPI.Ship?.ShipId
            });

            await request.SendAsync().ConfigureAwait(false);
        }

        private async void PlayerOnReputationAsync(object sender, ReputationEvent e) =>
            await AddCommand(new SetCommanderReputationMajorFaction(MajorFaction.Federation, e.Federation))
                .AddCommand(new SetCommanderReputationMajorFaction(MajorFaction.Alliance, e.Alliance))
                .AddCommand(new SetCommanderReputationMajorFaction(MajorFaction.Empire, e.Empire))
                .AddCommand(new SetCommanderReputationMajorFaction(MajorFaction.Independent, e.Independent))
                .SendAsync()
                .ConfigureAwait(false);


        private async void PowerplayOnPowerplayAsync(object sender, PowerplayEvent e) =>
            await AddCommand(new SetCommanderRankPower(e.Power, e.Rank))
                .SendAsync()
                .ConfigureAwait(false);

        private async void PlayerOnProgressAsync(object sender, ProgressEvent e) =>
            await AddCommand(new SetCommanderRankPilot(RankType.Combat, (double)e.Combat/100))
                .AddCommand(new SetCommanderRankPilot(RankType.Cqc, (double)e.Cqc/100))
                .AddCommand(new SetCommanderRankPilot(RankType.Empire, (double)e.Empire/100))
                .AddCommand(new SetCommanderRankPilot(RankType.Explore, (double)e.Explore/100))
                .AddCommand(new SetCommanderRankPilot(RankType.Federation, (double)e.Federation/100))
                .AddCommand(new SetCommanderRankPilot(RankType.Trade, (double)e.Trade/100))
                .SendAsync()
                .ConfigureAwait(false);

        private async void PlayerOnPromotionAsync(object sender, PromotionEvent e)
        {
            var request = StartRequest();

            if (e.Combat.HasValue) request.AddCommand(new SetCommanderRankPilot(RankType.Combat, (int)e.Combat.Value));
            if (e.Cqc.HasValue) request.AddCommand(new SetCommanderRankPilot(RankType.Cqc, (int)e.Cqc.Value));
            if (e.Empire.HasValue) request.AddCommand(new SetCommanderRankPilot(RankType.Empire, (int)e.Empire.Value));
            if (e.Explore.HasValue) request.AddCommand(new SetCommanderRankPilot(RankType.Explore, (int)e.Explore.Value));
            if (e.Federation.HasValue) request.AddCommand(new SetCommanderRankPilot(RankType.Federation, (int)e.Federation.Value));
            if (e.Trade.HasValue) request.AddCommand(new SetCommanderRankPilot(RankType.Trade, (int)e.Trade.Value));

            await request.SendAsync()
                .ConfigureAwait(false);
        }

        private async void PlayerOnRankAsync(object sender, RankEvent e)
        {
            var request = StartRequest();

            if (e.Combat.HasValue) request.AddCommand(new SetCommanderRankPilot(RankType.Combat, (int)e.Combat.Value));
            if (e.Cqc.HasValue) request.AddCommand(new SetCommanderRankPilot(RankType.Cqc, (int)e.Cqc.Value));
            if (e.Empire.HasValue) request.AddCommand(new SetCommanderRankPilot(RankType.Empire, (int)e.Empire.Value));
            if (e.Explore.HasValue) request.AddCommand(new SetCommanderRankPilot(RankType.Explore, (int)e.Explore.Value));
            if (e.Federation.HasValue) request.AddCommand(new SetCommanderRankPilot(RankType.Federation, (int)e.Federation.Value));
            if (e.Trade.HasValue) request.AddCommand(new SetCommanderRankPilot(RankType.Trade, (int)e.Trade.Value));

            await request.SendAsync()
                .ConfigureAwait(false);
        }

        private async void StationOnEngineerProgressAsync(object sender, EngineerProgressEvent e)
        {
            if(e?.Engineers == null || e.Engineers.Length==0) return;

            var request = StartRequest();
            foreach (var engineerProgress in e.Engineers)
                request.AddCommand(new SetCommanderRankEngineer(engineerProgress.EngineerName, engineerProgress.Progress.ToString()) {RankValue = engineerProgress.Rank});

            await request.SendAsync().ConfigureAwait(false);
        }

        private async void PlayerOnStatisticsAsync(object sender, StatisticsEvent e) =>
            await AddCommand(new SetCommanderGameStatistics(e))
                .SendAsync()
                .ConfigureAwait(false);

        private async void PlayerOnFriendsAsync(object sender, FriendsEvent e)
        {
            switch (e.Status)
            {
                case FriendStatus.Added:
                    await AddCommand(new AddCommanderFriend(e.Name, GamePlatform.PC))
                        .SendAsync()
                        .ConfigureAwait(false);
                    break;
                case FriendStatus.Lost:
                    await AddCommand(new RemoveCommanderFriend(e.Name, GamePlatform.PC))
                        .SendAsync()
                        .ConfigureAwait(false);
                    break;
            }
        }

        private async void GameOnLoadGameAsync(object sender, LoadGameEvent e)
        {
            SetCommander(e.Commander, e.FrontierId);
            await AddCommand(new SetCommanderCredits(e.Credits) {Loan = e.Loan})
                .SendAsync()
                .ConfigureAwait(false);
        }

        private void PlayerOnNewCommander(object sender, NewCommanderEvent e) => SetCommander(e.Name, e.FrontierId);

        private void PlayerOnCommander(object sender, CommanderEvent e) => SetCommander(e.Name, e.FrontierId);

        private void OnStatusChanged(object sender, ApiStatus status)
        {
            switch (status)
            {
                case ApiStatus.Running:
                    if (Synchronization == SynchronizationStatus.Pending)
                        Synchronization = SynchronizationStatus.Running;
                    break;
                case ApiStatus.Stopped:
                    StopSynchronization();
                    break;
            }
        }
    }
}