using DSharpPlus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace chocobot_racing.Services;

public class DiscordBotService(ILogger<DiscordBotService> logger, DiscordClient client, IHostApplicationLifetime applicationLifetime) : IHostedService
{
    private readonly ILogger<DiscordBotService> Logger = logger;
    private readonly DiscordClient Client = client;
    private readonly IHostApplicationLifetime ApplicationLifetime = applicationLifetime;

    public async Task StartAsync(CancellationToken token)
    {
        await Client.ConnectAsync();
    }

    public async Task StopAsync(CancellationToken token)
    {
        await Client.DisconnectAsync();
    }

}
