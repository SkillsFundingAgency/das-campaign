namespace Sfa.Das.Sas.Shared.Basket
{
    internal class ApprenticehipFavouritesBasketStoreConfig
    {
        public ApprenticehipFavouritesBasketStoreConfig(string connectionString, int expiryDuration = 90)
        {
            BasketRedisConnectionString = connectionString;
            BasketSlidingExpiryDays = expiryDuration;
        }

        public string BasketRedisConnectionString { get; private set; }
        public int BasketSlidingExpiryDays { get; private set; }
    }
}
