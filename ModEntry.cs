using HarmonyLib;
using JumpKing.Mods;
using JumpKing_MultiEndingStatsLevelName.Models;

namespace JumpKing_MultiEndingStatsLevelName
{
    [JumpKingMod("YutaGoto.JumpKing_MultiEndingStatsLevelName")]
    public static class ModEntry
    {
        internal static readonly string harmonyId = "YutaGoto.JumpKing_MultiEndingStatsLevelName";
        internal static Harmony harmony = new Harmony(harmonyId);

        [OnLevelStart]
        public static void OnLevelStart()
        {
            new MultiStatsScreen(harmony);
        }
    }
}
