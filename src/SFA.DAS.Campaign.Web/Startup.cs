using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Refit;
using Sfa.Das.Sas.Core.Configuration;
using Sfa.Das.Sas.Shared.Components.Configuration;
using Sfa.Das.Sas.Shared.Components.DependencyResolution;
using Sfa.Das.Sas.Shared.Components.ViewModels.Css.Interfaces;
using SFA.DAS.Apprenticeships.Api.Client;
using SFA.DAS.Campaign.Application.Configuration;
using SFA.DAS.Campaign.Application.Core;
using SFA.DAS.Campaign.Application.DataCollection;
using SFA.DAS.Campaign.Application.Geocode;
using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;
using SFA.DAS.Campaign.Domain.Vacancies;
using SFA.DAS.Campaign.Infrastructure.Configuration;
using SFA.DAS.Campaign.Infrastructure.Geocode;
using SFA.DAS.Campaign.Infrastructure.Geocode.Configuration;
using SFA.DAS.Campaign.Infrastructure.HealthChecks;
using SFA.DAS.Campaign.Infrastructure.Mappers;
using SFA.DAS.Campaign.Infrastructure.Queue;
using SFA.DAS.Campaign.Infrastructure.Repositories;
using SFA.DAS.Campaign.Infrastructure.Services;
using SFA.DAS.Campaign.Models.Configuration;
using SFA.DAS.Campaign.Web.HealthChecks;
using SFA.DAS.Campaign.Web.Models.Fat;
using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using VacanciesApi;

