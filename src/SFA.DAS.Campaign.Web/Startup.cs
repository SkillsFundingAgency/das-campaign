using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.Apprenticeships.Api.Client;
using SFA.DAS.Campaign.Application.ApprenticeshipCourses.Services;
using SFA.DAS.Campaign.Application.Core;
using SFA.DAS.Campaign.Application.Geocode;
using SFA.DAS.Campaign.Application.Vacancies;
using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;
using SFA.DAS.Campaign.Domain.Configuration;
using SFA.DAS.Campaign.Domain.Configuration.Models;
using SFA.DAS.Campaign.Domain.Geocode;
using SFA.DAS.Campaign.Domain.Vacancies;
using VacanciesApi;

namespace SFA.DAS.Campaign.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
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

            var postcodeConfig = new PostcodeApiConfiguration();
            Configuration.Bind("Postcode",postcodeConfig);
            
        
            services.AddSingleton<IPostcodeApiConfiguration>(postcodeConfig);
            services.AddTransient<IApprenticeshipProgrammeApiClient>(
                client => new ApprenticeshipProgrammeApiClient(Configuration["ApprenticeshipBaseUrl"]));
            services.AddTransient<IStandardsMapper, StandardsMapper>();
            services.AddTransient<IStandardsService, StandardsService>();
            services.AddTransient<IVacanciesMapper, VacanciesMapper>();
            services.AddTransient<IVacanciesService, VacanciesService>();

            var vacanciesHttpClient = new HttpClient(){BaseAddress = new Uri("https://apis.apprenticeships.sfa.bis.gov.uk/vacancies") };
            vacanciesHttpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "a38ac93176f04689a7d6cb3b53e60033");

            services.AddTransient<ILivevacanciesAPI>(client => new LivevacanciesAPI(vacanciesHttpClient,false));
            services.AddTransient<IGeocodeService, GeocodeService>();
            services.AddTransient<IRetryWebRequests, WebRequestRetryService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
