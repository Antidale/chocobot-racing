
namespace chocobot_racing;

public class FeInfoHttpClient : HttpClient
{
    public FeInfoHttpClient()
    {
        var apiKey = Environment.GetEnvironmentVariable("FE_Info_Api_Key");
        var baseAddress = new Uri("https://free-enterprise-info-api.herokuapp.com/api/");

#if DEBUG
        apiKey = Environment.GetEnvironmentVariable("FE_Info_Local_Key");
        baseAddress = new Uri("https://localhost:5001/api/");
#endif

        this.BaseAddress = baseAddress;
        this.DefaultRequestHeaders.Add("Api-Key", apiKey);
    }

    public FeInfoHttpClient(SocketsHttpHandler handler) : base(handler)
    {
        var apiKey = Environment.GetEnvironmentVariable("FE_Info_Api_Key");
        var baseAddress = new Uri("https://free-enterprise-info-api.herokuapp.com/api/");

#if DEBUG
        apiKey = Environment.GetEnvironmentVariable("FE_Info_Local_Key");
        baseAddress = new Uri("https://localhost:5001/api/");
#endif

        this.BaseAddress = baseAddress;
        this.DefaultRequestHeaders.Add("Api-Key", apiKey);
    }
}
