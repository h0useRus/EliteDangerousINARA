using NSW.EliteDangerous.API;
using NSW.EliteDangerous.API.Events;
using NSW.EliteDangerous.INARA.Commands;

namespace NSW.EliteDangerous.INARA
{
    internal partial class EliteDangerousINARA
    {
        private readonly IEliteDangerousAPI _eliteDangerousAPI;

        public bool IsApiAttached => _eliteDangerousAPI != null;
        public SynchronizationStatus Synchronization { get; internal set; }

        public void StopSynchronization()
        {
            if (Synchronization == SynchronizationStatus.Stopped)
                return;

            _eliteDangerousAPI.StatusChanged -= OnStatusChanged;
        }

        public void StartSynchronization()
        {
            if (Synchronization != SynchronizationStatus.Stopped || !IsApiAttached)
                return;

            _eliteDangerousAPI.StatusChanged += OnStatusChanged;
            _eliteDangerousAPI.Game.LoadGame += GameOnLoadGame;
            _eliteDangerousAPI.Player.Commander += PlayerOnCommander;
            _eliteDangerousAPI.Player.NewCommander += PlayerOnNewCommander;
            _eliteDangerousAPI.Player.Friends += PlayerOnFriends;
            _eliteDangerousAPI.Player.Statistics += PlayerOnStatistics;
            _eliteDangerousAPI.Player.Rank += PlayerOnRank;
            _eliteDangerousAPI.Player.Promotion += PlayerOnPromotion;
            _eliteDangerousAPI.Player.Progress += PlayerOnProgress;
            _eliteDangerousAPI.Player.Reputation += PlayerOnReputation;
            _eliteDangerousAPI.Station.EngineerProgress += StationOnEngineerProgress;
            _eliteDangerousAPI.Powerplay.Powerplay += PowerplayOnPowerplay;
            _eliteDangerousAPI.Travel.FsdJump += TravelOnFsdJump;
            _eliteDangerousAPI.Travel.Location += TravelOnLocation;
            _eliteDangerousAPI.Travel.Docked += TravelOnDocked;
        }

        private void TravelOnDocked(object sender, DockedEvent e)
        {
            if(e==null) return;

            var request = AddCommand(new AddCommanderTravelDock(e.StarSystem, e.StationName) { MarketId = e.MarketId != 0 ? e.MarketId : (long?)null });
        }

        private async void TravelOnLocation(object sender, LocationEvent e)
        {
            if(e==null) return;

            var request = StartRequest();

            if (e.Factions != null && e.Factions.Length > 0)
                foreach (var faction in e.Factions)
                    request.AddCommand(new SetCommanderReputationMinorFaction(faction.Name, faction.MyReputation));

            request.AddCommand(new SetCommanderTravelLocation(e.StarSystem) { Station = e.StationName, MarketId = e.MarketId });

            await request.SendAsync().ConfigureAwait(false);
        }

        private async void TravelOnFsdJump(object sender, FsdJumpEvent e)
        {
            if(e==null) return;

            var request = StartRequest();

            if (e.Factions != null && e.Factions.Length > 0)
                foreach (var faction in e.Factions)
                    request.AddCommand(new SetCommanderReputationMinorFaction(faction.Name, faction.MyReputation));

            request.AddCommand(new AddCommanderTravelFsdJump(e.StarSystem) { JumpDistance = e.JumpDist });

            await request.SendAsync().ConfigureAwait(false);
        }

        private async void PlayerOnReputation(object sender, ReputationEvent e)
        {
            if(e==null) return;
            
            await AddCommand(new SetCommanderReputationMajorFaction(MajorFaction.Federation, e.Federation))
                .AddCommand(new SetCommanderReputationMajorFaction(MajorFaction.Alliance, e.Alliance))
                .AddCommand(new SetCommanderReputationMajorFaction(MajorFaction.Empire, e.Empire))
                .AddCommand(new SetCommanderReputationMajorFaction(MajorFaction.Independent, e.Independent))
                .SendAsync().ConfigureAwait(false); 
        }


        private async void PowerplayOnPowerplay(object sender, PowerplayEvent e)
        {
            if(e == null) return;

            await AddCommand(new SetCommanderRankPower(e.Power, e.Rank))
                .SendAsync()
                .ConfigureAwait(false);
        }

