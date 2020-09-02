using System.Threading.Tasks;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace SFA.DAS.Campaign.Domain.Content
{
    public class ContentService : IContentService
    {
        private readonly IDatabase _redisDatabase;

        public ContentService(IDatabase redisDatabase)
        {
            _redisDatabase = redisDatabase;
        }
        
        public async Task<Page<T>> GetPage<T>(string slug) where T : IContent
        {
            var contentType = typeof(T).Name.ToLower();
            var contentEntry = (await _redisDatabase.StringGetAsync($"{contentType}_{slug}")).ToString();

            return contentEntry is null ? null : JsonConvert.DeserializeObject<Page<T>>(contentEntry);
        }
    }
}