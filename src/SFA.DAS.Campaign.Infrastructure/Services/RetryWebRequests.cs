using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Polly;

namespace SFA.DAS.Campaign.Infrastructure.Services
{
    public sealed class WebRequestRetryService : IRetryWebRequests
    {
        private readonly ILogger<WebRequestRetryService> _logger;

        public WebRequestRetryService(ILogger<WebRequestRetryService> logger)
        {
            _logger = logger;
        }

        public async Task<T> Retry<T>(Func<Task<T>> action, Action<Exception> onError)
        {
            var policy = Policy.Handle<Exception>()
                .WaitAndRetryAsync(
                    2,
                    retrytime => TimeSpan.FromSeconds(Math.Pow(2, retrytime)),
                    (exception, timespan) => { onError.Invoke(exception); });

            return await policy.ExecuteAsync(action);
        }

        public async Task<HttpResponseMessage> MakeRequestAsync(string url)
        {
            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                {
                    return await client.SendAsync(request);
                }
            }
        }
    }
}
