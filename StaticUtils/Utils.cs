using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Library;


namespace JoinHouses.StaticUtils
{
    public static class Utils
    {
        public static void PrintToMessages(string str, float r = 255f, float g = 255f, float b = 255f)
        {
            float[] numArray = new float[3]
            {
                r /  byte.MaxValue,
                g /  byte.MaxValue,
                b /  byte.MaxValue
            };
            Color color = new Color(numArray[0], numArray[1], numArray[2], 1f);
            InformationManager.DisplayMessage(new InformationMessage(str, color));
        }

        public static void JoinHouses(Clan otherClan)
        {
            List<Clan> clanList = (List<Clan>)AccessTools.Field(typeof(CampaignObjectManager), "_clans").GetValue(Campaign.Current.CampaignObjectManager);
            List<IFaction> ifactionList = (List<IFaction>)AccessTools.Field(typeof(CampaignObjectManager), "_factions").GetValue(Campaign.Current.CampaignObjectManager);
            Clan clan1 = Hero.MainHero.Clan;
            Kingdom kingdom1 = clan1.Kingdom;
            Kingdom kingdom2 = otherClan.Kingdom;
            GiveGoldAction.ApplyBetweenCharacters(otherClan.Leader, Hero.MainHero, otherClan.Gold, true);
            GainRenownAction.Apply(Hero.MainHero, otherClan.Renown, true);
            foreach (Town town in otherClan.Fiefs.ToList<Town>())
                town.OwnerClan = clan1;
            foreach (Hero hero in otherClan.Heroes.ToList<Hero>())
                hero.Clan = clan1;
            if (kingdom2 != null)
            {
                if (kingdom1 == null)
                {
                    if (kingdom2.RulingClan == otherClan)
                        kingdom2.RulingClan = clan1;
                    clan1.Kingdom = kingdom2;
                }
                else if (kingdom1 != null && kingdom1.RulingClan != clan1 && kingdom2.RulingClan == otherClan)
                {
                    kingdom2.RulingClan = clan1;
                    clan1.Kingdom = kingdom2;
                }
            }
            Utils.PrintToMessages(((object)otherClan.Name)?.ToString() + " has become one with " + ((object)clan1.Name)?.ToString() + "!", 102f, b: 102f);
            DestroyClanAction.ApplyByClanLeaderDeath(otherClan);
            clanList.Remove(otherClan);
            ifactionList.Remove(otherClan);
            if (kingdom1 == null || kingdom1.RulingClan != clan1 || kingdom2 == null || kingdom2.MapFaction.Leader != Hero.OneToOneConversationHero)
                return;
            List<Kingdom> kingdomList = (List<Kingdom>)AccessTools.Field(typeof(CampaignObjectManager), "_kingdoms").GetValue(Campaign.Current.CampaignObjectManager);
            foreach (Clan clan2 in kingdom2.Clans.ToList<Clan>())
                ChangeKingdomAction.ApplyByJoinToKingdom(clan2, kingdom1, false);
            Utils.PrintToMessages(((object)kingdom2.Name)?.ToString() + " has become one with " + ((object)kingdom1.Name)?.ToString() + "!", 102f, b: 102f);
            DestroyKingdomAction.Apply(kingdom2);
            kingdomList.Remove(kingdom2);
            ifactionList.Remove(kingdom2);
        }
    }
}
