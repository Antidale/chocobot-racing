namespace chocobot_racing.Helpers;

public static class SetupHelper
{
    public static string GetDiscordBotToken()
    {
        var token = Environment.GetEnvironmentVariable("DiscordBotToken");

#if DEBUG
        token = Environment.GetEnvironmentVariable("TestBotToken");
#endif

        return token ?? string.Empty;
    }
}
