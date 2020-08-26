using System;
using Sfa.Das.Sas.Core.Configuration;

namespace Sfa.Das.Sas.Shared.Components.Configuration
{
    public class FatSharedComponentsConfiguration : IFatConfigurationSettings, IPostcodeIOConfigurationSettings, IApprenticehipFavouritesBasketStoreConfig, ICacheSettings
    {
        public string FatApiBaseUrl { get; set; }
        public string SaveEmployerFavouritesUrl { get; set; }
        public Uri PostcodeUrl { get; set; }
        public Uri PostcodeTerminatedUrl { get; set; }
        public string BasketRedisConnectionString { get; set; }
        public int BasketSlidingExpiryDays { get; set; }
        public int CacheAbsoluteExpirationDays { get; set; }
        public int CacheSlidingExpirationDays { get; set; }
        public int CacheMemoryAbsoluteExpirySeconds { get; set;  }
    }
}
