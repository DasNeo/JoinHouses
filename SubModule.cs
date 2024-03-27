using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace JoinHouses
{
    public class SubModule : MBSubModuleBase
    {
        public static Config Config = new Config();

        protected override void OnSubModuleLoad() => new Harmony("JoinHouses").PatchAll();

        protected override void OnGameStart(Game game, IGameStarter gameStarter)
        {
            base.OnGameStart(game, gameStarter);
            if (!(game.GameType is Campaign))
                return;
            CampaignGameStarter game1 = (CampaignGameStarter)gameStarter;
            game1.AddBehavior(new JoinHousesCampaignBehavior(game1));
        }
    }
}
