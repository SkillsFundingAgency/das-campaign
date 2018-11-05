﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Polly;

namespace SFA.DAS.Campaign.Application.Core
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
    }
}
