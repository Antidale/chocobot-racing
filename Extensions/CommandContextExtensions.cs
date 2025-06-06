using System.Diagnostics.CodeAnalysis;
using chocobot_racing.Constants;
using Microsoft.Extensions.Logging;

namespace chocobot_racing.Extensions
{
    public static class CommandContextExtensions
    {
        public static async Task<DiscordMessage?> GetMessageAsync(this CommandContext ctx, ulong channelId, ulong messageId)
        {
            try
            {
                if (ctx.Guild is not null)
                {
                    var channel = await ctx.Guild.GetChannelAsync(channelId);
                    return await channel.GetMessageAsync(messageId);
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public static async Task<DiscordMessage?> EditResponseAsync(this CommandContext ctx, string updatedMessage)
        {
            return await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent(updatedMessage));
        }

        public static async Task<DiscordMessage?> EditResponseAsync(this CommandContext ctx, DiscordEmbed embed)
        {
            return await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
        }

        public static async Task<DiscordMessage?> EditResponseAsync(this CommandContext ctx, List<DiscordEmbed> embeds)
        {
            var builder = new DiscordWebhookBuilder();
            embeds.ForEach(embed => builder.AddEmbed(embed));
            return await ctx.EditResponseAsync(builder);
        }

        [SuppressMessage("Usage", "CA2254:Template should be a static expression", Justification = "No structured logging use")]
        public static async Task LogErrorAsync(this CommandContext ctx, string message, Exception? ex = null)
        {
            if (ex != null)
            {
                ctx.Client.Logger.LogError(message: ex.ToString());
            }

            var guild = ctx.Client.Guilds[GuildIds.AntiServer];
            if (guild is null) { return; }

            var channel = guild.Channels[ChannelIds.AntiServerBotTestingChannelId];
            if (channel is null) { return; }

            //Odds are good we don't need the whole message here, and this feels like a good enough arbitrary number to know what's going on.
            if (message.Length > 1700)
            {
                message = message[..1700];
            }

            await channel.SendMessageAsync(string.Join("\r\n", ctx.User.Username, message, ex?.GetType()));
        }

        public static async Task LogErrorAsync(this CommandContext ctx, string responseMessage, string errorMessage, Exception? ex = null)
        {
            await ctx.EditResponseAsync(responseMessage);
            await LogErrorAsync(ctx, errorMessage, ex);
        }

        public static async Task LogUsageAsync(this CommandContext ctx)
        {
            var guild = ctx.Client.Guilds[GuildIds.AntiServer];
            if (guild is null) { return; }

            var channel = guild.Channels[ChannelIds.AntiServerBotTestingChannelId];
            if (channel is null) { return; }

            var invokingGuild = ctx.Guild;

            var guildString = invokingGuild is null
                ? "a DM"
                : $"{invokingGuild.Name}";

            var message = $"{ctx.Command.Name} was invoked in {guildString}";

            await channel.SendMessageAsync(message);
        }
    }
}
