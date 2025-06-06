namespace chocobot_racing.RacingCommands.Helpers;

public static class PermissionsHelper
{
    public static DiscordPermissions BasicPermissions => new DiscordPermissions().Add(DiscordPermission.ViewChannel, DiscordPermission.SendMessages);

    private static DiscordPermissions AdminPermissions => new DiscordPermissions().Add(DiscordPermission.ViewChannel, DiscordPermission.ManageChannels, DiscordPermission.SendMessages, DiscordPermission.ManageMessages);

    public static DiscordOverwriteBuilder GetDenyEveryonePermissionSet(DiscordRole role) => new DiscordOverwriteBuilder(role).Deny(BasicPermissions);

    public static DiscordOverwriteBuilder GetAllowBotAndAdminPermissionSet(DiscordRole role) => new DiscordOverwriteBuilder(role).Allow(AdminPermissions);

    public static DiscordOverwriteBuilder GetAllowUserPermissionSet(DiscordMember member) => new DiscordOverwriteBuilder(member).Allow(BasicPermissions);
}
