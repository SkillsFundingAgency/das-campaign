using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
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
using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Infrastructure.Api;
using SFA.DAS.Campaign.Infrastructure.Api.Queries;
using StackExchange.Redis;
using SFA.DAS.Campaign.Web.Helpers;
using VacanciesApi;

namespace SFA.DAS.Campaign.Web
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")	                
                .AddJsonFile("appsettings.Development.json",true)	                
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddOptions();
            services.AddHttpClient<IApiClient, ApiClient>();


            services.ConfigureSfaConfigurations(Configuration);
            services.ConfigureSfaConnectionStrings(Configuration);
            services.ConfigureSfaVacancies(Configuration);
            services.ConfigureSfaServices();
            services.ConfigureSfaRepositories();
            services.ConfigureSfaMappers();
            services.ConfigureSfaDataCollection();
            services.ConfigureFactorys();
            services.ConfigureJsonConverters();
            services.AddMediatR(typeof(GetArticleQuery).Assembly);
            
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddLogging();

            services.AddApplicationInsightsTelemetry(Configuration["APPINSIGHTS_INSTRUMENTATIONKEY"]);

            
            
            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var cultureInfo = new CultureInfo("en-GB");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            app.UseStatusCodePagesWithReExecute("/error/{0}");
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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

            app.UseRouting();
            
            app.UseSession();
            
            app.UseEndpoints(builder =>
            {
                builder.MapControllerRoute(
                    "Fat",
                    "employer/find-apprenticeships/{action=Search}",
                    new { controller = "Fat" });
                builder.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                
            });
        }
    }
}
