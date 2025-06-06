using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using chocobot_racing.Helpers;
using chocobot_racing.RacingCommands.Helpers;
using DSharpPlus.Commands.Processors.SlashCommands.Metadata;

namespace chocobot_racing.RacingCommands;

[Command("async"), InteractionInstallType(DiscordApplicationIntegrationType.GuildInstall)]
[RequireGuild]
public class CreateAsyncRace(GuildConfigurationHelper configurationHelper)
{
    private readonly string alphaNumerics = "abcdefghjkmnpqrstvwxyz1234567890";
    [Command("create-async")]
    [Description("creat an async race")]
    [RequireGuild]
    public async Task CreateAsyncRaceAsync(
        SlashCommandContext ctx,
        [Parameter("description")]
        [Description("a brief description of the race")]
        [MaxLength(50)]
        string description
    )
    {
        await ctx.DeferResponseAsync(ephemeral: true);

        var guildId = ctx.Guild!.Id;
        configurationHelper.GuildConfigurations.TryGetValue(guildId, out var guildConfiguration);

        //todo: extract these checks out of this method
        //check to see if we're near the limit of 
        if (!ctx.Guild!.Channels.TryGetValue(guildConfiguration?.AsyncCategoryId ?? 0, out var category))
        {
            await ctx.RespondAsync("Guild incorrectly configured, requires setting valid async category");
            return;
        }

        if (category.Children.Count >= 48)
        {
            await ctx.RespondAsync("Too many races are open. Try again after at least one race has closed.");
            return;
        }

        //TODO: use configured role
        var everyoneRole = ctx.Guild!.EveryoneRole;
        var racebotAdminRoleSuccess = ctx.Guild!.Roles.TryGetValue(985344674652885044, out var racebotAdminRole);
        if (!racebotAdminRoleSuccess)
        {
            await ctx.RespondAsync("could not get racebot admin role");
            return;
        }

        //TODO: use correct role
        var racebotRoleSuccess = ctx.Guild!.Roles.TryGetValue(1178819176320737394, out var racebotRole);

        if (!racebotRoleSuccess)
        {
            await ctx.RespondAsync("could not get racebot role");
            return;
        }

        var everyonePermissions = PermissionsHelper.GetDenyEveryonePermissionSet(everyoneRole);
        var botPermissions = PermissionsHelper.GetAllowBotAndAdminPermissionSet(racebotAdminRole!);
        var adminPermissions = PermissionsHelper.GetAllowBotAndAdminPermissionSet(racebotAdminRole!);

        List<DiscordOverwriteBuilder> permissions = [everyonePermissions, botPermissions, adminPermissions];

        var channelBaseName = $"ff4fe-{RandomNumberGenerator.GetString(alphaNumerics, 6)}-async";
        var newChannel = await ctx.Guild!.CreateChannelAsync(
            name: channelBaseName,
            type: DiscordChannelType.Text,
            parent: category,
            overwrites: permissions
            );

        var spoilerChannel = await ctx.Guild!.CreateChannelAsync(
            name: $"{channelBaseName}-spoilers",
            type: DiscordChannelType.Text,
            parent: category,
            overwrites: permissions
            );

        //TODO: sent alert message, which requires moving AlterHelper out a bit, or creating one specifically for asyncs

        //todo Log channels created to a database to be able to delete later
        await ctx.RespondAsync("channel created!");
    }
}
