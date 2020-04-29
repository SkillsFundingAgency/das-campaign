using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace SFA.DAS.Campaign.Web.Helpers
{
    public interface ISessionService
    {
        T Get<T>(string key);
        void Set<T>(string key, T toStore);
    }

    public class SessionService : ISessionService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public SessionService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        
        public T Get<T>(string key)
        {
            return _contextAccessor.HttpContext.Session.GetString(key) != null 
                ? JsonConvert.DeserializeObject<T>(_contextAccessor.HttpContext.Session.GetString(key)) 
                : default(T);
        }

        public void Set<T>(string key, T toStore)
        {
            _contextAccessor.HttpContext.Session.SetString(key, JsonConvert.SerializeObject(toStore));
        }
    }
}