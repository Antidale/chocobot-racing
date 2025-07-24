using DSharpPlus.Commands.Processors.SlashCommands.ArgumentModifiers;

namespace chocobot_racing.RollCommand.Enums;

public enum FePresetChoices
{
    [ChoiceDisplayName("Adamant Cup Experience")]
    ACE,
    [ChoiceDisplayName("Firebomb Fiesta")]
    FBF,
    [ChoiceDisplayName("Zemus Zone: Anthology")]
    ZZA,

    //Temporarily put the 5.0 presets high up in the list
    [ChoiceDisplayName("Intro to Kchar (5.0)")]
    KcharIntro,
    [ChoiceDisplayName("Plink Pony Club (5.0)")]
    PlinkPonyClub,
    [ChoiceDisplayName("Holding Out for a Hero (5.0)")]
    HeroHold,
    [ChoiceDisplayName("Dark Matter, 5.0 Edition (5.0)")]
    DarkMatter5,
    [ChoiceDisplayName("You Spoony Ninja (5.0)")]
    SpoonyNinja,
    [ChoiceDisplayName("Angry Bird (5.0)")]
    AngryBird,

    //rest of the default presets
    [ChoiceDisplayName("Sumomo")]
    Sumomo,

    [ChoiceDisplayName("Adamant Cup Group")]
    AdamantCupGroup,
    [ChoiceDisplayName("Adamant Cup Bracket")]
    AdamantCupBracket,
    [ChoiceDisplayName("Supermarket Sweep")]
    SupermarketSweep,
    [ChoiceDisplayName("Omnidexterous Memers Guild (OMG)")]
    OmnidexterousMemersGuild,
    [ChoiceDisplayName("Doorway to Tomorrow")]
    D2T,
    [ChoiceDisplayName("ZZ4")]
    ZZ4,
    [ChoiceDisplayName("ZZ6")]
    ZZ6,
    [ChoiceDisplayName("Blue Moon (ZZ5)")]
    ZZ5BlueMoon,
    [ChoiceDisplayName("EEL - Potion Party")]
    EELPotionParty,
    [ChoiceDisplayName("EEL - Moonveil Mixer")]
    EELMoonveilMixer,
    [ChoiceDisplayName("Pro-B-Otics")]
    ProBotics,
    [ChoiceDisplayName("FuWario")]
    FuWario,
    [ChoiceDisplayName("Ladder Push B To Jump")]
    LadderPB2J,
}
