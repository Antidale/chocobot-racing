using chocobot_racing;
using chocobot_racing.Constants;
using DSharpPlus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var hostBuilder = Host.CreateApplicationBuilder();

#if DEBUG
hostBuilder.Configuration.AddEnvironmentVariables(prefix: "DB_CR_");
#else
hostBuilder.Configuration.AddEnvironmentVariables(prefix: "CR_");
#endif

var feInfoClient = new FeInfoHttpClient(
        apiKey: hostBuilder.Configuration.GetValue(ConfigKeys.FeInfoApiKey, string.Empty),
        baseAddress: hostBuilder.Configuration.GetValue(ConfigKeys.FeInfoUrl, string.Empty)
    );

var token = hostBuilder.Configuration.GetValue(ConfigKeys.BotToken, "");

if (string.IsNullOrWhiteSpace(token))
    throw new NullReferenceException($"{nameof(token)} is invalid. Check environment variables");

var discordClient = DiscordClientBuilder
                .CreateDefault(token: token, intents: DiscordIntents.AllUnprivileged)
                .ConfigureServices(a => a
                    .AddLogging(log => log.AddConsole())
                    .AddSingleton(service => feInfoClient)
                    .AddSingleton(service => new FeGenerationHttpClient()))
                .AddCommands()
                .Build();

await discordClient.ConnectAsync();

//TODO: swap to fully using host and .RunAsync()
await Task.Delay(-1);