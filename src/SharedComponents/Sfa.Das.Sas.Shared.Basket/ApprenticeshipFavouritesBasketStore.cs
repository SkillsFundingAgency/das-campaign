using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Sfa.Das.Sas.Shared.Basket.Interfaces;
using Sfa.Das.Sas.Shared.Basket.Models;

namespace Sfa.Das.Sas.Shared.Basket.Infrastructure
{
    internal class ApprenticeshipFavouritesBasketStore : IApprenticeshipFavouritesBasketStore
    {
        private const string CacheItemPrefix = "EmpFav-";
        private readonly IDistributedCache _cache;
        private readonly ApprenticehipFavouritesBasketStoreConfig _config;
        
        public ApprenticeshipFavouritesBasketStore(IDistributedCache cache, ApprenticehipFavouritesBasketStoreConfig config)
        {
            _cache = cache;
            _config = config;
        }

        public Task<ApprenticeshipFavouritesBasket> GetAsync(Guid basketId)
        {
            return RetrieveFromCache($"{CacheItemPrefix}{basketId}");
        }

        public Task UpdateAsync(ApprenticeshipFavouritesBasket basket)
        {
            return SaveToCache($"{CacheItemPrefix}{basket.Id}", basket, new TimeSpan(_config.BasketSlidingExpiryDays, 0, 0, 0));
        }

        public Task RemoveAsync(Guid basketId)
        {
            return RemoveFromCache($"{CacheItemPrefix}{basketId}");
        }

        private static ApprenticeshipFavouritesBasket DeserializeBasket(string json, string key)
        {
            if (json == null)
            {
                return null;
            }

            var basketItems = JsonConvert.DeserializeObject<IList<ApprenticeshipFavourite>>(json);
            var basket = new ApprenticeshipFavouritesBasket(basketItems);
            var basketIdValue = key.Remove(0, CacheItemPrefix.Length);
            basket.Id = Guid.Parse(basketIdValue);

            return basket;
        }

        private Task SaveToCache(string key, ApprenticeshipFavouritesBasket item, TimeSpan slidingExpiration)
        {
            var json = JsonConvert.SerializeObject(item);

            var options = new DistributedCacheEntryOptions
            {
                SlidingExpiration = slidingExpiration
            };

            return _cache.SetStringAsync(key, json, options);
        }

        private async Task<ApprenticeshipFavouritesBasket> RetrieveFromCache(string key)
        {
            var json = await _cache.GetStringAsync(key);

            return DeserializeBasket(json, key);
        }

        private async Task RemoveFromCache(string key)
        {
           await _cache.RemoveAsync(key);
        }
    }
}
