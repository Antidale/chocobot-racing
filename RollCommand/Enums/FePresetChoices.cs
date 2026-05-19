using DSharpPlus.Commands.Processors.SlashCommands.ArgumentModifiers;

namespace chocobot_racing.RollCommand.Enums;

public enum FePresetChoices
{
    [ChoiceDisplayName("Paladin Cup")]
    PC,
    [ChoiceDisplayName("Kokkol Express")]
    KokkolExpress,
    [ChoiceDisplayName("Push B To Baron")]
    pb2b,

    [ChoiceDisplayName("Megaflare Rally")]
    CC50,
    [ChoiceDisplayName("Tsunami Open")]
    CC100,
    [ChoiceDisplayName("Zantetsuken Circuit")]
    CC150,

    [ChoiceDisplayName("Megaflare Rally (Whale)")]
    CC50W,
    [ChoiceDisplayName("Tsunami Open (Whale)")]
    CC100W,
    [ChoiceDisplayName("Zantetsuken Circuit (Whale)")]
    CC150W,

    //While the AFC tournament is going on, these stay up top
    [ChoiceDisplayName("Adamant Cup Experience")]
    ACE,
    [ChoiceDisplayName("Firebomb Fiesta")]
    FBF,
    [ChoiceDisplayName("Zemus Zone: Anthology")]
    ZZA,

    //Temporarily put the 5.0 presets high up in the list
    [ChoiceDisplayName("Sample Standard")]
    SampleStandard,
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

    //And the rest of the presets can be below
    [ChoiceDisplayName("Adamant Cup Group")]
    AdamantCupGroup,
    [ChoiceDisplayName("Adamant Cup Bracket")]
    AdamantCupBracket,
    [ChoiceDisplayName("Supermarket Sweep")]
    SupermarketSweep,
    [ChoiceDisplayName("Sumomo")]
    Sumomo,
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
    [ChoiceDisplayName("FuWario")]
    FuWario,
    [ChoiceDisplayName("Ladder Push B To Jump")]
    LadderPB2J,
    [ChoiceDisplayName("Pro-B-Otics")]
    ProBotics,
}
