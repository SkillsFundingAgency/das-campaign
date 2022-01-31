using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using WireMock.Logging;
using WireMock.Net.StandAlone;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;

namespace SFA.DAS.Campaign.MockServer
{
    public static class MockApiServer
    {
        public static IWireMockServer Start()
        {
            var settings = new WireMockServerSettings
            {
                Port = 5016,
                Logger = new WireMockConsoleLogger()
            };

            var server = StandAloneApp.Start(settings);

            AddLandingPageResponses(server);
            AddArticlePageResponses(server);
            AddHubPageResponses(server);
            AddMenuResponses(server);
            AddSiteMapResponses(server);
            AddSectorsResponses(server);
            AddTrainingCoursesResponses(server);
            AddVacanciesResponses(server);
            AddBannerResponses(server);
            return server;
        }

        private static void AddLandingPageResponses(WireMockServer server)
        {
            server.Given(Request.Create()
                    .WithPath(o => Regex.IsMatch(o, "/landingpage/\\S+/\\S+"))
                    .UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithHeader("Content-Type", "application/json")
                    .WithBodyFromFile($"{Directory.GetCurrentDirectory()}/json/landing-page-data-response.json"));
        }

        private static void AddArticlePageResponses(WireMockServer server)
        {
            server.Given(Request.Create()
                    .WithPath(o => Regex.IsMatch(o, "/article/\\S+/\\S+"))
                    .UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithHeader("Content-Type", "application/json")
                    .WithBodyFromFile($"{Directory.GetCurrentDirectory()}/json/article-data-response.json"));
        }

        private static void AddHubPageResponses(WireMockServer server)
        {
            server.Given(Request.Create()
                    .WithPath(o => Regex.IsMatch(o, "/hub/\\S+"))
                    .UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithHeader("Content-Type", "application/json")
                    .WithBodyFromFile($"{Directory.GetCurrentDirectory()}/json/hub-data-response.json"));
        }

        private static void AddMenuResponses(WireMockServer server)
        {
            server.Given(Request.Create()
                    .WithPath(o => string.Compare(o, "/menu", StringComparison.OrdinalIgnoreCase) == 0)
                    .UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithHeader("Content-Type", "application/json")
                    .WithBodyFromFile($"{Directory.GetCurrentDirectory()}/json/menu-data-response.json"));
        }

        private static void AddBannerResponses(WireMockServer server)
        {
            server.Given(Request.Create()
                    .WithPath(o => string.Compare(o, "/banner", StringComparison.OrdinalIgnoreCase) == 0)
                    .UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithHeader("Content-Type", "application/json")
                    .WithBodyFromFile($"{Directory.GetCurrentDirectory()}/json/banner-data-response.json"));
        }

        private static void AddSiteMapResponses(WireMockServer server)
        {
            server.Given(Request.Create()
                    .WithPath(o => string.Compare(o, "/sitemap", StringComparison.OrdinalIgnoreCase) == 0)
                    .UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithHeader("Content-Type", "application/json")
                    .WithBodyFromFile($"{Directory.GetCurrentDirectory()}//json/sitemap-data-response.json"));
        }

        private static void AddSectorsResponses(WireMockServer server)
        {
            server.Given(Request.Create()
                    .WithPath(o => string.Compare(o, "/sectors", StringComparison.OrdinalIgnoreCase) == 0)
                    .UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithHeader("Content-Type", "application/json")
                    .WithBodyFromFile($"{Directory.GetCurrentDirectory()}//json/sectors-data-response.json"));

        }

        private static void AddTrainingCoursesResponses(WireMockServer server)
        {
            server.Given(Request.Create()
                    .WithUrl(o => o.Contains("/trainingcourses?sector=",
                        StringComparison.OrdinalIgnoreCase))
                    .UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithHeader("Content-Type", "application/json")
                    .WithBodyFromFile($"{Directory.GetCurrentDirectory()}//json/training-courses-data-response.json"));

        }

        private static void AddVacanciesResponses(WireMockServer server)
        {
            server.Given(Request.Create()
                    .WithUrl(o => o.Contains("/adverts"))
                    .UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithHeader("Content-Type", "application/json")
                    .WithBodyFromFile($"{Directory.GetCurrentDirectory()}//json/vacancies-api-data-response.json"));

        }
    }
} 