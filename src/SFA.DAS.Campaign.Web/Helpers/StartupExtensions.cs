using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.Campaign.Application.DataCollection;
using SFA.DAS.Campaign.Application.Geocode;
using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;
using SFA.DAS.Campaign.Domain.Interfaces;
using SFA.DAS.Campaign.Infrastructure.Api.Converters;
using SFA.DAS.Campaign.Infrastructure.Api.Factory;
using SFA.DAS.Campaign.Infrastructure.Configuration;
using SFA.DAS.Campaign.Infrastructure.Geocode.Configuration;
using SFA.DAS.Campaign.Infrastructure.Queue;
using SFA.DAS.Campaign.Infrastructure.Repositories;
using SFA.DAS.Campaign.Infrastructure.Services;
using StackExchange.Redis;

namespace SFA.DAS.Campaign.Web.Helpers
{
    public static class StartupExtensions
    {
        public static void ConfigureSfaConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            var campaignConfiguration = new CampaignConfiguration();
            configuration.Bind("CampaignConfiguration", campaignConfiguration);

            services.Configure<CampaignConfiguration>(configuration.GetSection("CampaignConfiguration"));

            services.AddSingleton<CampaignConfiguration>(campaignConfiguration);
            
            services.Configure<UserDataCryptography>(configuration.GetSection("CampaignConfiguration:UserDataCryptography"));
            services.Configure<UserDataQueueNames>(configuration.GetSection("CampaignConfiguration:UserDataQueueNames"));

            var mappingConfig = new MappingConfiguration();
            configuration.Bind("CampaignConfiguration:Mapping", mappingConfig);

            services.AddSingleton<IMappingConfiguration>(mappingConfig);

            services.Configure<MappingConfiguration>(configuration.GetSection("CampaignConfiguration:Mapping"));
        }

        public static void ConfigureSfaConnectionStrings(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionStrings = new ConnectionStrings();
            configuration.Bind("CampaignConfiguration:ConnectionStrings", connectionStrings);

            var queueStorageConnectionString = configuration.Get<CampaignConfiguration>().QueueConnectionString;

            services.AddHealthChecks()
                .AddAzureQueueStorage(queueStorageConnectionString, "queue-storage-check");

            if (configuration["Environment"] != "LOCAL")
            {
                var redis = ConnectionMultiplexer.Connect($"{connectionStrings.SharedRedis},DefaultDatabase=3");
                services.AddDataProtection()
                    .SetApplicationName("das-campaign-web")
                    .PersistKeysToStackExchangeRedis(redis, "DataProtection-Keys");    
            }
        }

        public static void ConfigureSfaServices(this IServiceCollection services)
        {
            services.AddTransient<IUserDataCryptographyService, UserDataCryptographyService>();
            services.AddTransient<ISessionService, SessionService>();
            services.AddTransient<IRetryWebRequests, WebRequestRetryService>();
            services.AddTransient<IMappingService, GoogleMappingService>();
            services.AddTransient(typeof(IQueueService<>), typeof(AzureQueueService<>));
        }

        public static void ConfigureSfaRepositories(this IServiceCollection services)
        {
            services.AddTransient<IStandardsRepository, StandardsRepository>();

        }

        public static void ConfigureSfaDataCollection(this IServiceCollection services)
        {
            services.AddTransient<IUserDataCollection, UserDataCollection>();
            services.AddTransient<IUserDataCollectionValidator, UserDataCollectionValidator>();
        }

        public static void ConfigureFactorys(this IServiceCollection services)
        {
            services.AddTransient<IHtmlControlAbstractFactory, HtmlControlAbstractFactory>();
            services.AddTransient<IHtmlControlFactory, ParagraphControlFactory>();
            services.AddTransient<IHtmlControlFactory, TableControlFactory>();
            services.AddTransient<IHtmlControlFactory, UnorderedListControlFactory>();
            services.AddTransient<IHtmlControlFactory, OrderedListControlFactory>();
            services.AddTransient<IHtmlControlFactory, HeadingControlFactory>();
            services.AddTransient<IHtmlControlFactory, ImageControlFactory>();
            services.AddTransient<IHtmlControlFactory, YouTubeControlFactory>();
            services.AddTransient<IHtmlControlFactory, BlockQuoteControlFactory>();
            services.AddTransient<IHtmlControlFactory, HorizontalRuleControlFactory>();
        }

        public static void ConfigureJsonConverters(this IServiceCollection services)
        {
            services.AddSingleton<ICmsPageConverter, ArticleJsonConverter>();
            services.AddSingleton<ICmsPageConverter, HubJsonConverter>();
            services.AddSingleton<ICmsPageConverter, LandingPageJsonConverter>();
            services.AddSingleton<ICmsPageConverter, SiteMapJsonConverter>();
            services.AddSingleton<ICmsPageConverter, MenuJsonConverter>();
            services.AddSingleton<ICmsPageConverter, BannerJsonConverter>();
        }
    }
}