namespace SFA.DAS.Campaign.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")	                
                .AddEnvironmentVariables()
                .AddAzureTableStorageConfiguration(
                    configuration["ConfigurationStorageConnectionString"],
                    configuration["Environment"],
                    configuration["Version"]
                    )
                .AddUserSecrets<Startup>()
                .Build();

            Configuration = config;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<CampaignConfiguration>(Configuration);

            services.Configure<UserDataCryptography>(Configuration.GetSection("UserDataCryptography"));
            services.Configure<UserDataQueueNames>(Configuration.GetSection("UserDataQueueNames"));

            var connectionStrings = new ConnectionStrings();

            Configuration.Bind("ConnectionStrings", connectionStrings);

            var postcodeConfig = new PostcodeApiConfiguration();
            Configuration.Bind("Postcode", postcodeConfig);

            var mappingConfig = new MappingConfiguration();
            Configuration.Bind("Mapping", mappingConfig);

            services.Configure<MappingConfiguration>(Configuration.GetSection("Mapping"));

            var queueStorageConnectionString = Configuration.Get<CampaignConfiguration>().QueueConnectionString;

            var healthChecks = services.AddHealthChecks()
                .AddAzureQueueStorage(queueStorageConnectionString, "queue-storage-check")
                .AddCheck<FatApiHealthCheck>("fat-api-check")
                .AddCheck<IfaApiHealthCheck>("ifa-api-check")
                .AddCheck<VacancyServiceApiHealthCheck>("vacancy-api-check")
                .AddCheck<PostCodeLookupHealthCheck>("postcode-api-check");


            services.AddMiniProfiler(options =>
            {
                // ALL of this is optional. You can simply call .AddMiniProfiler() for all defaults
                // Defaults: In-Memory for 30 minutes, everything profiled, every user can see

                // Path to use for profiler URLs, default is /mini-profiler-resources
                options.RouteBasePath = "/profiler";

                // Control storage - the default is 30 minutes
                //(options.Storage as MemoryCacheStorage).CacheDuration = TimeSpan.FromMinutes(60);
                //options.Storage = new SqlServerStorage("Data Source=.;Initial Catalog=MiniProfiler;Integrated Security=True;");

                // Control which SQL formatter to use, InlineFormatter is the default
                options.SqlFormatter = new StackExchange.Profiling.SqlFormatters.SqlServerFormatter();

                // To control authorization, you can use the Func<HttpRequest, bool> options:
                //options.ResultsAuthorize = request => !Program.DisableProfilingResults;
                //options.ResultsListAuthorize = request => MyGetUserFunction(request).CanSeeMiniProfiler;

                // To control which requests are profiled, use the Func<HttpRequest, bool> option:
                //options.ShouldProfile = request => MyShouldThisBeProfiledFunction(request);

                // Profiles are stored under a user ID, function to get it:
                //options.UserIdProvider =  request => MyGetUserIdFunction(request);

                // Optionally swap out the entire profiler provider, if you want
                // The default handles async and works fine for almost all applications
                //options.ProfilerProvider = new MyProfilerProvider();

                // Optionally disable "Connection Open()", "Connection Close()" (and async variants).
                //options.TrackConnectionOpenClose = false;);
            });
            services.AddSingleton<IPostcodeApiConfiguration>(postcodeConfig);
            services.AddSingleton<IMappingConfiguration>(mappingConfig);
            services.AddTransient<IApprenticeshipProgrammeApiClient>(client => new ApprenticeshipProgrammeApiClient(Configuration["FatSharedComponents:FatApiBaseUrl"]));
            services.AddTransient<IStandardsMapper, StandardsMapper>();
            services.AddTransient<IStandardsRepository, StandardsRepository>();
            services.AddTransient<IVacanciesMapper, VacanciesMapper>();
            services.AddTransient<IVacanciesRepository, VacanciesRepository>();
            services.AddTransient<ICountryMapper, CountryMapper>();

            services.AddRefitClient<IApprenticeshipStandardsApi>()
                .ConfigureHttpClient(c => c.BaseAddress = Configuration.GetValue<Uri>("IfaStandardsApiUrl"));

            var vacanciesBaseUrl = Configuration.GetValue<string>("VacanciesApi:BaseUrl");
            var vacanciesHttpClient = new HttpClient() { BaseAddress = new Uri(vacanciesBaseUrl) };
            vacanciesHttpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Configuration.GetValue<string>("VacanciesApi:ApiKey"));

            services.AddTransient<ILivevacanciesAPI>(client => new LivevacanciesAPI(vacanciesHttpClient, false){BaseUri = new Uri(vacanciesBaseUrl)  });
            services.AddTransient<IGeocodeService, GeocodeService>();
            services.AddTransient<IRetryWebRequests, WebRequestRetryService>();
            services.AddTransient<IMappingService, GoogleMappingService>();
            services.AddTransient(typeof(IQueueService<>), typeof(AzureQueueService<>));
            services.AddTransient<IUserDataCollection, UserDataCollection>();
            services.AddTransient<IUserDataCollectionValidator, UserDataCollectionValidator>();
            services.AddTransient<IUserDataCryptographyService, UserDataCryptographyService>();
            services.AddTransient<ICacheStorageService, CacheStorageService>();
            services.AddTransient<Sfa.Das.Sas.ApplicationServices.Services.ICacheStorageService, Sfa.Das.Sas.ApplicationServices.Services.CacheStorageService>();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            services.AddMemoryCache();

            var fatConfig = new FatSharedComponentsConfiguration();
            Configuration.Bind("fatSharedComponents", fatConfig);
            services.AddSingleton<IFatConfigurationSettings>(fs => fatConfig);

            services.AddFatSharedComponents(fatConfig);

            services.AddTransient<ICssViewModel, CampaignCssClasses>();

            services.AddApplicationInsightsTelemetry(Configuration["APPINSIGHTS_INSTRUMENTATIONKEY"]);


            if (Configuration["Environment"] == "LOCAL")
            {
                services.AddDistributedMemoryCache();
            }
            else
            {
                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = connectionStrings.SharedRedis;
                });

                healthChecks.AddRedis(connectionStrings.SharedRedis, "redis-app-cache-check");
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // By defualt app services have en-US locale set no matter what Region is being used.
            var cultureInfo = new CultureInfo("en-GB");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            app.UseStatusCodePagesWithReExecute("/error/{0}");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMiniProfiler();
            }
            else
            {
                app.UseExceptionHandler("/Error/Error");
                app.UseHsts();
            }

            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = HealthCheckResponseWriter.WriteJsonResponse
            });

            app.UseStaticFiles();
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                Secure = CookieSecurePolicy.Always
            });

            app.Use(async (context, next) =>
            {
                if (context.Response.Headers.ContainsKey("X-Frame-Options"))
                {
                    context.Response.Headers.Remove("X-Frame-Options");
                }

                context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");

                await next();
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "Fat",
                    "employer/find-apprenticeships/{action=Search}/{keywords?}",
                    new { controller = "Fat" });
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
