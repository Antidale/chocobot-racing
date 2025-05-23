using System;
using Microsoft.Extensions.Configuration;

namespace chocobot_racing.Extensions;

public static class ConfigurationManagerExtensions
{
    /// <summary>
    /// Method to verify and extract a config value that is required on startup. Do not use for later validation.
    /// </summary>
    /// <param name="config">The ConfigurationManager instance that's handling the application config</param>
    /// <param name="key">the key of the configu</param>
    /// <param name="output"></param>
    /// <returns>The configuration manager to continue operating on that object</returns>
    public static ConfigurationManager GetValueOrExit(this ConfigurationManager config, string key, out string output)
    {
        output = config.GetValue(key, string.Empty);

        output.ExitIfEmpty(key);

        return config;
    }
}
