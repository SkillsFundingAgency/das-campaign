using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SFA.DAS.Campaign.Application.Configuration;
using SFA.DAS.Campaign.Application.Core;
using SFA.DAS.Campaign.Application.DataCollection;
using SFA.DAS.Campaign.Application.Geocode;
using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;
using SFA.DAS.Campaign.Domain.Vacancies;
using SFA.DAS.Campaign.Infrastructure.Api.Converters;
using SFA.DAS.Campaign.Infrastructure.Api.Factory;
using SFA.DAS.Campaign.Infrastructure.Configuration;
using SFA.DAS.Campaign.Infrastructure.Geocode;
using SFA.DAS.Campaign.Infrastructure.Geocode.Configuration;
using SFA.DAS.Campaign.Infrastructure.HealthChecks;
using SFA.DAS.Campaign.Infrastructure.Mappers;
using SFA.DAS.Campaign.Infrastructure.Queue;
using SFA.DAS.Campaign.Infrastructure.Repositories;
using SFA.DAS.Campaign.Infrastructure.Services;
using StackExchange.Redis;
using VacanciesApi;

namespace SFA.DAS.Campaign.Web.Helpers
{
    public static class StartupExtensions
    {
        public static void ConfigureSfaConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CampaignConfiguration>(configuration);
            services.AddSingleton(cfg => cfg.GetService<IOptions<CampaignConfiguration>>().Value);

            services.Configure<UserDataCryptography>(configuration.GetSection("UserDataCryptography"));
            services.Configure<UserDataQueueNames>(configuration.GetSection("UserDataQueueNames"));

            var postcodeConfig = new PostcodeApiConfiguration();
            configuration.Bind("Postcode", postcodeConfig);

            var mappingConfig = new MappingConfiguration();
            configuration.Bind("Mapping", mappingConfig);

            services.AddSingleton<IPostcodeApiConfiguration>(postcodeConfig);
            services.AddSingleton<IMappingConfiguration>(mappingConfig);

            services.Configure<MappingConfiguration>(configuration.GetSection("Mapping"));
        }

        public static void ConfigureSfaConnectionStrings(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionStrings = new ConnectionStrings();
            configuration.Bind("ConnectionStrings", connectionStrings);

            var queueStorageConnectionString = configuration.Get<CampaignConfiguration>().QueueConnectionString;

            services.AddHealthChecks()
                .AddAzureQueueStorage(queueStorageConnectionString, "queue-storage-check")
                .AddCheck<VacancyServiceApiHealthCheck>("vacancy-api-check")
                .AddCheck<PostCodeLookupHealthCheck>("postcode-api-check");

            services.AddTransient<ConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect($"{connectionStrings.SharedRedis},{connectionStrings.ContentCacheDatabase},allowAdmin=true"));

            if (configuration["Environment"] == "LOCAL")
            {
                services.AddDistributedMemoryCache();
                return;
            }

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = connectionStrings.SharedRedis;
            });

            services.AddHealthChecks().AddRedis(connectionStrings.SharedRedis, "redis-app-cache-check");

            var redis = ConnectionMultiplexer.Connect($"{connectionStrings.SharedRedis},DefaultDatabase=3");
            services.AddDataProtection()
                .SetApplicationName("das-campaign-web")
                .PersistKeysToStackExchangeRedis(redis, "DataProtection-Keys");

            services.AddTransient<IDatabase>(client => client.GetService<ConnectionMultiplexer>().GetDatabase());
        }

        public static void ConfigureSfaVacancies(this IServiceCollection services, IConfiguration configuration)
        {
            var vacanciesBaseUrl = configuration.GetValue<string>("VacanciesApi:BaseUrl");
            var vacanciesHttpClient = new HttpClient() { BaseAddress = new Uri(vacanciesBaseUrl) };
            vacanciesHttpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", configuration.GetValue<string>("VacanciesApi:ApiKey"));

            services.AddTransient<ILivevacanciesAPI>(client => new LivevacanciesAPI(vacanciesHttpClient, false) { BaseUri = new Uri(vacanciesBaseUrl) });
        }

        public static void ConfigureSfaServices(this IServiceCollection services)
        {
            services.AddTransient<IUserDataCryptographyService, UserDataCryptographyService>();
            services.AddTransient<ISessionService, SessionService>();
            services.AddTransient<IGeocodeService, GeocodeService>();
            services.AddTransient<IRetryWebRequests, WebRequestRetryService>();
            services.AddTransient<IMappingService, GoogleMappingService>();
            services.AddTransient(typeof(IQueueService<>), typeof(AzureQueueService<>));
        }

        public static void ConfigureSfaRepositories(this IServiceCollection services)
        {
            services.AddTransient<IStandardsRepository, StandardsRepository>();
            services.AddTransient<IVacanciesRepository, VacanciesRepository>();

        }

        public static void ConfigureSfaMappers(this IServiceCollection services)
        {
            services.AddTransient<IVacanciesMapper, VacanciesMapper>();
            services.AddTransient<ICountryMapper, CountryMapper>();
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
        }
    }
}
