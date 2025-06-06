using chocobot_racing;
using chocobot_racing.Constants;
using chocobot_racing.Helpers;
using DSharpPlus;
using FeInfo.Common.DTOs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

List<GuildConfiguration> guildConfigurations = [new(GuildIds.AntiServer, ChannelIds.AntiServerRaceAlertsId, 0, ChannelIds.AntiServerFakeAsyncCategory), new(GuildIds.FreeenWorkshop, ChannelIds.WorkshopRaceAlertsId, 0, ChannelIds.WorkshopRaceAlertsId)];

var configHelper = new GuildConfigurationHelper(guildConfigurations);

var hostBuilder = Host.CreateApplicationBuilder()
                      .ConfigureEnvironmentVariables()
                      .Configuration
                        .GetValueOrExit(ConfigKeys.FeInfoApiKey, out var apiKey)
                        .GetValueOrExit(ConfigKeys.FeInfoUrl, out var baseAddress)
#if DEBUG
                        .GetValueOrExit(ConfigKeys.DiscordDebugToken, out var discordToken);
#else
                        .GetValueOrExit(ConfigKeys.DiscordToken);
#endif

var discordClient = DiscordClientBuilder
                .CreateDefault(token: discordToken, intents: DiscordIntents.AllUnprivileged)
                .ConfigureServices(a => a
                    .AddLogging(log => log.AddConsole())
                    .AddSingleton(service => new FeInfoHttpClient(apiKey, new Uri(baseAddress)))
                    .AddSingleton(service => new FeGenerationHttpClient())
                    .AddSingleton(service => new RacetimeHttpClient())
                    .AddSingleton(service => configHelper))
                .AddCommands()
                .Build();

await discordClient.ConnectAsync();

//TODO: swap to fully using host and .RunAsync()
await Task.Delay(-1);
