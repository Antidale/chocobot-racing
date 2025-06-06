using System.Collections.Concurrent;
using FeInfo.Common.DTOs;

namespace chocobot_racing.Helpers;

public class GuildConfigurationHelper
{
    public ConcurrentDictionary<ulong, GuildConfiguration> GuildConfigurations { get; private set; } = new();

    public GuildConfigurationHelper(List<GuildConfiguration> initialConfigurations)
    {
        initialConfigurations.ForEach(x => GuildConfigurations.AddOrUpdate(x.GuildId, x, (k, v) => x));
    }

    public GuildConfiguration AddOrUpdate(GuildConfiguration configuration) => GuildConfigurations.AddOrUpdate(configuration.GuildId, configuration, (k, v) => configuration);
}
