using chocobot_racing.RollCommand.Enums;

namespace chocobot_racing.RollCommand.Helpers;

public static class EndpointHelper
{
    public static string GenerateUrl(FeHostedApi hostedApi, string apiKey) =>
        $"{GetApiUrl(hostedApi)}generate{FormatKeyParam(apiKey)}";

    public static string TaskUrl(FeHostedApi hostedApi, string apiKey, string taskId) =>
        $"{GetApiUrl(hostedApi)}task{FormatKeyAndIdParams(apiKey, taskId)}";

    public static string SeedUrl(FeHostedApi hostedApi, string apiKey, string taskId)
        => $"{GetApiUrl(hostedApi)}seed{FormatKeyAndIdParams(apiKey, taskId)}";

    private static string FormatKeyAndIdParams(string apiKey, string id) => $"{FormatKeyParam(apiKey)}&{FormatIdParam(id)}";
    private static string FormatKeyParam(string apiKey) => $"?key={apiKey}";
    private static string FormatIdParam(string id) => $"id={id}";

    //TODO: I don't know why having a Description attribute and a .GetDescription call was blowing up so badly
    //but that is a TODO for a future me.
    private static string GetApiUrl(FeHostedApi api) => api switch
    {
#if DEBUG
        FeHostedApi.Local => "http://127.0.0.1:8080/api/",
#endif
        FeHostedApi.Main => "http://ff4fe.com/api/",
        FeHostedApi.Galeswift => "https://ff4fe.galeswift.com/api/",
        _ => ""
    };
}
