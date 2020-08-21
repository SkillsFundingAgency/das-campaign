namespace Sfa.Das.Sas.Core.Configuration
{
    public interface IApprenticehipFavouritesBasketStoreConfig
    {
        string BasketRedisConnectionString { get; }
        int BasketSlidingExpiryDays { get; }
    }
}
