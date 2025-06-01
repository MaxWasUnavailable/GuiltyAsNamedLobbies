using System.Collections.Generic;
using System.Reflection.Emit;
using GuiltyAsNamedLobbies.Features;
using HarmonyLib;
using Steamworks;

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
            .MatchForward(false, new CodeMatch(OpCodes.Call, AccessTools.Method(typeof(SteamFriends), nameof(SteamFriends.GetPersonaName))))
            .SetOperandAndAdvance(AccessTools.Method(typeof(SteamLobbyPatches), nameof(GetLobbyName)))
            .InstructionEnumeration();
    }

    private static string GetLobbyName()
    {
        var lobbyName = GANLConfig.LobbyName.Value;
        return string.IsNullOrEmpty(lobbyName) ? SteamFriends.GetPersonaName() : lobbyName;
    }
}