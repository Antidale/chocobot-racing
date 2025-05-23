using DSharpPlus.Commands.Processors.SlashCommands.ArgumentModifiers;

namespace chocobot_racing.RollCommand.Enums;

public enum FeHostedApi
{
#if DEBUG
    [ChoiceDisplayName("Local")]
    Local,
#endif

    [ChoiceDisplayName("Main")]
    Main,

    [ChoiceDisplayName("Galeswift")]
    Galeswift,
}
