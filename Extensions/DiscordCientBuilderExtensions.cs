using chocobot_racing.Constants;
using chocobot_racing.EventHandlers;
using DSharpPlus;
using DSharpPlus.Commands;
using DSharpPlus.Commands.Processors.SlashCommands;


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


            commands.CommandExecuted += CommandsEventHanlders.OnCommandInvokedAsync;

#if DEBUG

#else

#endif
        },
        commandsConfig);


        return builder;
    }

}
