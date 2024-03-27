using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;

namespace JoinHouses.StaticUtils
{
    public static class Conversation
    {
        public static bool JoinHousesAllowed()
        {
            Hero conversationHero = Hero.OneToOneConversationHero;
            if (Hero.MainHero.Spouse != null || conversationHero.Spouse != null || Hero.MainHero.IsFemale == conversationHero.IsFemale)
                return false;
            Clan clan = conversationHero.Clan;
            return clan != null && clan.IsNoble && clan.Leader == conversationHero;
        }

        public static bool JoinHousesAccepted()
        {
            Hero conversationHero = Hero.OneToOneConversationHero;
            float valueDouble = (float)SubModule.Config.GetValueDouble("baseRelationNeeded");
            if (Hero.MainHero.IsFactionLeader)
                valueDouble += -40f;
            if (conversationHero.IsFactionLeader)
                valueDouble += 50f;
            float num = valueDouble + Hero.MainHero.Clan.Tier * -8 + conversationHero.Clan.Tier * 10;
            return (double)conversationHero.GetRelationWithPlayer() >= (double)num;
        }

        public static void JoinHousesHandle()
        {
            Clan clan = Hero.MainHero.Clan;
            Utils.JoinHouses(Hero.OneToOneConversationHero.Clan);
            MarriageAction.Apply(Hero.MainHero, Hero.OneToOneConversationHero, true);
            if (Hero.MainHero.PartyBelongedTo == null || Hero.MainHero.PartyBelongedTo.LeaderHero != Hero.MainHero)
                return;
            AddHeroToPartyAction.Apply(Hero.OneToOneConversationHero, Hero.MainHero.PartyBelongedTo, true);
        }
    }
}
