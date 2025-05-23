
namespace chocobot_racing;

public class FeInfoHttpClient : HttpClient
{
    public FeInfoHttpClient(string apiKey, Uri baseAddress)
    {
        BaseAddress = baseAddress;
        DefaultRequestHeaders.Add("Api-Key", apiKey);
    }

    public FeInfoHttpClient(SocketsHttpHandler handler, string apiKey, Uri baseAddress) : base(handler)
    {
        BaseAddress = baseAddress;
        DefaultRequestHeaders.Add("Api-Key", apiKey);
    }
}
