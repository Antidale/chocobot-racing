using chocobot_racing.Attributes;
using DSharpPlus.Commands.Processors.SlashCommands.ArgumentModifiers;

namespace chocobot_racing.RacingCommands.Enums;

public enum RtggGoal
{
    [ChoiceDisplayName("Beat Zeromus")]
    [DiscordColor(0xCE1141)]
    BeatZeromus,
    [ChoiceDisplayName("Complete Objectives")]
    [DiscordColor(0xff6baa)]
    WinGame,
    [ChoiceDisplayName("Door Rando - Season -1")]
    [DiscordColor(0x006BB6)]
    DoorRando,
    [ChoiceDisplayName("5.0 alpha/beta")]
    [DiscordColor(0x7332a8)]
    Alpha,
    [ChoiceDisplayName("Entrance Rando - Season -1")]
    [DiscordColor(0x674736)]
    EntranceRando,
}
