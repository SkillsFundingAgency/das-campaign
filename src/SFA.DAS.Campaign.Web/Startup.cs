using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.Campaign.Web.HealthChecks;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using MediatR;
using Microsoft.Extensions.Hosting;
using Polly;
using Polly.Extensions.Http;
using SFA.DAS.Campaign.Application.Content.Queries;
using SFA.DAS.Campaign.Domain.Api.Interfaces;
using SFA.DAS.Campaign.Infrastructure.Api;
using SFA.DAS.Campaign.Web.Helpers;
using SFA.DAS.Campaign.Web.MiddleWare;
using SFA.DAS.Configuration.AzureTableStorage;
using System.Threading.RateLimiting;
using Microsoft.Extensions.Logging;

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
                .AddJsonFile("appsettings.Development.json", true)
                .AddEnvironmentVariables()
                .AddUserSecrets<Startup>();

            if (!configuration["Environment"].Equals("DEV", StringComparison.CurrentCultureIgnoreCase))
            {

#if DEBUG
                config
                    .AddJsonFile("appsettings.json", true)
                    .AddJsonFile("appsettings.Development.json", true);
#endif

                config.AddAzureTableStorage(options =>
                    {
                        options.ConfigurationKeys = configuration["ConfigNames"].Split(",");
                        options.StorageConnectionString = configuration["ConfigurationStorageConnectionString"];
                        options.EnvironmentName = configuration["Environment"];
                        options.PreFixConfigurationKeys = false;
                    }
                );
            }

            Configuration = config.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddRateLimiter(options =>
            {
                options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                {
                    var ipAddress = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault()
                                    ?? httpContext.Connection.RemoteIpAddress?.ToString()
                                    ?? "unknown";

                    return RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: ipAddress,
                        factory: partition => new FixedWindowRateLimiterOptions
                        {
                            AutoReplenishment = true,
                            PermitLimit = 5, // Max 5 requests
                            QueueLimit = 0,   // No queueing of requests
                            Window = TimeSpan.FromMinutes(1) // 1-minute window
                        });
                });

                // Optional: Configure on-rejection behavior, logging, etc.
                options.OnRejected = async (context, token) =>
                {
                    // Log the rejection for debugging/monitoring
                    var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Startup>>();
                    logger.LogWarning($"Rate limit exceeded for IP: {context.HttpContext.Connection.RemoteIpAddress}");

                    // Return 429 Too Many Requests
                    context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    await context.HttpContext.Response.WriteAsync("Too many requests. Please try again later.");
                };
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddOptions();
            services.AddHttpClient<IApiClient, ApiClient>().AddPolicyHandler(HttpClientRetryPolicy());



            services.ConfigureSfaConfigurations(Configuration);
            services.ConfigureSfaConnectionStrings(Configuration);
            services.ConfigureSfaServices();
            services.ConfigureSfaRepositories();
            services.ConfigureSfaDataCollection();
            services.ConfigureFactorys();
            services.ConfigureJsonConverters();
            services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(GetArticleQuery).Assembly));

            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
            }).AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddLogging();

            services.AddApplicationInsightsTelemetry(Configuration["APPINSIGHTS_INSTRUMENTATIONKEY"]);

            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

#if DEBUG
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
#endif



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            app.Use(async (context, next) =>
            {
                // Extract the IP address from the request
                var ipAddress = context.Request.Headers["X-Forwarded-For"].FirstOrDefault()
                                ?? context.Connection.RemoteIpAddress?.ToString()
                                ?? "unknown";

                // Log the IP address
                logger.LogInformation($"Request from IP Address: {ipAddress}");

                await next(); // Call the next middleware
            });

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

            app.AddRedirectRules();

            app.UseRouting();

            app.UseRateLimiter();

            app.UseMiddleware<SecurityHeadersMiddleware>();

            app.UseSession();

            app.UseEndpoints(builder =>
            {
                builder.MapControllerRoute(
                    "Fat",
                    "employer/find-apprenticeships/{action=Search}",
                    new { controller = "Fat" });
                builder.MapControllerRoute(
                    name: "default",
                    pattern: "sitemap.xml",
                    new { controller = "Home", action = "sitemap" });
                builder.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });
        }

        private static IAsyncPolicy<HttpResponseMessage> HttpClientRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,
                    retryAttempt)));
        }
    }
}
