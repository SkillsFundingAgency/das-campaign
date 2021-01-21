using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sfa.Das.Sas.ApplicationServices.Services;

namespace Sfa.Das.Sas.Shared.Components
{
    public static class ServiceCollectionExtensions
    {
        public static void AddFatSharedComponentsCaching(this IServiceCollection services, string redisConnectionString)
        {
            ConfigureCaching(services, redisConnectionString);

            services.AddScoped<ICacheStorageService, CacheStorageService>();
        }

        private static void ConfigureCaching(IServiceCollection services, string redisConnectionString)
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
            services.AddMemoryCache(x => {
                x.SizeLimit = 1024;
                x.CompactionPercentage = .33;
            });
        }
    }
}
