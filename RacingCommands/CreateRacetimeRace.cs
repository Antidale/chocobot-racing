using System.ComponentModel;
using chocobot_racing.Constants;
using chocobot_racing.RacingCommands.Enums;
using chocobot_racing.RacingCommands.Helpers;
using DSharpPlus.Commands.Processors.SlashCommands.ArgumentModifiers;

namespace chocobot_racing.RacingCommands;

public class CreateRacetimeRace(RacetimeHttpClient client)
{
    [Command("CreateRace")]
    [Description("Creates a race at racetime.gg")]
    [RequireGuild]
    public async Task CreateRaceAsync(
        SlashCommandContext ctx,
        [Parameter("description")]
        [Description("a brief description of the race")]
        string description,
        [Parameter("goal")]
        [Description("rt.gg category")]
        RtggGoal goal,
        [Parameter("ping-alert-role")]
        [Description("include a ping to ping-to-race")]
        bool includePing = true
    )
    {
        await ctx.DeferResponseAsync(ephemeral: true);

        var alertsChannelId = ChannelIds.WorkshopRaceAlertsId;
        var restrictedGuildId = GuildIds.FreeenWorkshop;
        var urlBase = "https://racetime.gg";
#if DEBUG
        alertsChannelId = ChannelIds.AntiServerRaceAlertsId;
        restrictedGuildId = GuildIds.AntiServer;
        urlBase = "http://localhost:8000";
#endif

        if (ctx.Guild?.Id != restrictedGuildId)
        {
            await ctx.EditResponseAsync("Invalid Guild");
            return;
        }

        var response = await client.CreateRaceAsync(new()
        {
            Goal = goal.GetAttribute<ChoiceDisplayNameAttribute>()?.DisplayName ?? goal.ToString(),
            InfoUser = description,
        });

        if (response is null || !response.IsSuccessStatusCode)
        {
            await ctx.EditResponseAsync("Race creation failed. Try going directly to racetime: https://racetime.gg/ff4fe/startrace");
            return;
        }

        var raceUrl = GetRaceLocation(response, urlBase);
        await ctx.EditResponseAsync($"Race Created: {raceUrl}");

        if (!ctx.Guild!.Channels.TryGetValue(alertsChannelId, out var alertsChannel))
        {
            return;
        }

        var pingRole = ctx.Guild.GetDiscordRole("pingtorace");

        var alertMessage = AlertMessageHelper.CreateAlertMessage(ctx.Member!.DisplayName, description, raceUrl, pingRole, goal);

        await alertsChannel.SendMessageAsync(alertMessage);
    }

    [Command("create_afc_race")]
    [Description("Creates a race at racetime.gg")]
    [RequireGuild]
    public async Task CreateAfcRaceAsync(
        SlashCommandContext ctx,
        [Parameter("description")]
        [Description("a brief description of the race")]
        string description,
        [Parameter("flagset")]
        [Description("flagset")]
        AfcFlagset flagset,
        [Parameter("racer-one")]
        [Description("pings racer")]
        DiscordUser racerOne,
        [Parameter("racer-two")]
        [Description("pings racer")]
        DiscordUser racerTwo
    )
    {
        await ctx.DeferResponseAsync(ephemeral: true);

        var alertsChannelId = ChannelIds.WorkshopRaceAlertsId;
        var restrictedGuildId = GuildIds.FreeenWorkshop;
        var urlBase = "https://racetime.gg";
#if DEBUG
        alertsChannelId = ChannelIds.AntiServerRaceAlertsId;
        restrictedGuildId = GuildIds.AntiServer;
        urlBase = "http://localhost:8000";
#endif

        if (ctx.Guild?.Id != restrictedGuildId)
        {
            await ctx.EditResponseAsync("Invalid Guild");
            return;
        }

        var goal = "All Forked Cup team tournament";

        var response = await client.CreateRaceAsync(new()
        {
            Goal = goal,
            InfoUser = description,
        });

        if (response is null || !response.IsSuccessStatusCode)
        {
            await ctx.EditResponseAsync("Race creation failed. Try going directly to racetime: https://racetime.gg/ff4fe/startrace");
            return;
        }

        var raceUrl = GetRaceLocation(response, urlBase);
        await ctx.EditResponseAsync($"Race Created: {raceUrl}");

        if (!ctx.Guild!.Channels.TryGetValue(alertsChannelId, out var alertsChannel))
        {
            return;
        }

        var alertMessage = AlertMessageHelper.Create1v1AlertMessage(ctx, description, raceUrl, [racerOne, racerTwo], flagset);

        await alertsChannel.SendMessageAsync(alertMessage);
    }

    private static string GetRaceLocation(HttpResponseMessage response, string urlBase)
    {
        var locationHeader = response.Headers.FirstOrDefault(x => x.Key == "Location");
        return string.Join(string.Empty, urlBase, locationHeader.Value.First());
    }
}
