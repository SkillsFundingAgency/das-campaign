using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sfa.Das.Sas.Shared.Basket.Infrastructure;
using Sfa.Das.Sas.Shared.Basket.Interfaces;

namespace Sfa.Das.Sas.Shared.Basket
{
    public static class ServiceCollectionExtensions
    {
        public static void AddFavouritesBasket(this IServiceCollection services, string redisConnectionString, int expiryLengthInDays = 90)
        {
            ConfigureCaching(services, redisConnectionString, expiryLengthInDays);

            services.AddSingleton(x => new ApprenticehipFavouritesBasketStoreConfig(redisConnectionString, expiryLengthInDays));
            services.AddScoped<IApprenticeshipFavouritesBasketStore, ApprenticeshipFavouritesBasketStore>();
        }

        private static void ConfigureCaching(IServiceCollection services, string redisConnectionString, int expiryLengthInDays = 90)
        {
            if (!services.BuildServiceProvider().GetService<IHostingEnvironment>().IsDevelopment())
            {
                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = redisConnectionString;
                });
            }
            else
            {
                services.AddDistributedMemoryCache();
            }
        }
    }
}
