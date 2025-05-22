using System;

namespace chocobot_racing.Extensions;

public static class StringExtensions
{
    public static bool HasContent(this string value) => !string.IsNullOrWhiteSpace(value);
}
