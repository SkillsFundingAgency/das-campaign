using System;
using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Infrastructure.Services
{
    public interface IIfaStandardsCacheService
    {
        Task<T> RetrieveFromCache<T>(string key);
        Task SaveToCache<T>(string key, T item, TimeSpan absoluteExpiration, TimeSpan slidingExpiration);
        Task DeleteFromCache(string key);
    }
}