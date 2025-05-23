using System;

namespace chocobot_racing.Extensions;

public static class StringExtensions
{
    public static bool HasContent(this string value) => !string.IsNullOrWhiteSpace(value);

    public static void ExitIfEmpty(this string value, string propertyName = "value")
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            Console.WriteLine($"{propertyName} not found. Check environment variables");
            Environment.Exit(exitCode: 1);
        }
    }
}
