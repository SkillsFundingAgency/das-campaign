using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.Apprenticeships.Api.Client;
using SFA.DAS.Campaign.Application.ApprenticeshipCourses.Services;
using SFA.DAS.Campaign.Application.DataCollection.Services;
using SFA.DAS.Campaign.Application.DataCollection.Validation;
using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;
using SFA.DAS.Campaign.Domain.DataCollection;
using SFA.DAS.Campaign.Infrastructure.Configuration;
using SFA.DAS.Campaign.Infrastructure.Queue;
using SFA.DAS.Campaign.Models.Configuration;

namespace SFA.DAS.Campaign.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .AddAzureTableStorageConfiguration(
                    builder["ConfigurationStorageConnectionString"],
                    builder["Environment"],
                    builder["Version"]
                    )
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

            services.AddTransient<IApprenticeshipProgrammeApiClient>(client => new ApprenticeshipProgrammeApiClient(Configuration["ApprenticeshipBaseUrl"]));
            services.AddTransient<IStandardsMapper, StandardsMapper>();
            services.AddTransient<IStandardsService, StandardsService>();
            services.AddTransient(typeof(IQueueService<>), typeof(AzureQueueService<>));
            services.AddTransient<IUserDataCollection, UserDataCollection>();
            services.AddTransient<IUserDataCollectionValidator, UserDataCollectionValidator>();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddApplicationInsightsTelemetry(Configuration["APPINSIGHTS_INSTRUMENTATIONKEY"]);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
