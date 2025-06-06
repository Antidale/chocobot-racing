using System.Net.Http.Json;
using System.Text;
using chocobot_racing.RacingCommands.Requests;


namespace chocobot_racing;

public class RacetimeHttpClient : HttpClient
{
    private readonly string ClientId = string.Empty;
    private readonly string ClientSecret = string.Empty;
    private string authToken = string.Empty;
    private DateTime TokenExpiresAt = DateTime.UtcNow.AddMinutes(-10);

    public RacetimeHttpClient()
    {
        ClientId = Environment.GetEnvironmentVariable("Racetime_Client_Id") ?? ClientId;
        ClientSecret = Environment.GetEnvironmentVariable("Racetime_Client_Secret") ?? ClientSecret;
        var baseAddress = new Uri("https://racetime.gg/o/");

#if DEBUG
        ClientId = Environment.GetEnvironmentVariable("Local_RTGG_Client_ID") ?? ClientId;
        ClientSecret = Environment.GetEnvironmentVariable("Local_RTGG_Client_Secret") ?? ClientSecret;
        baseAddress = new Uri("http://localhost:8000/o/");
#endif

        BaseAddress = baseAddress;
    }

    private async Task AuthorizeAsync()
    {
        DefaultRequestHeaders.Remove("Authorization");
        var stuff = new StringContent($"client_id={ClientId}&client_secret={ClientSecret}&grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");

        var response = await this.PostAsync("token", stuff);

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("Failed authorization");
            return;
        }

        var responseDto = await response.Content.ReadFromJsonAsync<AuthResponse>();
        if (responseDto == null)
        {
            Console.WriteLine("We didn't get an auth token from the auth endpoint");
            return;
        }

        TokenExpiresAt = DateTime.UtcNow.AddSeconds(responseDto.expires_in - 100);

        authToken = responseDto.access_token;

        DefaultRequestHeaders.Add("Authorization", $"Bearer {authToken}");
    }

    public async Task<HttpResponseMessage?> CreateRaceAsync(CreateRacetimeRace request)
    {
        if (!DefaultRequestHeaders.Contains("Authorization") || TokenExpiresAt < DateTime.UtcNow)
        {
            await AuthorizeAsync();
        }

        var content = request.ToStringContent();

        return await this.PostAsync("ff4fe/startrace", content);
    }

    record AuthResponse(string access_token, int expires_in, string token_type, string scope) { }
}
