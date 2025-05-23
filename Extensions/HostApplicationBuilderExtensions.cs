using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace chocobot_racing.Extensions;

public static class HostApplicationBuilderExtensions
{
    public static HostApplicationBuilder ConfigureEnvironmentVariables(this HostApplicationBuilder builder)
    {
#if DEBUG
        builder.Configuration.AddEnvironmentVariables(prefix: "DB_CR_");
#else
        builder.Configuration.AddEnvironmentVariables(prefix: "CR_");
#endif

        builder.Configuration.AddEnvironmentVariables(prefix: "FE_GEN_");
        return builder;
    }
}
