using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Threading.Channels;
using chocobot_racing.Constants;
using chocobot_racing.Helpers;
using chocobot_racing.RacingCommands.Helpers;
using DSharpPlus.Commands.Processors.SlashCommands.Metadata;

namespace chocobot_racing.RacingCommands;

[Command("async"), InteractionInstallType(DiscordApplicationIntegrationType.GuildInstall)]
[RequireGuild]
public class AsyncRace(GuildConfigurationHelper configurationHelper)
{
    private readonly string alphaNumerics = "abcdefghjkmnpqrstvwxyz1234567890";
    [Command("create-async")]
    [Description("creat an async race")]
    public async Task CreateAsyncRaceAsync(
        SlashCommandContext ctx,
        [Parameter("description")]
        [Description("a brief description of the race")]
        [MaxLength(50)]
        string description
    )
    {
        await ctx.DeferResponseAsync(ephemeral: true);

        var alertsChannelId = ChannelIds.WorkshopRaceAlertsId;

#if DEBUG
        alertsChannelId = ChannelIds.AntiServerRaceAlertsId;
#endif

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

        //TODO: use correct role, currently using Racingway's role in my test server
        var racebotRoleSuccess = ctx.Guild!.Roles.TryGetValue(1380349994053144708, out var racebotRole);

        if (!racebotRoleSuccess)
        {
            await ctx.RespondAsync("could not get racebot role");
            return;
        }

        var everyonePermissions = PermissionsHelper.GetDenyEveryonePermissionSet(everyoneRole);
        var botPermissions = PermissionsHelper.GetAllowBotAndAdminPermissionSet(racebotRole!);
        var adminPermissions = PermissionsHelper.GetAllowBotAndAdminPermissionSet(racebotAdminRole!);

        List<DiscordOverwriteBuilder> permissions = [everyonePermissions, botPermissions, adminPermissions];

        var channelBaseName = $"ff4fe-{RandomNumberGenerator.GetString(alphaNumerics, 6)}-async";
        var newChannel = await ctx.Guild!.CreateChannelAsync(
            name: channelBaseName,
            type: DiscordChannelType.Text,
            parent: category,
            overwrites: permissions
            );

        var message = await newChannel.SendMessageAsync("new race channel!");

        await message.PinAsync();

        var spoilerChannel = await ctx.Guild!.CreateChannelAsync(
            name: $"{channelBaseName}-spoilers",
            type: DiscordChannelType.Text,
            parent: category,
            overwrites: permissions
            );

        var leaderboard = await spoilerChannel.SendMessageAsync("leaderboard post");
        await leaderboard.PinAsync();
        //TODO: store this message id so that we can update the leaderboard as people finish

        //TODO: sent alert message, which requires moving AlterHelper out a bit, or creating one specifically for asyncs

        //todo Log channels created to a database to be able to delete later
        await ctx.RespondAsync("channel created!");

        var alertMessage = AlertMessageHelper.CreateAsyncAlert(ctx.Member!.DisplayName, description, newChannel.Id, ctx.Guild.Id);
        if (!ctx.Guild!.Channels.TryGetValue(alertsChannelId, out var alertsChannel))
        {
            return;
        }

        await alertsChannel.SendMessageAsync(alertMessage);
    }

    [Command("join")]
    [Description("join an async race")]
    public async Task JoinAsync(SlashCommandContext ctx,
    [Parameter("race-room")]
    [Description("a specific race to join")]
    string roomName)
    {
        await ctx.DeferResponseAsync(ephemeral: true);

        //Current: look up room and assign the user the rights to be there. Eventually this might be convered by button interactions?
        var potentialRoom = ctx.Guild!.Channels.FirstOrDefault(x => x.Value.Name.Equals(roomName, StringComparison.InvariantCultureIgnoreCase)).Value;

        if (potentialRoom is null)
        {
            await ctx.EditResponseAsync($"Could not find room {roomName} in this server");
            return;
        }

        var member = ctx.Member;
        if (member is null)
        {
            await ctx.EditResponseAsync("You must be a member of the server to join asyncs there");
            return;
        }

        //TODO: see if the person is already in the race, if they are, let them know but don't do anything else.


        var overwrites = potentialRoom.PermissionOverwrites.Select(x => DiscordOverwriteBuilder.From(x)).Append(PermissionsHelper.GetAllowUserPermissionSet(member));

        await potentialRoom.ModifyAsync((channel) =>
        {
            channel.PermissionOverwrites = overwrites;
        });


        //TODO: Log user joining room to api and/or local db. Make sure we have enough info to send them a DM later when the room closes
        await ctx.RespondAsync($"Join successful! <#{potentialRoom.Id}>");

        await potentialRoom.SendMessageAsync($"{member.DisplayName} joined the race!");

    }
}
