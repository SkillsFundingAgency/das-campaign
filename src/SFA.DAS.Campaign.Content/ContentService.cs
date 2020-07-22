using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contentful.Core;
using Contentful.Core.Search;
using SFA.DAS.Campaign.Content.ContentTypes;

namespace SFA.DAS.Campaign.Content
{
    public class ContentService : IContentService
    {
        private readonly IContentfulClient _contentfulClient;

        public ContentService(IContentfulClient contentfulClient)
        {
            _contentfulClient = contentfulClient;
        }
        
        public async Task<T> GetContentById<T>(string id, int includeLevel = 1)  where T : ContentBase
        {
            var builder = new QueryBuilder<T>().FieldEquals(f => f.Sys.Id, id).Include(includeLevel);
            return (await _contentfulClient.GetEntries(builder)).FirstOrDefault();
        }

        public async Task<T> GetContentByHubAndSlug<T>(string hub, string slug, int includeLevel = 1) where T : ContentBase
        {
            var builder = QueryBuilder<T>.New
                .FieldEquals(i => i.Slug, slug)
                .FieldEquals(i => i.Hub, hub)
                .Include(includeLevel);

            var content = (await _contentfulClient.GetEntriesByType(typeof(T).Name.FirstCharacterToLower(), builder)).FirstOrDefault();
            
            return content;
        }

        public async Task<T> GetContentBySlug<T>(string slug, int includeLevel = 1) where T : ContentBase
        {
            var builder = QueryBuilder<T>.New
                .FieldEquals(i => i.Slug, slug)
                .Include(includeLevel);

            var content = (await _contentfulClient.GetEntriesByType(typeof(T).Name.FirstCharacterToLower(), builder)).FirstOrDefault();
            
            return content;
        }

        public async Task<List<T>> GetContentByType<T>() where T : ContentBase
        {
            return (await _contentfulClient.GetEntriesByType<T>(typeof(T).Name.FirstCharacterToLower())).ToList();
        }

        public async Task<NavigationBar> GetNavigationFor(string hub)
        {
            var builder = QueryBuilder<NavigationBar>.New
                .FieldEquals(i => i.Hub, hub)
                .Include(3);

            return (await _contentfulClient.GetEntriesByType<NavigationBar>("navigationBar",builder)).FirstOrDefault();
        }
    }
}