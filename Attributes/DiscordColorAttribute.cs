
namespace chocobot_racing.Attributes;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class DiscordColorAttribute : Attribute
{
    readonly DiscordColor _color;

    public DiscordColorAttribute(int hexColor)
    {
        _color = hexColor;
    }

    public DiscordColor Color => _color;

}
