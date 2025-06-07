using System;

namespace chocobot_racing.Extensions;

public static class DiscordGuildExtensions
{
    public static DiscordRole? GetDiscordRole(this DiscordGuild guild, string roleName)
    {
        //TODO: make these .Replace calls into a regex or something to just compare on alphanumercs only, possibly?
        return guild?.Roles.FirstOrDefault(x =>
            x.Value.Name
                .Replace("-", string.Empty)
                .Replace("_", string.Empty)
                .Equals(roleName.Replace("-", string.Empty).Replace("_", string.Empty), StringComparison.InvariantCultureIgnoreCase))
            .Value;
    }

}
