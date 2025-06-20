using chocobot_racing.RacingCommands.Helpers;
using DSharpPlus;
using DSharpPlus.EventArgs;

namespace chocobot_racing.EventHandlers;

public class ButtonEventHandlers
{
    public static async Task OnButtonClick(DiscordClient s, ComponentInteractionCreatedEventArgs e)
    {
        await e.Interaction.CreateResponseAsync(DiscordInteractionResponseType.DeferredMessageUpdate);

        var followUpBuilder = new DiscordFollowupMessageBuilder().AsEphemeral();

        var buttonData = e.Id.Split("|");
        if (e.Guild?.Id.ToString() != buttonData.First())
        {
            await e.Interaction.CreateFollowupMessageAsync(followUpBuilder.WithContent("invalid guild"));
            return;
        }

        if (!ulong.TryParse(buttonData.First(), out var guildId))
        {
            await e.Interaction.CreateFollowupMessageAsync(followUpBuilder.WithContent("invalid guild"));
            return;
        }

        if (!ulong.TryParse(buttonData.Skip(1).First(), out var channelId))
        {
            await e.Interaction.CreateFollowupMessageAsync(followUpBuilder.WithContent("invalid channel")); return;
        }

        var member = await e.Guild.GetMemberAsync(e.Interaction.User.Id);

        if (member is null)
        {
            await e.Interaction.CreateFollowupMessageAsync(followUpBuilder.WithContent("invalid guild"));
            return;
        }

        //Current: look up room and assign the user the rights to be there. Eventually this might be convered by button interactions?
        if (!e.Guild!.Channels.TryGetValue(channelId, out var potentialRoom))
        {
            await e.Interaction.CreateFollowupMessageAsync(followUpBuilder.WithContent("invalid channel"));
            return;
        }

        var overwrites = potentialRoom.PermissionOverwrites.Select(x => DiscordOverwriteBuilder.From(x)).Append(PermissionsHelper.GetAllowUserPermissionSet(member));

        await potentialRoom.ModifyAsync((channel) =>
        {
            channel.PermissionOverwrites = overwrites;
        });

        await e.Interaction.CreateFollowupMessageAsync(followUpBuilder
                .WithContent($"Join successful! {Formatter.Mention(potentialRoom)}")
                .AsEphemeral());

        if (buttonData.Last().Equals("join", StringComparison.InvariantCultureIgnoreCase))
        {
            await potentialRoom.SendMessageAsync($"{member.DisplayName} joined the race!");
        }

    }
}
