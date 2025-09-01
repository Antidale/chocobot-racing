using chocobot_racing;
using chocobot_racing.Constants;
using chocobot_racing.EventHandlers;
using chocobot_racing.Helpers;
using chocobot_racing.Services;
using DSharpPlus;
using DSharpPlus.Extensions;
using FeInfo.Common.DTOs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

List<GuildConfiguration> guildConfigurations = [
  new(GuildIds.AntiServer, ChannelIds.AntiServerRaceAlertsId, 0, ChannelIds.AntiServerFakeAsyncCategory),
  new(GuildIds.FreeenWorkshop, ChannelIds.WorkshopRaceAlertsId, 0, ChannelIds.WorkshopRaceAlertsId)
];

var configHelper = new GuildConfigurationHelper(guildConfigurations);

var hostBuilder = Host.CreateApplicationBuilder()
                      .ConfigureEnvironmentVariables();

hostBuilder.Logging.AddConsole();

hostBuilder.Configuration.GetValueOrExit(ConfigKeys.FeInfoApiKey, out var apiKey)
                         .GetValueOrExit(ConfigKeys.FeInfoUrl, out var baseAddress)
                         .GetValueOrExit(ConfigKeys.DiscordToken, out var discordToken);


hostBuilder.Services.AddSingleton(service => new FeInfoHttpClient(apiKey, new Uri(baseAddress)))
                    .AddSingleton(service => new FeGenerationHttpClient())
                    .AddSingleton(service => new RacetimeHttpClient())
                    .AddSingleton(service => configHelper)
                    .AddHostedService<DiscordBotService>()
                    .AddDiscordClient(token: discordToken, intents: DiscordIntents.AllUnprivileged)
                    .AddCommands();

var app = hostBuilder.Build();
await app.RunAsync();
