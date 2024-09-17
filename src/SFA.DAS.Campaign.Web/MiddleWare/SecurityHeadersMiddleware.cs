using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Web.MiddleWare
{
    public class SecurityHeadersMiddleware
    {
        private readonly RequestDelegate next;
        public SecurityHeadersMiddleware(RequestDelegate next) => this.next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            var authority = context.Request.GetUri().Authority;
            const string dasCdn = " das-at-frnt-end.azureedge.net das-pp-frnt-end.azureedge.net das-mo-frnt-end.azureedge.net das-test-frnt-end.azureedge.net das-test2-frnt-end.azureedge.net das-prd-frnt-end.azureedge.net";
            
            context.Response.Headers.AddIfNotPresent("x-frame-options", new StringValues("DENY"));
            context.Response.Headers.AddIfNotPresent("x-content-type-options", new StringValues("nosniff"));
            context.Response.Headers.AddIfNotPresent("X-Permitted-Cross-Domain-Policies", new StringValues("none"));
            context.Response.Headers.AddIfNotPresent("x-xss-protection", new StringValues("0"));
            context.Response.Headers.AddIfNotPresent(
                "Content-Security-Policy",
                new StringValues(
                    $"script-src 'self' 'unsafe-inline' 'unsafe-eval' {dasCdn} https://*.twitter.com *.snapchat.com snap.licdn.com static.ads-twitter.com sc-static.net munchkin.marketo.net acdn.adnxs.com www.youtube.com connect.facebook.net bat.bing.com *.typekit.net https://consent-api-bgzqvpmbyq-nw.a.run.app https://www.googletagmanager.com https://tagmanager.google.com https://www.google-analytics.com https://ssl.google-analytics.com https://*.zdassets.com https://*.clarity.ms https://c.bing.com https://*.zopim.com https://*.rcrsv.io; " +
                    $"style-src 'self' 'unsafe-inline' {dasCdn} https://*.fontawesome.com/ *.typekit.net https://tagmanager.google.com https://fonts.googleapis.com https://*.rcrsv.io ; " +
                    $"img-src {dasCdn} {authority} images.ctfassets.net https://*.doubleclick.net https://t.co ib.adnxs.com www.facebook.com bat.bing.com https://*.linkedin.com www.facebook.com https://analytics.twitter.com https://*.google.com https://*.google.co.uk www.googletagmanager.com https://ssl.gstatic.com https://www.gstatic.com https://*.google-analytics.com ; " +
                    $"font-src {dasCdn} *.typekit.net https://fonts.gstatic.com https://*.rcrsv.io data: ;" +
                    $"connect-src wss://localhost:* bat.bing.com https://t.co https://*.mktoresp.com https://*.doubleclick.net https://*.linkedin.com https://*.snapchat.com https://consent-api-bgzqvpmbyq-nw.a.run.app https://*.google-analytics.com https://*.zendesk.com https://*.zdassets.com wss://*.zopim.com https://*.rcrsv.io ;"));

            await next(context);
        }
    }
}
