using System.ComponentModel;
using System.Net.Http.Json;
using chocobot_racing.Constants;
using chocobot_racing.RacingCommands.Enums;
using chocobot_racing.RacingCommands.Helpers;
using DSharpPlus.Commands.Processors.SlashCommands.ArgumentModifiers;
using FeInfo.Common.Requests;

namespace chocobot_racing.RacingCommands;

public class CreateRacetimeRace(RacetimeHttpClient client, FeInfoHttpClient feInfoHttpClient)
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
        [Parameter("settings")]
        [Description("strict or casual settings")]
        RaceSettings settings,
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
            StreamingRequired = settings == RaceSettings.Strict,
            InfoUser = description,
        });

        if (response is null || !response.IsSuccessStatusCode)
        {
            await ctx.EditResponseAsync("Race creation failed. Try going directly to racetime: https://racetime.gg/ff4fe/startrace");
            return;
        }

        var raceUrl = GetFullRaceUrl(response, urlBase);
        await ctx.EditResponseAsync($"Race Created: {raceUrl}");

        if (!ctx.Guild!.Channels.TryGetValue(alertsChannelId, out var alertsChannel))
        {
            return;
        }

        var pingRole = ctx.Guild.GetDiscordRole("pingtorace");

        var alertMessage = AlertMessageHelper.CreateAlertMessage(ctx.Member!.DisplayName, description, raceUrl, pingRole, goal, settings);

        await alertsChannel.SendMessageAsync(alertMessage);
        var goalString = goal.GetAttribute<ChoiceDisplayNameAttribute>()?.DisplayName ?? goal.ToString();
        await LogRaceCreated(response, goalString, description, ctx);
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

        var raceUrl = GetFullRaceUrl(response, urlBase);
        await ctx.EditResponseAsync($"Race Created: {raceUrl}");

        if (!ctx.Guild!.Channels.TryGetValue(alertsChannelId, out var alertsChannel))
        {
            return;
        }

        var alertMessage = AlertMessageHelper.Create1v1AlertMessage(ctx, description, raceUrl, [racerOne, racerTwo], flagset);

        await alertsChannel.SendMessageAsync(alertMessage);
        await LogRaceCreated(response, goal, description, ctx);
    }

    private static string GetRaceLocation(HttpResponseMessage response)
    {
        return response.Headers.FirstOrDefault(x => x.Key == "Location").Value.First() ?? "";
    }

    private static string GetFullRaceUrl(HttpResponseMessage response, string urlBase)
    {
        var locationHeader = response.Headers.FirstOrDefault(x => x.Key == "Location");
        return string.Join(string.Empty, urlBase, locationHeader.Value.First());
    }

    private async Task LogRaceCreated(HttpResponseMessage response, string goal, string description, SlashCommandContext ctx)
    {
        try
        {
            var shortRaceName = GetRaceLocation(response);
            var metadataDict = new Dictionary<string, string>
            {
                ["Goal"] = goal,
                ["Description"] = description
            };

            if (shortRaceName.StartsWith("/"))
            {
                shortRaceName = string.Join("", shortRaceName.Skip(1));
            }

            var logCreatedRace = new CreateRaceRoom(ctx.User.Id.ToString(), shortRaceName, "FFA", "Racetime.gg", metadataDict);

            var apiResponse = await feInfoHttpClient.PostAsJsonAsync("races", logCreatedRace);

        }
        catch (Exception)
        {
            //don't care currently
        }
    }
}
