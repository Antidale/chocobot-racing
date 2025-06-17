using System.Net.Http.Json;
using chocobot_racing.Constants;
using chocobot_racing.DTOs;
using chocobot_racing.RollCommand.DTOs;
using chocobot_racing.RollCommand.Enums;
using FeInfo.Common.Requests;
using static chocobot_racing.RollCommand.DTOs.FeApiResponse;
using static chocobot_racing.RollCommand.Helpers.EndpointHelper;

namespace chocobot_racing.RollCommand.Helpers;

public static class SeedRollerHelper
{
    private const int MAX_TRIES = 100;
    private const int STANDARD_DELAY = 2;
    public static async Task<SeedResponse> RollSeedAsync(HttpClient client, GenerateRequest generateRequest, FeHostedApi api)
    {
        var apiKey = GetApiKey(api);
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            return SetError<SeedResponse>($"Configuration incomplete, cannot create seeds for {api}");
        }

        return await GenerateSeedAsync(client, generateRequest, api, apiKey) switch
        {
            var error when error.Status == FeStatusConstants.Error => SetError<SeedResponse>(error.Error),

            ProgressResponse pr when pr.seed_id.HasContent()
                => await GetGeneratedSeedAsync(client, api, apiKey, pr.seed_id),

            ProgressResponse pr when pr.task_id.HasContent()
                => await PollForSeedAsync(client, api, apiKey, pr.task_id),

            _ => SetError<SeedResponse>("Unknown Issue preventing seed generation")
        };
    }

    private static async Task<FeApiResponse> GenerateSeedAsync(HttpClient client, GenerateRequest generateRequest, FeHostedApi api, string apiKey)
    {
        var postUrl = GenerateUrl(api, apiKey);
        var generateResponse = await client.PostAsJsonAsync(
            postUrl,
            generateRequest);

        if (!generateResponse.IsSuccessStatusCode)
        {
            var message = await generateResponse.Content.ReadAsStringAsync();
            return SetError<ProgressResponse>(message);
        }

        return await generateResponse.Content.ReadFromJsonAsync<ProgressResponse>()
            ?? SetError<ProgressResponse>("Failed to get content");
    }

    private static async Task<SeedResponse> PollForSeedAsync(HttpClient client, FeHostedApi api, string apiKey, string taskId)
    {
        var tries = 0;
        var progressResponse = new ProgressResponse();
        List<string> stopProcessingStatuses = [FeStatusConstants.Done, FeStatusConstants.Error];

        while (!stopProcessingStatuses.Contains(progressResponse.Status) && tries < MAX_TRIES)
        {
            await Task.Delay(TimeSpan.FromSeconds(STANDARD_DELAY).Add(TimeSpan.FromMilliseconds(tries * 10)));
            var taskResponse = await client.GetAsync(TaskUrl(api, apiKey, taskId));

            if (!taskResponse.IsSuccessStatusCode)
            {
                return SetError<SeedResponse>(await taskResponse.Content.ReadAsStringAsync());
            }

            progressResponse = await taskResponse.Content.ReadFromJsonAsync<ProgressResponse>()
                ?? progressResponse;

            tries++;
        }

        return progressResponse switch
        {
            { Error: var error } when error.HasContent() => SetError<SeedResponse>(progressResponse.Error),
            { seed_id: var seedId } when string.IsNullOrWhiteSpace(seedId) => SetError<SeedResponse>("API seems to be not responding"),
            _ => await GetGeneratedSeedAsync(client, api, apiKey, progressResponse.seed_id)
        };
    }

    public static async Task LogRolledSeedAsync(FeInfoHttpClient client, LogSeedRoled seedInfo)
    {
        try
        {
            var response = await client.PostAsJsonAsync("seed", seedInfo);
        }
        catch (Exception)
        {
            //currently not interested in handling errors here
        }
    }

    private static async Task<SeedResponse> GetGeneratedSeedAsync(HttpClient client, FeHostedApi api, string apiKey, string seedId)
    {
        var getSeedResponse = await client.GetAsync(SeedUrl(api, apiKey, seedId));

        if (getSeedResponse.IsSuccessStatusCode)
        {
            return await getSeedResponse.Content.ReadFromJsonAsync<SeedResponse>() ?? SetError<SeedResponse>("Failed to get seed content");
        }
        else
        {
            var errorMessage = await getSeedResponse.Content.ReadAsStringAsync();
            return SetError<SeedResponse>(errorMessage);
        }
    }

    private static string GetApiKey(FeHostedApi url)
    {
        return Environment.GetEnvironmentVariable($"{url}_API_KEY".ToUpperInvariant()) ?? string.Empty;
    }
}
