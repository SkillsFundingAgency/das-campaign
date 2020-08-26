using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sfa.Das.Sas.ApplicationServices.Services
{
    public interface ICacheStorageService
    {
        Task<T> RetrieveFromCache<T>(string key);
        Task SaveToCache<T>(string key, T item, TimeSpan absoluteExpiration, TimeSpan slidingExpiration);
        Task DeleteFromCache<T>(string key);

    }
}
