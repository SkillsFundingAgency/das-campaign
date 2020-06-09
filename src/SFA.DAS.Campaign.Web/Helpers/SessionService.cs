using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SFA.DAS.Campaign.Web.Helpers
{
    public interface ISessionService
    {
        T Get<T>(string key);
        void Set<T>(string key, T toStore);
        string LevyOptionKey { get; }
    }

    public class SessionService : ISessionService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ILogger<SessionService> _logger;

        public string LevyOptionKey => "LevyOption";

        public SessionService(IHttpContextAccessor contextAccessor, ILogger<SessionService> logger)
        {
            _contextAccessor = contextAccessor;
            _logger = logger;
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
            catch (JsonSerializationException ex)
            {
                _logger.LogError(ex,"Attempted to deserialize json from session for key '{0}', and was not successful.", key);

                return default(T);
            }
        }

        public void Set<T>(string key, T toStore)
        {
            _contextAccessor.HttpContext.Session.SetString(key, JsonConvert.SerializeObject(toStore));
        }
    }
}