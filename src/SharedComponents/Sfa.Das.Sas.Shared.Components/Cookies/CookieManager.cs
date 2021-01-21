using Microsoft.AspNetCore.Http;
using System;

namespace Sfa.Das.Sas.Shared.Components.Cookies
{
    public class CookieManager : ICookieManager
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CookieManager(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string Get(string cookieName)
        {
            return _contextAccessor.HttpContext.Request.Cookies[cookieName];
        }

        public void Set(string cookieName, string value, DateTimeOffset? expiry)
        {
            CookieOptions option = new CookieOptions
            {
                HttpOnly = true,
                IsEssential = true,
                Secure = true,
                Expires = expiry
            };

            _contextAccessor.HttpContext.Response.Cookies.Append(cookieName, value, option);
        }
    }
}
