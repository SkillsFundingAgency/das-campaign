namespace Sfa.Das.Sas.Core.Configuration
{
    public interface ICacheSettings
    {
        int CacheAbsoluteExpirationDays { get; }
        int CacheSlidingExpirationDays { get;  }
        int CacheMemoryAbsoluteExpirySeconds { get; }
    }
}
