using DSharpPlus.Commands.Processors.SlashCommands;
using DSharpPlus.Entities;

namespace chocobot_racing.Extensions;

public static class SlashCommandContextExtensions
{
    public static async ValueTask RespondAsync(this SlashCommandContext ctx, List<DiscordEmbed> embeds, bool ephemeral = false)
    {
        await ctx.RespondAsync(embeds.First(), ephemeral);

        var extraEmbeds = embeds.Skip(1).ToList();

        foreach (var embed in extraEmbeds)
        {
            await ctx.FollowupAsync(embed, ephemeral);
        }
    }
}
