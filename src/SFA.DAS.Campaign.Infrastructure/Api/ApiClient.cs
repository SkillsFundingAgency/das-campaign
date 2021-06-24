using System;
using System.ComponentModel.Design;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SFA.DAS.Campaign.Infrastructure.Api.Converters;
using SFA.DAS.Campaign.Infrastructure.Api.Factory;
using SFA.DAS.Campaign.Infrastructure.Configuration;
using SFA.DAS.Campaign.Models.Configuration;

namespace SFA.DAS.Campaign.Infrastructure.Api
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IHtmlControlAbstractFactory _htmlControlAbstractFactory;
        private readonly OuterApiConfiguration _config;

        public ApiClient (HttpClient httpClient, IOptions<CampaignConfiguration> config, IHtmlControlAbstractFactory htmlControlAbstractFactory)
        {
            _httpClient = httpClient;
            _htmlControlAbstractFactory = htmlControlAbstractFactory;
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

                var articleJsonConverter = new ArticleJsonConverter(_htmlControlAbstractFactory);

                if (articleJsonConverter.CanConvert(typeof(TResponse)))
                {
                    return JsonConvert.DeserializeObject<TResponse>(json, articleJsonConverter);
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