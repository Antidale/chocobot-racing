using DSharpPlus.Commands.Processors.SlashCommands.ArgumentModifiers;

namespace chocobot_racing.RacingCommands.Enums;

public enum RaceSettings
{
    [ChoiceDisplayName("Strict (streaming required)")]
    Strict,
    [ChoiceDisplayName("Casual (no streaming required)")]
    Casual,
}
