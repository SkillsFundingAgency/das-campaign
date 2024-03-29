using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SFA.DAS.Campaign.Domain.Api.Interfaces;
using SFA.DAS.Campaign.Infrastructure.Api.Converters;
using SFA.DAS.Campaign.Infrastructure.Configuration;

namespace SFA.DAS.Campaign.Infrastructure.Api
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IEnumerable<ICmsPageConverter> _jsonConverters;
        private readonly OuterApiConfiguration _config;

        public ApiClient (HttpClient httpClient, IOptions<CampaignConfiguration> config, IEnumerable<ICmsPageConverter> jsonConverters)
        {
            _httpClient = httpClient;
            _jsonConverters = jsonConverters;
            _config = config.Value.OuterApi;
            _httpClient.BaseAddress = new Uri(_config.BaseUrl);
        }
        
        public async Task<TResponse> Get<TResponse>(IGetApiRequest request) 
        {
            AddHeaders();
 
            var response = await _httpClient.GetAsync(request.GetUrl).ConfigureAwait(false);

            if (response.StatusCode.Equals(HttpStatusCode.NotFound))
            {
                return default;
            }

            if (response.IsSuccessStatusCode)
            {
                
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                foreach (var cmsPageConverter in _jsonConverters)
                {
                    var converter = (JsonConverter) cmsPageConverter;
                    if (converter.CanConvert(typeof(TResponse)))
                    {
                        return JsonConvert.DeserializeObject<TResponse>(json, converter);
                    }
                }
               
                return JsonConvert.DeserializeObject<TResponse>(json);
            }
            
            response.EnsureSuccessStatusCode();
            
            return default;
        }

        private void AddHeaders()
        {
            _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _config.Key);
            _httpClient.DefaultRequestHeaders.Add("X-Version", "1");
        }
    }
}