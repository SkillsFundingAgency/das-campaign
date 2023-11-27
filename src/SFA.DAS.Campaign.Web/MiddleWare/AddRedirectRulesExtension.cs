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
            
            options.Add(new PathWithQueryRule(@"(?i)apprentice\b\/(starting-apprenticeship|your-apprenticeship)", "/apprentices/starting-apprenticeship"));
            options.AddRedirect(@"(?i)apprentice\b\/(starting-apprenticeship|your-apprenticeship)", "/apprentices/starting-apprenticeship", (int)HttpStatusCode.PermanentRedirect);

            options.Add(new PathWithQueryRule(@"(?i)apprentice\b\/(interview)", "/apprentices/interview-process"));
            options.AddRedirect(@"(?i)apprentice\b\/(interview)", "/apprentices/interview-process", (int)HttpStatusCode.PermanentRedirect);

            options.Add(new PathWithQueryRule(@"(?i)apprentice\b\/(application|application-process)", "/apprentices/applying-apprenticeship"));
            options.AddRedirect(@"(?i)apprentice\b\/(application|application-process)", "/apprentices/applying-apprenticeship", (int)HttpStatusCode.PermanentRedirect);

            options.Add(new PathWithQueryRule(@"(?i)apprentice\b\/(what-is-apprenticeship|what-is-an-apprenticeship)", "/apprentices/becoming-apprentice"));
            options.AddRedirect(@"(?i)apprentice\b\/(what-is-apprenticeship|what-is-an-apprenticeship)", "/apprentices/becoming-apprentice", (int)HttpStatusCode.PermanentRedirect);

            options.Add(new PathWithQueryRule(@"(?i)apprentice\b\/(what-are-the-benefits-for-me)", "/apprentices/benefits-apprenticeship"));
            options.AddRedirect(@"(?i)apprentice\b\/(what-are-the-benefits-for-me)", "/apprentices/benefits-apprenticeship", (int)HttpStatusCode.PermanentRedirect);

            options.Add(new PathWithQueryRule(@"(?i)apprentice\b\/(assessment-and-certification)", "/apprentices/assessment-and-certification"));
            options.AddRedirect(@"(?i)apprentice\b\/(assessment-and-certification)", "/apprentices/assessment-and-certification", (int)HttpStatusCode.PermanentRedirect);

            options.Add(new PathWithQueryRule(@"(?i)apprentice\b\/(find-an-apprenticeship)", "/apprentices/browse-apprenticeships"));
            options.AddRedirect(@"(?i)apprentice\b\/(find-an-apprenticeship)", "/apprentices/browse-apprenticeships", (int)HttpStatusCode.PermanentRedirect);

            options.Add(new PathWithQueryRule(@"(?i)employer\b\/(how-much-is-it-going-to-cost|funding-an-apprenticeship)", "/employers/funding-an-apprenticeship"));
            options.AddRedirect(@"(?i)employer\b\/(how-much-is-it-going-to-cost|funding-an-apprenticeship)", "/employers/funding-an-apprenticeship", (int)HttpStatusCode.PermanentRedirect);

            options.Add(new PathWithQueryRule(@"(?i)employer\b\/(the-right-apprenticeship)", "/employers/choose-apprenticeship-training"));
            options.AddRedirect(@"(?i)employer\b\/(the-right-apprenticeship)", "/employers/choose-apprenticeship-training", (int)HttpStatusCode.PermanentRedirect);

            options.Add(new PathWithQueryRule(@"(?i)employer\b\/(choose-training-provider)", "/employers/choose-training-provider"));
            options.AddRedirect(@"(?i)employer\b\/(choose-training-provider)", "/employers/choose-training-provider", (int)HttpStatusCode.PermanentRedirect);

            options.Add(new PathWithQueryRule(@"(?i)employer\b\/(hire-an-apprentice|hiring-an-apprentice)", "/employers/hiring-an-apprentice"));
            options.AddRedirect(@"(?i)employer\b\/(hire-an-apprentice|hiring-an-apprentice)", "/employers/hiring-an-apprentice", (int)HttpStatusCode.PermanentRedirect);

            options.Add(new PathWithQueryRule(@"(?i)employer\b\/(end-point-assessments|assessment-and-certification)", "/employers/end-point-assessments"));
            options.AddRedirect(@"(?i)employer\b\/(end-point-assessments|assessment-and-certification)", "/employers/end-point-assessments", (int)HttpStatusCode.PermanentRedirect);

            options.Add(new PathWithQueryRule(@"(?i)employer\b\/(benefits)", "/employers/benefits-of-hiring-apprentice"));
            options.AddRedirect(@"(?i)employer\b\/(benefits)", "/employers/benefits-of-hiring-apprentice", (int)HttpStatusCode.PermanentRedirect);

            options.Add(new PathWithQueryRule(@"(?i)employer\b\/(training-your-apprentice)", "/employers/training-your-apprentice"));
            options.AddRedirect(@"(?i)employer\b\/(training-your-apprentice)", "/employers/training-your-apprentice", (int)HttpStatusCode.PermanentRedirect);

            options.Add(new PathWithQueryRule(@"(?i)employer\b\/(upskill)", "/employers/upskilling-your-workforce"));
            options.AddRedirect(@"(?i)employer\b\/(upskill)", "/employers/upskilling-your-workforce", (int)HttpStatusCode.PermanentRedirect);

            options.Add(new PathWithQueryRule(@"(?i)employers\b\/(upskill)", "/employers/upskilling-your-workforce"));
            options.AddRedirect(@"(?i)employer\b\/(upskill)", "/employers/upskilling-your-workforce", (int)HttpStatusCode.PermanentRedirect);

            options.Add(new PathWithQueryRule(@"(?i)real\-stories\b\/(apprentice)", "/apprentices/real-stories"));
            options.AddRedirect(@"(?i)real\-stories\b\/(apprentice)$", "/apprentices/real-stories", (int)HttpStatusCode.PermanentRedirect);

            options.Add(new PathWithQueryRule(@"(?i)apprentice\b\/(real\-stories)$", "/apprentices/real-stories"));
            options.AddRedirect(@"(?i)apprentice\b\/(real\-stories)$", "/apprentices/real-stories", (int)HttpStatusCode.PermanentRedirect);

            options.Add(new PathWithQueryRule(@"(?i)real\-stories\b\/(employer)$", "/employers/real-stories-employers"));
            options.AddRedirect(@"(?i)real\-stories\b\/(employer)$", "/employers/real-stories-employers", (int)HttpStatusCode.PermanentRedirect);

            options.Add(new PathWithQueryRule(@"(?i)employer\b\/(real\-stories)$", "/employers/real-stories-employers"));
            options.AddRedirect(@"(?i)employer\b\/(real\-stories)$", "/employers/real-stories-employers", (int)HttpStatusCode.PermanentRedirect);

            options.Add(new PathWithQueryRule(@"(?i)employers\b\/(real\-stories)$", "/employers/real-stories-employers"));
            options.AddRedirect(@"(?i)employers\b\/(real\-stories)$", "/employers/real-stories-employers", (int)HttpStatusCode.PermanentRedirect);

            options.Add(new PathWithQueryRule(@"(?i)parents\b\/(their\-career)$", "/apprentices/help-shape-their-career"));
            options.AddRedirect(@"(?i)parents\b\/(their\-career)$", "/apprentices/help-shape-their-career", (int)HttpStatusCode.PermanentRedirect);

            options.Add(new PathWithQueryRule(@"(?i)influencers\b\/(become\-an\-ambassador)$", "/influencers/what-is-the-aan"));
            options.AddRedirect(@"(?i)influencers\b\/(become\-an\-ambassador)$", "/influencers/what-is-the-aan", (int)HttpStatusCode.PermanentRedirect);

            options.Add(new PathWithQueryRule(@"(?i)apprentices\b\/(real\-stories)$", "/apprentices/apprentice-stories"));
            options.AddRedirect(@"(?i)apprentices\b\/(real\-stories)$", "/apprentices/apprentice-stories", (int)HttpStatusCode.PermanentRedirect);

            options.Add(new PathWithQueryRule(@"(?i)apprentices\b\/(becoming\-apprentice)$", "/apprentices/about-apprenticeships"));
            options.AddRedirect(@"(?i)apprentices\b\/(becoming\-apprentice)$", "/apprentices/about-apprenticeships", (int)HttpStatusCode.PermanentRedirect);

            options.Add(new PathWithQueryRule(@"(?i)apprentices\b\/(assessment\-and\-certification)$", "/apprentices/understanding-end-point-assessments"));
            options.AddRedirect(@"(?i)apprentices\b\/(assessment\-and\-certification)$", "/apprentices/understanding-end-point-assessments", (int)HttpStatusCode.PermanentRedirect);

            options.Add(new PathWithQueryRule(@"(?i)apprentices\b\/(create\-account)$", "/apprentices/how-to-find-apprenticeships"));
            options.AddRedirect(@"(?i)apprentices\b\/(create\-account)$", "/apprentices/how-to-find-apprenticeships", (int)HttpStatusCode.PermanentRedirect);

            options.Add(new PathWithQueryRule(@"(?i)apprentices\b\/(useful\-resources\-for\-apprentices)$", "/apprentices/support-centre"));
            options.AddRedirect(@"(?i)apprentices\b\/(useful\-resources\-for\-apprentices)$", "/apprentices/support-centre", (int)HttpStatusCode.PermanentRedirect);

            app.UseRewriter(options);
        }
    }
}