        private async void PlayerOnProgress(object sender, ProgressEvent e)
        {
            if(e==null) return;
            
            await AddCommand(new SetCommanderRankPilot(RankType.Combat, (double)e.Combat/100))
                    .AddCommand(new SetCommanderRankPilot(RankType.Cqc, (double)e.Cqc/100))
                    .AddCommand(new SetCommanderRankPilot(RankType.Empire, (double)e.Empire/100))
                    .AddCommand(new SetCommanderRankPilot(RankType.Explore, (double)e.Explore/100))
                    .AddCommand(new SetCommanderRankPilot(RankType.Federation, (double)e.Federation/100))
                    .AddCommand(new SetCommanderRankPilot(RankType.Trade, (double)e.Trade/100))
                    .SendAsync().ConfigureAwait(false);
        }

        private async void PlayerOnPromotion(object sender, PromotionEvent e)
        {
            if(e==null) return;

            var request = StartRequest();

            if (e.Combat.HasValue) request.AddCommand(new SetCommanderRankPilot(RankType.Combat, (int)e.Combat.Value));
            if (e.Cqc.HasValue) request.AddCommand(new SetCommanderRankPilot(RankType.Cqc, (int)e.Cqc.Value));
            if (e.Empire.HasValue) request.AddCommand(new SetCommanderRankPilot(RankType.Empire, (int)e.Empire.Value));
            if (e.Explore.HasValue) request.AddCommand(new SetCommanderRankPilot(RankType.Explore, (int)e.Explore.Value));
            if (e.Federation.HasValue) request.AddCommand(new SetCommanderRankPilot(RankType.Federation, (int)e.Federation.Value));
            if (e.Trade.HasValue) request.AddCommand(new SetCommanderRankPilot(RankType.Trade, (int)e.Trade.Value));

            await request.SendAsync().ConfigureAwait(false);
        }

        private async void PlayerOnRank(object sender, RankEvent e)
        {
            if(e==null) return;

            var request = StartRequest();

            if (e.Combat.HasValue) request.AddCommand(new SetCommanderRankPilot(RankType.Combat, (int)e.Combat.Value));
            if (e.Cqc.HasValue) request.AddCommand(new SetCommanderRankPilot(RankType.Cqc, (int)e.Cqc.Value));
            if (e.Empire.HasValue) request.AddCommand(new SetCommanderRankPilot(RankType.Empire, (int)e.Empire.Value));
            if (e.Explore.HasValue) request.AddCommand(new SetCommanderRankPilot(RankType.Explore, (int)e.Explore.Value));
            if (e.Federation.HasValue) request.AddCommand(new SetCommanderRankPilot(RankType.Federation, (int)e.Federation.Value));
            if (e.Trade.HasValue) request.AddCommand(new SetCommanderRankPilot(RankType.Trade, (int)e.Trade.Value));

            await request.SendAsync().ConfigureAwait(false);
        }

        private async void StationOnEngineerProgress(object sender, EngineerProgressEvent e)
        {
            if(e?.Engineers == null || e.Engineers.Length==0) return;

            var request = StartRequest();
            foreach (var engineerProgress in e.Engineers)
                request.AddCommand(new SetCommanderRankEngineer(engineerProgress.EngineerName, engineerProgress.Progress.ToString()) {RankValue = engineerProgress.Rank});

            await request.SendAsync().ConfigureAwait(false);
        }

        private async void PlayerOnStatistics(object sender, StatisticsEvent e)
        {
            if (e != null)
                await AddCommand(new SetCommanderGameStatistics(e))
                    .SendAsync()
                    .ConfigureAwait(false);
        }

        private async void PlayerOnFriends(object sender, FriendsEvent e)
        {
            if(e!=null)
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

        private async void GameOnLoadGame(object sender, LoadGameEvent e)
        {
            if(e==null) return;

            SetCommander(e.Commander, e.FrontierId);
            await AddCommand(new SetCommanderCredits(e.Credits) {Loan = e.Loan})
                .SendAsync()
                .ConfigureAwait(false);

        }

        private void PlayerOnNewCommander(object sender, NewCommanderEvent e)
        {
            if(e!=null) SetCommander(e.Name, e.FrontierId);
        }

        private void PlayerOnCommander(object sender, CommanderEvent e)
        {
            if (e != null) SetCommander(e.Name, e.FrontierId);
        }

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