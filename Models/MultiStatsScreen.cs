using HarmonyLib;
using JumpKing;
using JumpKing.GameManager;
using JumpKing.GameManager.MultiEnding;
using JumpKing.Workshop;
using System.Text.RegularExpressions;

namespace JumpKing_MultiEndingStatsLevelName.Models
{
    [HarmonyPatch(typeof(StatsScreen))]
    internal class MultiStatsScreen
    {
        internal static string[] Tags = new string[0];
        internal static Regex MultiScreenEndingTitleRegex = new Regex("^(NewBabePlus|GhostOfTheBabe)Title=.+$");
        public MultiStatsScreen(Harmony harmony)
        {
            harmony.Patch(AccessTools.Method(typeof(StatsScreen), "GetEnding"), new HarmonyMethod(AccessTools.Method(GetType(), "Ending")), null);
        }

        public static bool Ending(ref string __result)
        {
            if (Game1.instance.contentManager.root != "Content")
            {
                Tags = XmlSerializerHelper.Deserialize<Level.LevelSettings>(Game1.instance.contentManager.root + "\\" + Level.FileName).Tags;
                if (Tags == null) return true;

                foreach (string tag in Tags)
                {
                    if (!MultiScreenEndingTitleRegex.IsMatch(tag))
                    {
                        continue;
                    }

                    string[] strings = tag.Split('=');
                    if (strings.Length == 2)
                    {
                        if (strings[0] == "NewBabePlusTitle" && GameEnding.GetEnding() == EndingType.NewBabePlus)
                        {
                            __result = strings[1];
                            return false;
                        }
                        else if (strings[0] == "GhostOfTheBabeTitle" && GameEnding.GetEnding() == EndingType.Ghost)
                        {
                            __result = strings[1];
                            return false;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }

            }

            return true;
        }
    }
}
