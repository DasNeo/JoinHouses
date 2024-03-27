using JoinHouses.StaticUtils;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Conversation;


namespace JoinHouses
{
    public class JoinHousesCampaignBehavior : CampaignBehaviorBase
    {
        private CampaignGameStarter game;

        public JoinHousesCampaignBehavior(CampaignGameStarter game)
        {
            this.game = game;
            this.AddJoinHousesDialog();
        }

        public override void RegisterEvents()
        {
        }

        public override void SyncData(IDataStore dataStore)
        {
        }

        private void AddJoinHousesDialog()
        {
            this.game.AddPlayerLine("JoinHousesEventStart",
                "lord_talk_speak_diplomacy_2", 
                "JoinHousesEventStartOutput",
                "We should join our houses and consider marriage with each other.",
                new ConversationSentence.OnConditionDelegate(Conversation.JoinHousesAllowed),
                null);
            this.game.AddDialogLine("JoinHousesEventStartInterested", 
                "JoinHousesEventStartOutput", 
                "JoinHousesEventConfirm", 
                "I think that can be arranged.[rf:positive, rb:unsure]", 
                new ConversationSentence.OnConditionDelegate(Conversation.JoinHousesAccepted),
                null);
            this.game.AddDialogLine("JoinHousesEventStartNotInterested", 
                "JoinHousesEventStartOutput", 
                "lord_pretalk", 
                "I don't think so.[rf:very_negative_ag, rb:negative]",
                null,
                null);
            this.game.AddPlayerLine("JoinHousesEventConfirmYes", 
                "JoinHousesEventConfirm", 
                "JoinHousesEventGo", 
                "It's settled then.",
                null,
                null);
            this.game.AddPlayerLine("JoinHousesEventConfirmNo", 
                "JoinHousesEventConfirm", 
                "lord_pretalk",
                "Uh, nevermind.",
                null, 
                null);
            this.game.AddDialogLine("JoinHousesEventDoThatThang", 
                "JoinHousesEventGo", 
                "lord_pretalk", 
                "We should prepare immediately.[rf:positive, rb:positive]",
                null, 
                new ConversationSentence.OnConsequenceDelegate(Conversation.JoinHousesHandle));
        }
    }
}
