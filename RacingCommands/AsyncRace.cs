using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Security.Cryptography;
using chocobot_racing.Constants;
using chocobot_racing.Helpers;
using chocobot_racing.RacingCommands.Helpers;
using DSharpPlus;
using DSharpPlus.Commands.Processors.SlashCommands.Metadata;
using FeInfo.Common.Requests;

namespace chocobot_racing.RacingCommands;

[Command("async"), InteractionInstallType(DiscordApplicationIntegrationType.GuildInstall)]
[RequireGuild]
public class AsyncRace(GuildConfigurationHelper configurationHelper, FeInfoHttpClient feInfoHttpClient)
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
        ctx.Guild!.Roles.TryGetValue(985344674652885044, out var racebotAdminRole);
        if (racebotAdminRole is null)
        {
            await ctx.RespondAsync("could not get racebot admin role");
            return;
        }

        //TODO: use correct role, currently using Racingway's role in my test server
        var racebotRoleSuccess = ctx.Guild!.Roles.TryGetValue(1380349994053144708, out var racebotRole);
        if (racebotRole is null)
        {
            await ctx.RespondAsync("could not get racebot role");
            return;
        }

        var roomPermissions = PermissionsHelper.GetStandardRoomPermissions(everyoneRole, racebotRole, racebotAdminRole);

        var channelBaseName = $"ff4fe-{RandomNumberGenerator.GetString(alphaNumerics, 6)}-async";
        var raceChannel = await ctx.Guild!.CreateChannelAsync(
            name: channelBaseName,
            type: DiscordChannelType.Text,
            parent: category,
            overwrites: roomPermissions
        );

        var message = await raceChannel.SendMessageAsync("new race channel!");
        await message.PinAsync();

        var spoilerChannel = await ctx.Guild!.CreateChannelAsync(
            name: $"{channelBaseName}-spoilers",
            type: DiscordChannelType.Text,
            parent: category,
            overwrites: roomPermissions
        );

        var leaderboard = await spoilerChannel.SendMessageAsync("leaderboard post");
        await leaderboard.PinAsync();

        await ctx.RespondAsync($"shiny new channel: {Formatter.Mention(raceChannel)}");


        //TODO: fix ordering her so it makes sense as things are more finalized
        var alertMessageBuilder = AlertMessageHelper.CreateAsyncAlert(ctx.Member!.DisplayName, description, raceChannel.Id, ctx.Guild.Id);
        if (!ctx.Guild!.Channels.TryGetValue(alertsChannelId, out var alertsChannel))
        {
            return;
        }

        var alertMessage = await alertsChannel.SendMessageAsync(alertMessageBuilder);

        Dictionary<string, string> metadataDict = new()
        {
            [MetadataKeys.RoomId] = raceChannel.Id.ToString(),
            [MetadataKeys.SpoilerRoomId] = spoilerChannel.Id.ToString(),
            [MetadataKeys.AlertMessageId] = alertMessage.Id.ToString(),
            [MetadataKeys.LeaderboardMessageId] = leaderboard.Id.ToString()
        };

        var logCreatedRace = new CreateRaceRoom(ctx.User.Id.ToString(), channelBaseName, "FFA", "discord", metadataDict);

        var apiResponse = await feInfoHttpClient.PostAsJsonAsync("races", logCreatedRace);

        //todo: log info to appropriate internal cache object so we only go to the db for this stuff on startup.
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
