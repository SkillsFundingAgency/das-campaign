using System;
using System.Threading.Tasks;
using Polly;
using SFA.DAS.NLog.Logger;

namespace Sfa.Das.Sas.Core
{
    public sealed class WebRequestRetryService : IRetryWebRequests
    {
        private readonly ILog _logger;

        public WebRequestRetryService(ILog logger)
        {
            _logger = logger;
        }

        public async Task<T> RetryWeb<T>(Func<Task<T>> action, Action<Exception> onError)
        {
            var policy = Policy.Handle<Exception>()
                .WaitAndRetryAsync(
                    2,
                    retrytime => TimeSpan.FromSeconds(Math.Pow(2, retrytime)),
                    (exception, timespan) => { onError.Invoke(exception); });

            return await policy.ExecuteAsync(action);
        }
    }
}
