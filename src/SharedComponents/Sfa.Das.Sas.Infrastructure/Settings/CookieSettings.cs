namespace Sfa.Das.Sas.Infrastructure.Settings
{
    using System;
    using System.Configuration;

    using Sfa.Das.Sas.ApplicationServices.Settings;

    public sealed class CookieSettings : ICookieSettings
    {
        // Will set the use secure cookies to true if no valid false value is found (secure by default)
        public bool UseSecureCookies => !ConfigurationManager.AppSettings["cookie.secure"]
                                             .Equals("false", StringComparison.InvariantCultureIgnoreCase);

        public string CookieDomain => ConfigurationManager.AppSettings["cookie.domain"];
    }
}