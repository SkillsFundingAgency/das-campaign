using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;

namespace SFA.DAS.Campaign.Web.MiddleWare
{
    public static class AddRedirectRulesExtension
    {
        public static void AddRedirectRules(this IApplicationBuilder app)
        {
            var options = new RewriteOptions();
            //Courses
            options.Add(new PathWithQueryRule(@"(?i)apprentices\b\/(starting-apprenticeship|your-apprenticeship)", "apprentices/starting-apprenticeship"));
            options.AddRedirect(@"(?i)apprentices\b\/(starting-apprenticeship|your-apprenticeship)", "apprentices/starting-apprenticeship", (int)HttpStatusCode.PermanentRedirect);
            //options.AddRedirect("(?i)provider/frameworkresults", "courses", (int)HttpStatusCode.PermanentRedirect);
            //options.AddRedirect("(?i)apprenticeship/searchforframeworkproviders", "courses", (int)HttpStatusCode.PermanentRedirect);
            //options.AddRedirect("(?i)apprenticeship/framework/(.*)", "courses", (int)HttpStatusCode.PermanentRedirect);

            ////Course Provider
            //options.Add(new PathWithQueryRule("(?i)provider/detail\\b", "/courses/$0/providers/$1?location=$2", new List<string> { "standardCode", "ukprn", "postcode" }));

            ////Course Providers
            //options.Add(new PathWithQueryRule("(?i)provider/standardresults\\b", "/courses/$0/providers?location={1}", new List<string> { "apprenticeshipid", "postcode" }));
            //options.Add(new PathWithQueryRule("(?i)Apprenticeship/SearchForStandardProviders\\b", "/courses/$0/providers", new List<string> { "standardId" }));

            ////Home
            //options.AddRedirect("(?i)apprenticeshiporprovider", "/", (int)HttpStatusCode.PermanentRedirect);
            //options.AddRedirect("(?i)apprenticeship/apprenticeshiporprovider", "/", (int)HttpStatusCode.PermanentRedirect);
            //options.AddRedirect("(?i)provider/(.*)", "/", (int)HttpStatusCode.PermanentRedirect);

            ////Course
            //options.AddRedirect("(?i)apprenticeship/standard/(.*)", "courses/$1", (int)HttpStatusCode.PermanentRedirect);

            app.UseRewriter(options);
        }
    }
}
