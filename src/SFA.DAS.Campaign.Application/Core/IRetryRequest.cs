using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Application.Core
{
    public interface IRetryWebRequests
    {
        Task<T> Retry<T>(Func<Task<T>> action, Action<Exception> onError);
        Task<HttpResponseMessage> MakeRequestAsync(string url);
    }
}
