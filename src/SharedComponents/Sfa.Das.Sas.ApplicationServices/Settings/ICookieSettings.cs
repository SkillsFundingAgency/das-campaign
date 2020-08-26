namespace Sfa.Das.Sas.ApplicationServices.Settings
{
    public interface ICookieSettings
    {
        bool UseSecureCookies { get; }

        string CookieDomain { get; }
    }
}