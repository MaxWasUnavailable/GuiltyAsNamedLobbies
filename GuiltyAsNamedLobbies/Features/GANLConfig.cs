using BepInEx.Configuration;

namespace GuiltyAsNamedLobbies.Features;

/// <summary>
///     Configuration class for GuiltyAsNamedLobbies.
/// </summary>
// ReSharper disable once InconsistentNaming
public static class GANLConfig
{
    internal const string GeneralSection = "General";

    internal const string LobbyNameKey = "LobbyName";
    internal const string LobbyNameDefault = "";
    internal const string LobbyNameDescription = "The name of the lobby to create. If empty, vanilla name generation will be used.";

    internal static ConfigEntry<string> LobbyName { get; private set; } = null!;

    /// <summary>
    ///     Initializes the configuration entries.
    /// </summary>
    internal static void Init()
    {
        LobbyName = GuiltyAsNamedLobbies.Instance!.Config.Bind(GeneralSection, LobbyNameKey, LobbyNameDefault, LobbyNameDescription);
    }
}