using HarmonyLib;
using SandBox.CampaignBehaviors;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.MapNotificationTypes;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace JoinHouses.Patches
{
    [HarmonyPatch(typeof(DefaultCutscenesCampaignBehavior), "OnHeroComesOfAge")]
    internal class HeroComesOfAgeCutscenePatch
    {
        [HarmonyPrefix]
        private static bool Prefix(Hero hero)
        {
            if (hero.Father != null && hero.Mother != null || hero.Clan != Hero.MainHero.Clan)
                return true;
            TextObject textObject = new TextObject("{=t4KwQOB7}{HERO.NAME} is now of age.", null);
            TextObjectExtensions.SetCharacterProperties(textObject, "HERO", hero.CharacterObject, false);
            Campaign.Current.CampaignInformationManager.NewMapNoticeAdded(new HeirComeOfAgeMapNotification(hero, hero.Clan.Leader, textObject, CampaignTime.Now));
            return false;
        }
    }
}
