using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;


namespace JoinHouses
{
    public class ConsoleCommands
    {
        [CommandLineFunctionality.CommandLineArgumentFunction("reloadconfig", "joinhouses")]
        private static string CommandReloadConfig(List<string> args)
        {
            SubModule.Config.LoadConfig();
            return "Config reloaded!";
        }

        [CommandLineFunctionality.CommandLineArgumentFunction("debug_marry", "joinhouses")]
        private static string DebugCommandMarry(List<string> args)
        {
            if (Hero.MainHero.Spouse != null)
                return "You are already married.";
            if (args.Count <= 0)
                return "Proper usage: joinhouses.debug_marry Rhagaea";
            string str1 = args[0];
            foreach (Hero aliveHero in (List<Hero>)Campaign.Current.AliveHeroes)
            {
                string str2 = aliveHero.Name.ToString().Replace(" ", "");
                if (str1 == str2)
                {
                    Hero.MainHero.Spouse = aliveHero;
                    aliveHero.SetNewOccupation((Occupation)3);
                    return str2 + " made your spouse!";
                }
            }
            return "No hero with that name found. If the name has spaces, try without spaces.";
        }
    }
}
