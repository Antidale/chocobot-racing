using System;
using chocobot_racing.Constants;
using chocobot_racing.EventHandlers;
using chocobot_racing.RacingCommands;
using chocobot_racing.RollCommand;
using Microsoft.Extensions.DependencyInjection;

namespace chocobot_racing.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommands(this IServiceCollection collection)
    {
        var commandsConfig = new CommandsConfiguration
        {
            RegisterDefaultCommandProcessors = false
        };

        collection.AddCommandsExtension((ServiceProvider, commands) =>
        {
            commands.AddProcessor(new SlashCommandProcessor());
            commands.AddCommands<FlagsetChooser>();
            commands.AddCommands<Recall>();
            commands.AddCommands<SeedRoller>();

            commands.CommandExecuted += CommandsEventHanlders.OnCommandInvokedAsync;

#if DEBUG
            commands.AddCommands<Tournament>(GuildIds.AntiServer);
            commands.AddCommands<TournamentAdministration>(GuildIds.AntiServer);
            commands.AddCommands<TournamentOverrides>(GuildIds.AntiServer);
            commands.AddCommands<CreateRacetimeRace>(GuildIds.AntiServer);
#else
            commands.AddCommands<Tournament>(GuildIds.AntiServer, GuildIds.SideTourneyServer);
            commands.AddCommands<TournamentAdministration>(GuildIds.AntiServer, GuildIds.SideTourneyServer);
            commands.AddCommands<TournamentOverrides>(GuildIds.AntiServer, GuildIds.SideTourneyServer);
            commands.AddCommands<CreateRacetimeRace>(GuildIds.FreeenWorkshop);
#endif
        },
        commandsConfig);

        return collection;
    }
}

internal class TournamentOverrides
{
}

internal class TournamentAdministration
{
}

internal class Tournament
{
}

internal class Recall
{
}