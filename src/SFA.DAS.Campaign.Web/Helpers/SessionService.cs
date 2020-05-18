using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace SFA.DAS.Campaign.Web.Helpers
{
    public interface ISessionService
    {
        T Get<T>(string key);
        void Set<T>(string key, T toStore);
        string LevyOptionViewModelKey { get; }
    }

    public class SessionService : ISessionService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public string LevyOptionViewModelKey => "LevyOptionViewModel";

        public SessionService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }


        public T Get<T>(string key)
        {
            var jsonString = _contextAccessor.HttpContext.Session.GetString(key);

            if (jsonString == null)
            {
                return default(T);
            }

            try
            {
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            catch
            {
                return default(T);
            }
        }

        public void Set<T>(string key, T toStore)
        {
            _contextAccessor.HttpContext.Session.SetString(key, JsonConvert.SerializeObject(toStore));
        }
    }
}