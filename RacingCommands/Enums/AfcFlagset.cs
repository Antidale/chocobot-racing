using chocobot_racing.Attributes;
using DSharpPlus.Commands.Processors.SlashCommands.ArgumentModifiers;

namespace chocobot_racing.RacingCommands.Enums;

public enum AfcFlagset
{
    [ChoiceDisplayName("Adamant Cup Experience")]
    [DiscordColor(0x125740)]
    Ace,
    [ChoiceDisplayName("Firebomb Fiesta")]
    [DiscordColor(0x002C5F)]
    Fbf,
    [ChoiceDisplayName("Zemus Zone: Anthology")]
    [DiscordColor(0xA71930)]
    Zza
}
