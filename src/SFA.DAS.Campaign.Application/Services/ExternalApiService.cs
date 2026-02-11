using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SFA.DAS.Campaign.Infrastructure.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Application.Services;

public class ExternalApiService : IExternalApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl;
    private readonly string _apiKey;
    private readonly ILogger<ExternalApiService> _logger;

    public ExternalApiService(HttpClient httpClient, IOptions<CampaignConfiguration> config, ILogger<ExternalApiService> logger)
    {
        if (config.Value.CampaignRegisterInterestOuterApi.BaseUrl == null)
        {
            throw new ArgumentNullException(nameof(config));
        }
        if (config.Value.CampaignRegisterInterestOuterApi.Key == null)
        {
            throw new ArgumentNullException(nameof(config));
        }
        _httpClient = httpClient;
        _apiUrl = config.Value.CampaignRegisterInterestOuterApi.BaseUrl;
        _apiKey = config.Value.CampaignRegisterInterestOuterApi.Key;
        _logger = logger;
    }

    public async Task<string> PostDataAsync(string endpoint, object body)
    {
        var requestUrl = $"{_apiUrl}/{endpoint}";
        _logger.LogInformation("Making POST request to {RequestUrl}", requestUrl);

        var request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
        request.Headers.Add("Accept","text/plain");
        request.Headers.Add("X-Version", "1");
        request.Headers.Add("Ocp-Apim-Subscription-Key", _apiKey);

        // Serialize the body object to JSON and set it as the content
        var jsonBody = JsonSerializer.Serialize(body);
        request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("Failed to post data to {RequestUrl}. Status Code: {StatusCode}", requestUrl, response.StatusCode);
            response.EnsureSuccessStatusCode();
        }

        var content = await response.Content.ReadAsStringAsync();
        _logger.LogInformation("Received response: {Content}", content);

        return content;
    }
}
