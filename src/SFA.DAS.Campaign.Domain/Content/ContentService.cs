using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace SFA.DAS.Campaign.Domain.Content
{
    public class ContentService : IContentService
    {
        private readonly IDatabase _redisDatabase;
        private readonly ConnectionMultiplexer _redisConnection;

        public ContentService(IDatabase redisDatabase, ConnectionMultiplexer redisConnection)
        {
            _redisDatabase = redisDatabase;
            _redisConnection = redisConnection;
        }
        
        public async Task<Page<T>> GetPage<T>(string slug) where T : IContent
        {
            var contentType = typeof(T).Name.ToLower();
            var contentEntry = (await _redisDatabase.StringGetAsync($"{contentType}_{slug}")).ToString();

            return contentEntry is null ? null : JsonConvert.DeserializeObject<Page<T>>(contentEntry);
        }

        public List<ArticleCard> GetArticleCardsFor(string landingPageSlug, HubType hubType)
        {
            var endpoint = _redisConnection.GetEndPoints()[0];
            var server = _redisConnection.GetServer(endpoint);

            var articleCardKeys = server.Keys(pattern: "articleCard_*");

            var articleCards = articleCardKeys.Select(articleCardKey => JsonConvert.DeserializeObject<ArticleCard>(_redisDatabase.StringGet(articleCardKey))).ToList();

            return articleCards.Where(ac => ac.HubType == hubType && ac.LandingPageSlug == landingPageSlug).ToList();
        }
    }
}