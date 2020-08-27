using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace SFA.DAS.Campaign.Domain.Content
{
    public class ContentService : IContentService
    {
        private readonly IDatabase _redisDatabase;
        private readonly ILogger<ContentService> _logger;

        public ContentService(IDatabase redisDatabase, ILogger<ContentService> logger)
        {
            _redisDatabase = redisDatabase;
            _logger = logger;
        }
        
        public async Task<Page<T>> GetPage<T>(string slug) where T : IContent
        {
            var contentType = typeof(T).Name.ToLower();
            _logger.LogInformation($"ContentType: {contentType}");
            var contentEntry = await _redisDatabase.StringGetAsync($"{contentType}_{slug}");
            _logger.LogInformation($"ContentEntry: {contentEntry}");
            
            return JsonConvert.DeserializeObject<Page<T>>(contentEntry);
        }
    }
}