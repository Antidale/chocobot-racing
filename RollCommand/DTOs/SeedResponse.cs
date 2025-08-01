using System.Text.RegularExpressions;

namespace chocobot_racing.RollCommand.DTOs;

public partial class SeedResponse : FeApiResponse
{
    public string Version { get; set; } = string.Empty;
    public string Seed { get; set; } = string.Empty;
    public string Flags { get; set; } = string.Empty;
    public string Verification { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;

    public string BinaryFlags => UrlFlagsRegex().Matches(Url).FirstOrDefault()?.Groups.Values.LastOrDefault()?.Value ?? "";

    public List<DiscordMessageBuilder> ToMessageBuilders(string flags, string? seed)
    {
        var embedList = new List<DiscordMessageBuilder>() { ToMessageBuilder(seed) };
        var verificationEmbed = ToFlagsVerificationMessageBuilder(flags);
        if (verificationEmbed is not null)
        {
            embedList.Add(verificationEmbed);
        }
        return embedList;
    }

    private DiscordMessageBuilder ToMessageBuilder(string? seed)
    {
        var seedLabel = seed is null ? "Seed" : "Provided Seed";

        return new DiscordMessageBuilder().EnableV2Components().AddContainerComponent
        (
            new DiscordContainerComponent(
            components:
            [
                new DiscordTextDisplayComponent($"### [Requested Seed](<{Url}>)"),
                new DiscordTextDisplayComponent($"```\r\n{Flags}\r\n```"),
                new DiscordTextDisplayComponent($"**URL**: {Url}"),
                new DiscordTextDisplayComponent($"**Hash**: {Verification}"),
                new DiscordTextDisplayComponent($"**{seedLabel}**: {(seed is null ? Seed : seed)}")
            ],
            color: GetDiscordColor())
        );
    }

    private DiscordMessageBuilder? ToFlagsVerificationMessageBuilder(string flags)
    {
        return VerifyFlags(flags)
        ? null
        : new DiscordMessageBuilder().EnableV2Components().AddContainerComponent
        (
            new DiscordContainerComponent
            (
                components:
                [
                    new DiscordTextDisplayComponent($"### Flags Mismatch\r\n{FlagsVerificationEmbedDescription(flags)}")
                ],
                color: DiscordColor.Red
            )
        );
    }

    private string FlagsVerificationEmbedDescription(string desiredFlags)
    {
        var comparisonFlags = desiredFlags.Contains(' ')
            ? Flags
            : BinaryFlags;
        return $@"The seed above has a flag mismatch between the requested and rolled flags

**Requested Flags**
```
{desiredFlags}
```

**Returned Flags**
```
{comparisonFlags}
```
";
    }

    private bool VerifyFlags(string flags)
    {
        if (flags.Contains(' '))
        {
            return string.Equals(Flags.SortFlags(), flags.SortFlags(), StringComparison.InvariantCultureIgnoreCase);
        }

        var matches = UrlFlagsRegex().Match(Url);
        if (!matches.Success)
        {
            return false;
        }

        var binaryFlags = matches.Groups[1].Captures.FirstOrDefault()?.Value ?? "";
        return binaryFlags.Equals(flags, StringComparison.InvariantCultureIgnoreCase);
    }

    private DiscordColor GetDiscordColor()
    {
        return Url switch
        {
            var galeswift when galeswift.Contains("galeswift") => DiscordColor.CornflowerBlue,
            var alpha when alpha.Contains("alpha") => DiscordColor.Purple,
            _ => DiscordColor.DarkBlue
        };
    }


    [GeneratedRegex(@"=(\w+)\.", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex UrlFlagsRegex();
}
