using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;

namespace GuiltyAsNamedLobbies.Patches;

[HarmonyPatch(typeof(SteamLobby))]
[HarmonyPriority(Priority.First)]
[HarmonyWrapSafe]
internal static class SteamLobbyPatches
{
    [HarmonyTranspiler]
    [HarmonyPatch(nameof(SteamLobby.OnLobbyCreated))]
    private static IEnumerable<CodeInstruction> SteamLobbyTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        return new CodeMatcher(instructions)
            .MatchForward(false, new CodeMatch(OpCodes.Call, AccessTools.Method(typeof(Steamworks.SteamFriends), nameof(Steamworks.SteamFriends.GetPersonaName))))
            .SetOperandAndAdvance(AccessTools.Method(typeof(SteamLobbyPatches), nameof(GetLobbyName)))
            .InstructionEnumeration();
    }

    private static string GetLobbyName()
    {
        return ".1 This is a test name"; // TODO: Implement logic to read from config
    }
}