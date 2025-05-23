
namespace chocobot_racing;

public class FeInfoHttpClient : HttpClient
{
    public FeInfoHttpClient(string apiKey, string baseAddress)
    {
        BaseAddress = new Uri(baseAddress);
        DefaultRequestHeaders.Add("Api-Key", apiKey);
    }

    public FeInfoHttpClient(SocketsHttpHandler handler, string apiKey, string baseAddress) : base(handler)
    {
        BaseAddress = new Uri(baseAddress);
        DefaultRequestHeaders.Add("Api-Key", apiKey);
    }
}
