using System;
using System.Threading.Tasks;

namespace Sfa.Das.Sas.Core
{
    public interface IRetryWebRequests
    {
        Task<T> RetryWeb<T>(Func<Task<T>> action, Action<Exception> onError);
    }
}