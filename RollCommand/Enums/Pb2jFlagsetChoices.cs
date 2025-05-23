using DSharpPlus.Commands.Processors.SlashCommands.ArgumentModifiers;

namespace chocobot_racing.Enums
{
    public enum Pb2jFlagsetChoices
    {
        [ChoiceDisplayName("None")]
        None,
        [ChoiceDisplayName("Ladder PB2J")]
        Ladder,
        [ChoiceDisplayName("Hop 'Til You Shop")]
        HopTillYouShop,
        [ChoiceDisplayName("Pro B-otics")]
        ProBotics
    }

    // public class Pb2jFlagsetChoiceProvider : IChoiceProvider
    // {
    //     private static IReadOnlyDictionary<string, object> Options => 
    //         (IReadOnlyDictionary<string, object>)Enum.GetValues<Pb2jFlagsetChoices>()
    //             .ToDictionary(x => x.GetDescription(), x => x);

    //     public ValueTask<IReadOnlyDictionary<string, object>> ProvideAsync(CommandParameter commandParameter) => ValueTask.FromResult(Options);
    // }
}
