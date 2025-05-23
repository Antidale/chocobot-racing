using System.Text.RegularExpressions;

namespace chocobot_racing.RollCommand.DTOs;

public partial class SeedResponse : FeApiResponse
{
    public string Version { get; set; } = string.Empty;
    public string Seed { get; set; } = string.Empty;
    public string Flags { get; set; } = string.Empty;
    public string Verification { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;

    public string BinaryFlags => UrlFlagsRegex().Matches(Url).FirstOrDefault()?.Captures.FirstOrDefault()?.Value ?? "";

    public List<DiscordEmbed> ToEmbedList(string flags, string? seed)
    {
        var embedList = new List<DiscordEmbed>() { ToEmbed(seed) };
        var verificationEmbed = ToFlagsVerificationEmbed(flags);
        if (verificationEmbed is not null)
        {
            embedList.Add(verificationEmbed);
        }
        return embedList;
    }

    private DiscordEmbed ToEmbed(string? seed)
    {
        var builder = new DiscordEmbedBuilder()
            .WithTitle("Requested Seed")
            .WithUrl(Url)
            .WithDescription(
@$"```
{Flags}
```")
            .WithColor(GetDiscordColor())
            .AddField("URL", Url)
            .AddField("Hash", Verification, inline: true);

        if (seed is not null)
        {
            builder.AddField("Provided Seed", seed, inline: true);
        }
        else
        {
            builder.AddField("Seed", Seed, inline: true);
        }

        return builder.Build();
    }

    private DiscordEmbed? ToFlagsVerificationEmbed(string flags)
    {
        return VerifyFlags(flags)
        ? null
        : new DiscordEmbedBuilder()
            .WithTitle("Flags Mismatch")
            .WithDescription(FlagsVerificationEmbedDescription(flags))
            .WithColor(DiscordColor.Red)
            .Build();
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
            return string.Equals(Flags, flags, StringComparison.InvariantCultureIgnoreCase);
        }
        else
        {
            var matches = UrlFlagsRegex().Match(Url);
            if (matches.Success)
            {
                var binaryFlags = matches.Groups[1].Captures.FirstOrDefault()?.Value ?? "";
                return binaryFlags.Equals(flags, StringComparison.InvariantCultureIgnoreCase);
            }
            else
            {
                return false;
            }
        }
    }

    private DiscordColor GetDiscordColor()
    {
        return Url switch
        {
            var galeswift when galeswift.Contains("galeswift") => DiscordColor.CornflowerBlue,
            _ => DiscordColor.DarkBlue
        };
    }


    [GeneratedRegex(@".+=(.+)\.", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex UrlFlagsRegex();
}
