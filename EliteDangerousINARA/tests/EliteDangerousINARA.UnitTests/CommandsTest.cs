using System;
using NSW.EliteDangerous.INARA.Commands;
using Xunit;

namespace NSW.EliteDangerous.INARA
{
    public class CommandsTest : IClassFixture<InaraCommandFixture>
    {
        private readonly InaraCommandFixture _fixture;

        public CommandsTest(InaraCommandFixture fixture)
        {
            _fixture = fixture;
            Assert.False(_fixture.INARA.IsApiAttached);
        }

        [Fact]
        public void AddCommanderFriend()
        {
            var json = _fixture.INARA.AddCommand(new AddCommanderFriend("Vasya Pupkin", GamePlatform.PC)).GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"addCommanderFriend\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"commanderName\":\"Vasya Pupkin\",\"gamePlatform\":\"pc\"}}]}", json);
        }

        [Fact]
        public void RemoveCommanderFriend()
        {
            var json = _fixture.INARA.AddCommand(new RemoveCommanderFriend("Vasya Pupkin", GamePlatform.PC)).GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"delCommanderFriend\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"commanderName\":\"Vasya Pupkin\",\"gamePlatform\":\"pc\"}}]}", json);
        }

        [Fact]
        public void FindCommanderProfile()
        {
            var json = _fixture.INARA.AddCommand(new GetCommanderProfile("Vasya Pupkin")).GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"getCommanderProfile\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"searchName\":\"Vasya Pupkin\"}}]}", json);
        }

        [Fact]
        public void AddCommanderPermit()
        {
            var json = _fixture.INARA.AddCommand(new AddCommanderPermit("Sol")).GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"addCommanderPermit\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"starsystemName\":\"Sol\"}}]}", json);
        }

        [Fact]
        public void SetCommanderCredits()
        {
            var json = _fixture.INARA.AddCommand(new SetCommanderCredits(1000)).GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"setCommanderCredits\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"commanderCredits\":1000}}]}", json);

            json = _fixture.INARA.AddCommand(new SetCommanderCredits(1000) { Assets = 50, Loan = 20 }).GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"setCommanderCredits\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"commanderCredits\":1000,\"commanderAssets\":50,\"commanderLoan\":20}}]}", json);
        }

        [Fact]
        public void SetCommanderGameStatistics()
        {
            // TODO: Add tests
        }

        [Fact]
        public void SetCommanderRankEngineer()
        {
            var json = _fixture.INARA
                .AddCommand(new SetCommanderRankEngineer("Elvira Martuuk", "Unlocked", 4))
                .AddCommand(new SetCommanderRankEngineer("Liz Ryder", "Unlocked", 2))
                .GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"setCommanderRankEngineer\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":[{\"engineerName\":\"Elvira Martuuk\",\"rankStage\":\"Unlocked\",\"rankValue\":4},{\"engineerName\":\"Liz Ryder\",\"rankStage\":\"Unlocked\",\"rankValue\":2}]}]}", json);
        }

        [Fact]
        public void SetCommanderRankPilot()
        {
            var json = _fixture.INARA
                .AddCommand(new SetCommanderRankPilot(RankType.Combat, 4))
                .GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"setCommanderRankPilot\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"rankName\":\"combat\",\"rankValue\":4}}]}", json);

            json = _fixture.INARA
                .AddCommand(new SetCommanderRankPilot(RankType.Combat, 4))
                .AddCommand(new SetCommanderRankPilot(RankType.Trade, 2, 0.31))
                .GetJson();

            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"setCommanderRankPilot\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":[{\"rankName\":\"combat\",\"rankValue\":4},{\"rankName\":\"trade\",\"rankValue\":2,\"rankProgress\":0.31}]}]}", json);
        }

        [Fact]
        public void SetCommanderRankPower()
        {
            var json = _fixture.INARA.AddCommand(new SetCommanderRankPower("Felicia Winters", 2)).GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"setCommanderRankPower\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"powerName\":\"Felicia Winters\",\"rankValue\":2}}]}", json);
        }

        [Fact]
        public void SetCommanderReputationMajorFaction()
        {
            var json = _fixture.INARA.AddCommand(new SetCommanderReputationMajorFaction(MajorFaction.Empire, 0.84)).GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"setCommanderReputationMajorFaction\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"majorfactionName\":\"empire\",\"majorfactionReputation\":0.84}}]}", json);

            json = _fixture.INARA
                .AddCommand(new SetCommanderReputationMajorFaction(MajorFaction.Empire, 0.84))
                .AddCommand(new SetCommanderReputationMajorFaction(MajorFaction.Federation, 0.36))
                .GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"setCommanderReputationMajorFaction\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":[{\"majorfactionName\":\"empire\",\"majorfactionReputation\":0.84},{\"majorfactionName\":\"federation\",\"majorfactionReputation\":0.36}]}]}", json);
        }

        [Fact]
        public void SetCommanderReputationMinorFaction()
        {
            var json = _fixture.INARA.AddCommand(new SetCommanderReputationMinorFaction("Inara Nexus", 0.76)).GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"setCommanderReputationMinorFaction\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"minorfactionName\":\"Inara Nexus\",\"minorfactionReputation\":0.76}}]}", json);

            json = _fixture.INARA
                .AddCommand(new SetCommanderReputationMinorFaction("Inara Nexus", 0.76))
                .AddCommand(new SetCommanderReputationMinorFaction("Inara Independents", 0.24))
                .GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"setCommanderReputationMinorFaction\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":[{\"minorfactionName\":\"Inara Nexus\",\"minorfactionReputation\":0.76},{\"minorfactionName\":\"Inara Independents\",\"minorfactionReputation\":0.24}]}]}", json);
        }

        [Fact]
        public void AddCommanderInventoryCargoItem()
        {
            var json = _fixture.INARA.AddCommand(new AddCommanderInventoryCargoItem("conductivefabrics", 3)).GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"addCommanderInventoryCargoItem\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"itemName\":\"conductivefabrics\",\"itemCount\":3}}]}", json);

            json = _fixture.INARA.AddCommand(new AddCommanderInventoryCargoItem("conductivefabrics", 3) { IsStolen = true }).GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"addCommanderInventoryCargoItem\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"itemName\":\"conductivefabrics\",\"itemCount\":3,\"isStolen\":true}}]}", json);

            json = _fixture.INARA.AddCommand(new AddCommanderInventoryCargoItem("conductivefabrics", 3) { IsStolen = true, MissionID = 1234567890 }).GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"addCommanderInventoryCargoItem\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"itemName\":\"conductivefabrics\",\"itemCount\":3,\"isStolen\":true,\"missionGameID\":1234567890}}]}", json);
        }

        [Fact]
        public void RemoveCommanderInventoryCargoItem()
        {
            var json = _fixture.INARA.AddCommand(new RemoveCommanderInventoryCargoItem("conductivefabrics", 3)).GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"delCommanderInventoryCargoItem\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"itemName\":\"conductivefabrics\",\"itemCount\":3}}]}", json);

            json = _fixture.INARA.AddCommand(new RemoveCommanderInventoryCargoItem("conductivefabrics", 3) { IsStolen = true }).GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"delCommanderInventoryCargoItem\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"itemName\":\"conductivefabrics\",\"itemCount\":3,\"isStolen\":true}}]}", json);

            json = _fixture.INARA.AddCommand(new RemoveCommanderInventoryCargoItem("conductivefabrics", 3) { IsStolen = true, MissionID = 1234567890 }).GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"delCommanderInventoryCargoItem\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"itemName\":\"conductivefabrics\",\"itemCount\":3,\"isStolen\":true,\"missionGameID\":1234567890}}]}", json);
        }

        [Fact]
        public void SetCommanderInventoryCargo()
        {
            var json = _fixture.INARA
                .AddCommand(new SetCommanderInventoryCargo("conductivefabrics", 1))
                .AddCommand(new SetCommanderInventoryCargo("modularterminals", 6))
                .AddCommand(new SetCommanderInventoryCargo("modularterminals", 2) { IsStolen = true })
                .AddCommand(new SetCommanderInventoryCargo("cobalt", 6) { MissionId = 170577201 })
                .GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"setCommanderInventoryCargo\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":[{\"itemName\":\"conductivefabrics\",\"itemCount\":1},{\"itemName\":\"modularterminals\",\"itemCount\":6},{\"itemName\":\"modularterminals\",\"itemCount\":2,\"isStolen\":true},{\"itemName\":\"cobalt\",\"itemCount\":6,\"missionGameID\":170577201}]}]}", json);
        }

        [Fact]
        public void SetCommanderInventoryCargoItem()
        {
            var json = _fixture.INARA.AddCommand(new SetCommanderInventoryCargoItem("cobalt", 11) {MissionId = 170577201}).GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"setCommanderInventoryCargoItem\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"itemName\":\"cobalt\",\"itemCount\":11,\"missionGameID\":170577201}}]}", json);
        }

        [Fact]
        public void AddCommanderInventoryMaterialsItem()
        {
            var json = _fixture.INARA.AddCommand(new AddCommanderInventoryMaterialsItem("nickel", 2)).GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"addCommanderInventoryMaterialsItem\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"itemName\":\"nickel\",\"itemCount\":2}}]}", json);
        }

        [Fact]
        public void RemoveCommanderInventoryMaterialsItem()
        {
            var json = _fixture.INARA.AddCommand(new RemoveCommanderInventoryMaterialsItem("nickel", 2)).GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"delCommanderInventoryMaterialsItem\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"itemName\":\"nickel\",\"itemCount\":2}}]}", json);
        }

        [Fact]
        public void SetCommanderInventoryMaterials()
        {
            var json = _fixture.INARA
                .AddCommand(new SetCommanderInventoryMaterials("phosphorus", 30))
                .AddCommand(new SetCommanderInventoryMaterials("nickel", 10))
                .AddCommand(new SetCommanderInventoryMaterials("crystalshards", 20))
                .GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"setCommanderInventoryMaterials\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":[{\"itemName\":\"phosphorus\",\"itemCount\":30},{\"itemName\":\"nickel\",\"itemCount\":10},{\"itemName\":\"crystalshards\",\"itemCount\":20}]}]}", json);
        }

        [Fact]
        public void SetCommanderInventoryMaterialsItem()
        {
            var json = _fixture.INARA.AddCommand(new SetCommanderInventoryMaterialsItem("crystalshards", 15)).GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"setCommanderInventoryMaterialsItem\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"itemName\":\"crystalshards\",\"itemCount\":15}}]}", json);
        }

        [Fact]
        public void SetCommanderStorageModules()
        {
            var json = _fixture.INARA
                .AddCommand(new SetCommanderStorageModules("$hpt_pulselaser_gimbal_large_name"){ItemValue = 350640, IsHot = true, StarSystem = "Sol", Station = "Abraham Lincoln", MarketId = 128016896, Engineering = new Blueprint{BlueprintName = "Weapon_Overcharged", Level = 4, Quality = 0, ExperimentalEffect = "special_incendiary_rounds"}})
                .AddCommand(new SetCommanderStorageModules("$int_cargorack_size7_class1_name"){ItemValue = 12800, IsHot = false, StarSystem = "Sol", Station = "Abraham Lincoln", MarketId = 128016896})
                .AddCommand(new SetCommanderStorageModules("$int_dronecontrol_collection_size5_class5_name"){ItemValue = 4500, StarSystem = "Inara", Station = "Citi Gateway", MarketId = 3226573056})
                .GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"setCommanderStorageModules\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":[{\"itemName\":\"$hpt_pulselaser_gimbal_large_name\",\"itemValue\":350640,\"isHot\":true,\"starsystemName\":\"Sol\",\"stationName\":\"Abraham Lincoln\",\"marketID\":128016896,\"engineering\":{\"blueprintName\":\"Weapon_Overcharged\",\"blueprintLevel\":4,\"blueprintQuality\":0.0,\"experimentalEffect\":\"special_incendiary_rounds\"}},{\"itemName\":\"$int_cargorack_size7_class1_name\",\"itemValue\":12800,\"isHot\":false,\"starsystemName\":\"Sol\",\"stationName\":\"Abraham Lincoln\",\"marketID\":128016896},{\"itemName\":\"$int_dronecontrol_collection_size5_class5_name\",\"itemValue\":4500,\"starsystemName\":\"Inara\",\"stationName\":\"Citi Gateway\",\"marketID\":3226573056}]}]}", json);
        }

        [Fact]
        public void AddCommanderShip()
        {
            var json = _fixture.INARA.AddCommand(new AddCommanderShip("Sidewinder", 7)).GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"addCommanderShip\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"shipType\":\"Sidewinder\",\"shipGameID\":7}}]}", json);
        }

        [Fact]
        public void RemoveCommanderShip()
        {
            var json = _fixture.INARA.AddCommand(new RemoveCommanderShip("Sidewinder", 7)).GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"delCommanderShip\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"shipType\":\"Sidewinder\",\"shipGameID\":7}}]}", json);
        }

        [Fact]
        public void SetCommanderShip()
        {
            var json = _fixture.INARA.AddCommand(new SetCommanderShip("Sidewinder", 7){Name = "Goldie", Identifier = "OK-475", IsCurrent = true, IsMain = true, IsHot = false, HullValue = 35000, ModulesValue = 126540, RebuyCost = 15400, StarSystem = "Inara", Station = "Citi Gateway"}).GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"setCommanderShip\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"shipType\":\"Sidewinder\",\"shipGameID\":7,\"shipName\":\"Goldie\",\"shipIdent\":\"OK-475\",\"isCurrentShip\":true,\"isMainShip\":true,\"isHot\":false,\"shipHullValue\":35000,\"shipModulesValue\":126540,\"shipRebuyCost\":15400,\"starsystemName\":\"Inara\",\"stationName\":\"Citi Gateway\"}}]}", json);
        }

        [Fact]
        public void SetCommanderShipLoadout()
        {
            // TODO: Add loadout test
            //var json = _fixture.INARA.AddCommand(new SetCommanderShipLoadout("Federation_Corvette", 7, )).GetJson();
            //Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventName\":\"setCommanderShip\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"shipType\":\"Sidewinder\",\"shipGameID\":7,\"shipName\":\"Goldie\",\"shipIdent\":\"OK-475\",\"isCurrentShip\":true,\"isMainShip\":true,\"isHot\":false,\"shipHullValue\":35000,\"shipModulesValue\":126540,\"shipRebuyCost\":15400,\"starsystemName\":\"Inara\",\"stationName\":\"Citi Gateway\"}}]}", json);
        }

        [Fact]
        public void SetCommanderShipTransfer()
        {
            var json = _fixture.INARA.AddCommand(new SetCommanderShipTransfer("Sidewinder", 7, "Sol", "Abraham Lincoln") { TransferTime = 643 }).GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"setCommanderShipTransfer\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"shipType\":\"Sidewinder\",\"shipGameID\":7,\"starsystemName\":\"Sol\",\"stationName\":\"Abraham Lincoln\",\"transferTime\":643}}]}", json);
        }

        [Fact]
        public void AddCommanderTravelDock()
        {
            var json = _fixture.INARA.AddCommand(new AddCommanderTravelDock("Kokatese", "Koontz Enterprise"){ShipType = "Sidewinder", ShipId = 2}).GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"addCommanderTravelDock\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"starsystemName\":\"Kokatese\",\"stationName\":\"Koontz Enterprise\",\"shipType\":\"Sidewinder\",\"shipGameID\":2}}]}", json);
        }

        [Fact]
        public void AddCommanderTravelFsdJump()
        {
            var json = _fixture.INARA.AddCommand(new AddCommanderTravelFsdJump("Kokatese"){ShipType = "Sidewinder", ShipId = 2, JumpDistance = 10.28}).GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"addCommanderTravelFSDJump\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"starsystemName\":\"Kokatese\",\"jumpDistance\":10.28,\"shipType\":\"Sidewinder\",\"shipGameID\":2}}]}", json);
        }

        [Fact]
        public void SetCommanderTravelLocation()
        {
            var json = _fixture.INARA.AddCommand(new SetCommanderTravelLocation("Inara") { Station = "Citi Gateway" }).GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"setCommanderTravelLocation\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"starsystemName\":\"Inara\",\"stationName\":\"Citi Gateway\"}}]}", json);
        }

        [Fact]
        public void AddCommanderMission()
        {
            var json = _fixture.INARA.AddCommand(new AddCommanderMission("Mission_Massacre_Legal_Military", 170577201, "Inara")
            {
                Expiry = DateTime.Parse("2017-10-18T10:48:33Z"),
                InfluenceGain = "+++++",
                ReputationGain = "+++",
                OriginStation = "Koontz Port",
                OriginMinorFaction = "Inara Crimson Vision Network",
                TargetStarSystem = "HIP 15415",
                TargetStation = "Bisson Landing",
                TargetMinorFaction = "Social Warnones Resistance",
                TargetType = "$MissionUtil_FactionTag_Pirate;",
                KillCount = 16
            }).GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"addCommanderMission\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"missionName\":\"Mission_Massacre_Legal_Military\",\"missionGameID\":170577201,\"starsystemNameOrigin\":\"Inara\",\"missionExpiry\":\"2017-10-18T10:48:33Z\",\"influenceGain\":\"+++++\",\"reputationGain\":\"+++\",\"stationNameOrigin\":\"Koontz Port\",\"minorfactionNameOrigin\":\"Inara Crimson Vision Network\",\"starsystemNameTarget\":\"HIP 15415\",\"stationNameTarget\":\"Bisson Landing\",\"minorfactionNameTarget\":\"Social Warnones Resistance\",\"targetType\":\"$MissionUtil_FactionTag_Pirate;\",\"killCount\":16}}]}", json);
        }

        [Fact]
        public void SetCommanderMissionAbandoned()
        {
            var json = _fixture.INARA.AddCommand(new SetCommanderMissionAbandoned(171735583)).GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"setCommanderMissionAbandoned\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"missionGameID\":171735583}}]}", json);
        }

        [Fact]
        public void SetCommanderMissionCompleted()
        {
            var json = _fixture.INARA.AddCommand(new SetCommanderMissionCompleted(171735583)
            {
                RewardCredits = 12000,
                RewardPermits = new [] { new RewardPermit { StarSystem = "Alioth" }, new RewardPermit { StarSystem = "Polaris" } },
                RewardCommodities = new [] { new Commodity { Name = "NeofabricInsulation", Count = 4 }, new Commodity { Name = "Bromellite", Count = 2 } },
                RewardMaterials = new [] { new Commodity { Name = "Arsenic", Count = 2 } },
                FactionEffects = new[] { new FactionEffect { FactionName = "Inara Nexus", InfluenceGain = "++++", ReputationGain = "++" }, new FactionEffect { FactionName = "Inara Transport Inc", InfluenceGain = "+++", ReputationGain = "+" } }
            }).GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"setCommanderMissionCompleted\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"missionGameID\":171735583,\"rewardCredits\":12000,\"rewardPermits\":[{\"starsystemName\":\"Alioth\"},{\"starsystemName\":\"Polaris\"}],\"rewardCommodities\":[{\"itemName\":\"NeofabricInsulation\",\"itemCount\":4},{\"itemName\":\"Bromellite\",\"itemCount\":2}],\"rewardMaterials\":[{\"itemName\":\"Arsenic\",\"itemCount\":2}],\"minorfactionEffects\":[{\"minorfactionName\":\"Inara Nexus\",\"influenceGain\":\"++++\",\"reputationGain\":\"++\"},{\"minorfactionName\":\"Inara Transport Inc\",\"influenceGain\":\"+++\",\"reputationGain\":\"+\"}]}}]}", json);
        }

        [Fact]
        public void SetCommanderMissionFailed()
        {
            var json = _fixture.INARA.AddCommand(new SetCommanderMissionFailed(171735583)).GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"setCommanderMissionFailed\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"missionGameID\":171735583}}]}", json);
        }

        [Fact]
        public void AddCommanderCombatDeath()
        {
            var json = _fixture.INARA.AddCommand(new AddCommanderCombatDeath("Lave")
            {
                OpponentName = "$ShipName_MilitaryFighter_Federation;"
            }).GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"addCommanderCombatDeath\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"starsystemName\":\"Lave\",\"opponentName\":\"$ShipName_MilitaryFighter_Federation;\"}}]}", json);

            json = _fixture.INARA.AddCommand(new AddCommanderCombatDeath("Lave")
            {
                OpponentName = "Cmdr Mydlivoj Tydlitat",
                IsPlayer = true
            }).GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"addCommanderCombatDeath\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"starsystemName\":\"Lave\",\"opponentName\":\"Cmdr Mydlivoj Tydlitat\",\"isPlayer\":true}}]}", json);

            json = _fixture.INARA.AddCommand(new AddCommanderCombatDeath("Lave")
            {
                WingOpponentNames = new[] { "Cmdr Narkoleptik", "Cmdr Almara Potrhla", "Cmdr Jason Vyvoneny" }
            }).GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"addCommanderCombatDeath\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"starsystemName\":\"Lave\",\"wingOpponentNames\":[\"Cmdr Narkoleptik\",\"Cmdr Almara Potrhla\",\"Cmdr Jason Vyvoneny\"]}}]}", json);
        }

        [Fact]
        public void AddCommanderCombatInterdicted()
        {
            var json = _fixture.INARA.AddCommand(new AddCommanderCombatInterdicted("Lave", "White Rabbit Revenger", true) {IsSubmit = true}).GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"addCommanderCombatInterdicted\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"starsystemName\":\"Lave\",\"opponentName\":\"White Rabbit Revenger\",\"isPlayer\":true,\"isSubmit\":true}}]}", json);
        }

        [Fact]
        public void AddCommanderCombatInterdiction()
        {
            var json = _fixture.INARA.AddCommand(new AddCommanderCombatInterdiction("Lave", "Mad White Rabbit", true) {IsSuccess = true}).GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"addCommanderCombatInterdiction\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"starsystemName\":\"Lave\",\"opponentName\":\"Mad White Rabbit\",\"isPlayer\":true,\"isSuccess\":true}}]}", json);
        }

        [Fact]
        public void AddCommanderCombatInterdictionEscape()
        {
            var json = _fixture.INARA.AddCommand(new AddCommanderCombatInterdictionEscape("Lave", "White Rabbit Revenger", true)).GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"addCommanderCombatInterdictionEscape\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"starsystemName\":\"Lave\",\"opponentName\":\"White Rabbit Revenger\",\"isPlayer\":true}}]}", json);
        }

        [Fact]
        public void AddCommanderCombatKill()
        {
            var json = _fixture.INARA.AddCommand(new AddCommanderCombatKill("Lave", "Cmdr White Rabbit Revenger")).GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"addCommanderCombatKill\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"starsystemName\":\"Lave\",\"opponentName\":\"Cmdr White Rabbit Revenger\"}}]}", json);
        }

        [Fact]
        public void GetCommunityGoalsRecent()
        {
            var json = _fixture.INARA.AddCommand(new GetCommunityGoalsRecent()).GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"getCommunityGoalsRecent\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{}}]}", json);
        }

        [Fact]
        public void SetCommanderCommunityGoalProgress()
        {
            var json = _fixture.INARA.AddCommand(new SetCommanderCommunityGoalProgress(428,123500,75,600000){IsTopRank = false}).GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"setCommanderCommunityGoalProgress\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"communitygoalGameID\":428,\"contribution\":123500,\"percentileBand\":75,\"percentileBandReward\":600000,\"isTopRank\":false}}]}", json);
        }

        [Fact]
        public void SetCommunityGoal()
        {
            var json = _fixture.INARA.AddCommand(new SetCommunityGoal(428,"Conflict in Colonia - Colonia Council","Colonia","Jaques Station",DateTime.Parse("2017-10-29T18:00:00Z"))
            {
                TierReached = 2,
                TierMax = 8,
                TopRankSize = 10,
                IsCompleted = false,
                ContributorsCount = 1458,
                ContributionsTotal = 1568756
            }).GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"setCommunityGoal\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"communitygoalGameID\":428,\"communitygoalName\":\"Conflict in Colonia - Colonia Council\",\"starsystemName\":\"Colonia\",\"stationName\":\"Jaques Station\",\"goalExpiry\":\"2017-10-29T18:00:00Z\",\"tierReached\":2,\"tierMax\":8,\"topRankSize\":10,\"isCompleted\":false,\"contributorsNum\":1458,\"contributionsTotal\":1568756}}]}", json);
        }

        [Fact]
        public void MultiEvent()
        {
            var json = _fixture.INARA
                .AddCommand(new AddCommanderFriend("Vasya Pupkin", GamePlatform.PC))
                .AddCommand(new RemoveCommanderFriend("Vasya Pupkin", GamePlatform.PC))
                .AddCommand(new GetCommanderProfile("Vasya Pupkin"))
                .GetJson();
            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"addCommanderFriend\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"commanderName\":\"Vasya Pupkin\",\"gamePlatform\":\"pc\"}},{\"eventCustomID\":1,\"eventName\":\"delCommanderFriend\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"commanderName\":\"Vasya Pupkin\",\"gamePlatform\":\"pc\"}},{\"eventCustomID\":2,\"eventName\":\"getCommanderProfile\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"searchName\":\"Vasya Pupkin\"}}]}", json);

            json = _fixture.INARA
                .AddCommand(new AddCommanderInventoryCargoItem("conductivefabrics", 3))
                .AddCommand(new SetCommanderInventoryMaterials("phosphorus", 30))
                .AddCommand(new SetCommanderInventoryMaterials("nickel", 10))
                .AddCommand(new SetCommanderInventoryMaterials("crystalshards", 20))
                .GetJson();

            Assert.Equal("{\"header\":{\"appName\":\"your_application_name\",\"appVersion\":\"1.2.3\",\"isDeveloped\":true,\"APIkey\":\"your_apikey_from_inara\",\"commanderName\":\"Your Commander\",\"commanderFrontierID\":\"F1234567\"},\"events\":[{\"eventCustomID\":0,\"eventName\":\"addCommanderInventoryCargoItem\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":{\"itemName\":\"conductivefabrics\",\"itemCount\":3}},{\"eventCustomID\":1,\"eventName\":\"setCommanderInventoryMaterials\",\"eventTimestamp\":\"2019-09-02T17:00:00Z\",\"eventData\":[{\"itemName\":\"phosphorus\",\"itemCount\":30},{\"itemName\":\"nickel\",\"itemCount\":10},{\"itemName\":\"crystalshards\",\"itemCount\":20}]}]}", json);
        }
    }
}