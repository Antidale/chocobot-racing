using chocobot_racing;
using chocobot_racing.Constants;
using DSharpPlus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var hostBuilder = Host.CreateApplicationBuilder()
                      .ConfigureEnvironmentVariables()
                      .Configuration
                        .GetValueOrExit(ConfigKeys.FeInfoApiKey, out var apiKey)
                        .GetValueOrExit(ConfigKeys.FeInfoUrl, out var baseAddress)
                        .GetValueOrExit(ConfigKeys.DiscordToken, out var discordToken);

var discordClient = DiscordClientBuilder
                .CreateDefault(token: discordToken, intents: DiscordIntents.AllUnprivileged)
                .ConfigureServices(a => a
                    .AddLogging(log => log.AddConsole())
                    .AddSingleton(service => new FeInfoHttpClient(apiKey, new Uri(baseAddress)))
                    .AddSingleton(service => new FeGenerationHttpClient()))
                .AddCommands()
                .Build();

await discordClient.ConnectAsync();

//TODO: swap to fully using host and .RunAsync()
await Task.Delay(-1);
