using chocobot_racing.Attributes;
using chocobot_racing.RacingCommands.Enums;
using DSharpPlus.Commands.Processors.SlashCommands.ArgumentModifiers;

namespace chocobot_racing.RacingCommands.Helpers;

public class AlertMessageHelper
{
    public static DiscordMessageBuilder CreateAlertMessage(string memberDisplayName, string description, string raceUrl, DiscordRole? role, RtggGoal goal, RaceSettings raceSettings)
    {
        var raceDetailsText =
@$"**Goal**: {goal.GetAttribute<ChoiceDisplayNameAttribute>()?.DisplayName ?? goal.ToString()}
**Settings**: {raceSettings.GetAttribute<ChoiceDisplayNameAttribute>()?.DisplayName ?? raceSettings.ToString()}
**URL**: {raceUrl}
-# **Created by**: {memberDisplayName}";

        var messageBuilder = new DiscordMessageBuilder()
            .EnableV2Components()
            .AddContainerComponent(new DiscordContainerComponent(
                [
                    new DiscordTextDisplayComponent($"### {description}"),
                    new DiscordTextDisplayComponent(raceDetailsText)
                ],
                color: goal.GetAttribute<DiscordColorAttribute>()?.Color
            ));

        AddRolePing(role, messageBuilder);

        return messageBuilder;
    }

    public static DiscordMessageBuilder Create1v1AlertMessage(SlashCommandContext ctx, string description, string raceUrl, List<DiscordUser> pingUsers, AfcFlagset flagset)
    {
        var mentions = string.Join(" ", pingUsers.Select(x => x.Mention));

        var flagsetName = flagset.GetAttribute<ChoiceDisplayNameAttribute>()?.DisplayName ?? flagset.GetDescription();

        var raceDescriptionText = $"### {description}";
        var receDetailText = @$"**URL**: {raceUrl}
**Flagset**: `{flagsetName}`
**Racers**: {mentions}
-# **Created by**: {ctx.Member!.DisplayName}";

        var messageBuilder = new DiscordMessageBuilder()
            .EnableV2Components()
            .AddContainerComponent(
                new DiscordContainerComponent(
                components:
                [
                    new DiscordTextDisplayComponent(raceDescriptionText),
                    new DiscordTextDisplayComponent(receDetailText)
                ],
                color: flagset.GetAttribute<DiscordColorAttribute>()?.Color)
            );

        pingUsers.ForEach(user =>
        {
            messageBuilder.WithAllowedMention(new UserMention(user));
        });

        return messageBuilder;
    }

    public static DiscordMessageBuilder CreateAsyncAlert(string memberDisplayName, string roomName, ulong raceRoomId, ulong guildId, int entrantCount = 0, string status = "open", string flags = "", string link = "")
    {
        //TODO buid this message better, and possibly take in an object encapsulating all this stuff rather than the huge mess of params?
        var raceDetailsText = @$"### {roomName}
**Flags**: To Be Rolled
**Link**: To be Rolled
**Status**: {status}
**Entrants**: {entrantCount}
**Created By**: {memberDisplayName}";

        var buttonBase = $"{guildId}|{raceRoomId}|";
        var joinButton = new DiscordButtonComponent(DiscordButtonStyle.Primary, $"{buttonBase}join", "join");
        var watchButton = new DiscordButtonComponent(DiscordButtonStyle.Secondary, $"{buttonBase}watch", "watch");

        var messageBuilder = new DiscordMessageBuilder()
            .EnableV2Components()
            .AddContainerComponent(new DiscordContainerComponent(
                [
                    new DiscordTextDisplayComponent(raceDetailsText),
                    new DiscordActionRowComponent([joinButton, watchButton])
                ],
                color: DiscordColor.SapGreen
            ));

        return messageBuilder;
    }

    private static void AddRolePing(DiscordRole? role, DiscordMessageBuilder messageBuilder)
    {
        if (role is not null)
        {
            messageBuilder.AddTextDisplayComponent(new DiscordTextDisplayComponent($"{role.Mention}"));
            messageBuilder.WithAllowedMention(new RoleMention(role));
        }
    }
}
