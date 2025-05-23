
using System.Reflection;
using chocobot_racing.RollCommand.Enums;
using DSharpPlus.Commands.Processors.SlashCommands.ArgumentModifiers;

namespace chocobot_racing.RollCommand.Providers;

public class PresetsAutoCompleteProvider : IAutoCompleteProvider
{
    static readonly DiscordAutoCompleteChoice[] choices;

    static PresetsAutoCompleteProvider()
    {
        List<DiscordAutoCompleteChoice> choiceList = [];
        foreach (FieldInfo fieldInfo in typeof(FePresetChoices).GetFields())
        {
            if (fieldInfo.IsSpecialName || !fieldInfo.IsStatic)
            {
                continue;
            }

            string displayName = fieldInfo.GetCustomAttribute<ChoiceDisplayNameAttribute>() is ChoiceDisplayNameAttribute displayNameAttribute
                ? displayNameAttribute.DisplayName
                : fieldInfo.Name;

            object? obj = fieldInfo.GetValue(null);
            if (obj is null || obj is not FePresetChoices || (int)obj < 0)
            {
                continue;
            }

            choiceList.Add(new DiscordAutoCompleteChoice(displayName, (int)obj));
        }

        choices = [.. choiceList];
    }

    public ValueTask<IEnumerable<DiscordAutoCompleteChoice>> AutoCompleteAsync(AutoCompleteContext context)
    {

        List<DiscordAutoCompleteChoice> results = [];
        foreach (DiscordAutoCompleteChoice choice in choices)
        {
            if (choice.Name.Contains(context.UserInput ?? "", StringComparison.OrdinalIgnoreCase))
            {
                results.Add(choice);
                if (results.Count == 25)
                {
                    break;
                }
            }
        }

        return ValueTask.FromResult<IEnumerable<DiscordAutoCompleteChoice>>(results);
    }
}
