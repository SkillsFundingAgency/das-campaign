using System;

namespace Sfa.Das.Sas.Shared.Components.Cookies
{
    public interface ICookieManager
    {
        string Get(string cookieName);
        void Set(string cookieName, string value, DateTimeOffset? expiry);
    }
}
