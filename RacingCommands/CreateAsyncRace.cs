using System.ComponentModel;
using System.Security.Cryptography;

namespace chocobot_racing.RacingCommands;

public class CreateAsyncRace
{

    private readonly string alphaNumerics = "abcdefghjkmnpqrstvwxyz1234567890";
    [Command("create-async")]
    [Description("creat an async race")]
    [RequireGuild]
    public async Task CreateAsyncRaceAsync(
        SlashCommandContext ctx
    )
    {
        await ctx.DeferResponseAsync(ephemeral: true);
        //check to see if we're near the limit of 
        if (!ctx.Guild!.Channels.TryGetValue(Constants.ChannelIds.AntiServerFakeAsyncCategory, out var category))
        {
            await ctx.RespondAsync("Coudn't find the parent");
            return;
        }


        if (category.Children.Count >= 48)
        {
            await ctx.RespondAsync("Too many races are open. Try again after at least one race has closed");
            return;
        }

        var everyoneRole = ctx.Guild!.EveryoneRole;
        var racebotAdminRoleSuccess = ctx.Guild!.Roles.TryGetValue(985344674652885044, out var racebotAdminRole);
        if (!racebotAdminRoleSuccess)
        {
            await ctx.RespondAsync("could not get racebot admin role");
            return;
        }

        var racebotRoleSuccess = ctx.Guild!.Roles.TryGetValue(1178819176320737394, out var racebotRole);

        if (!racebotRoleSuccess)
        {
            await ctx.RespondAsync("could not get racebot role");
            return;
        }

        var everyonePermissions = new DiscordPermissions().Add(DiscordPermission.ViewChannel, DiscordPermission.SendMessages);

        var adminPermissions = new DiscordPermissions().Add(DiscordPermission.ViewChannel, DiscordPermission.ManageChannels, DiscordPermission.SendMessages, DiscordPermission.ManageMessages);

        var stuff = new DiscordOverwriteBuilder(everyoneRole).Deny(everyonePermissions);

        var junk = new DiscordOverwriteBuilder(racebotRole!).Allow(adminPermissions);

        var wat = new DiscordOverwriteBuilder(racebotAdminRole!).Allow(adminPermissions);

        List<DiscordOverwriteBuilder> permissions = [stuff, junk, wat];

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

        //todo Log channels created to a database to be able to delete later
        await ctx.RespondAsync("channel created!");

    }

}
