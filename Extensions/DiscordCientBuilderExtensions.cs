using chocobot_racing.EventHandlers;
using chocobot_racing.RacingCommands;
using chocobot_racing.RollCommand;
using DSharpPlus;

namespace chocobot_racing.Extensions;

public static class DiscordConfiguration
{
    public static DiscordClientBuilder AddCommands(this DiscordClientBuilder builder)
    {
        var commandsConfig = new CommandsConfiguration
        {
            RegisterDefaultCommandProcessors = false
        };

        builder.UseCommands((ServiceProvider, commands) =>
        {
            commands.AddProcessor(new SlashCommandProcessor());
            commands.AddCommands<SeedRoller>();
            commands.AddCommands<CreateAsyncRace>();
            commands.AddCommands<CreateRacetimeRace>();

            commands.CommandExecuted += CommandsEventHanlders.OnCommandInvokedAsync;

#if DEBUG

#else

#endif
        },
        commandsConfig);


        return builder;
    }

}
