using System.Collections.Generic;
using System.Threading.Tasks;
using Contentful.Core.Models.Management;
using Newtonsoft.Json;
using SFA.DAS.Campaign.Content.ContentTypes;
using SFA.DAS.Campaign.Domain.Content;
using InfoPage = SFA.DAS.Campaign.Content.ContentTypes.InfoPage;

namespace SFA.DAS.Campaign.Content
{
    public class ContentService : IContentService
    {
        private IContentRepo _contentRepo;

        public ContentService(IContentRepo contentRepo)
        {
            _contentRepo = contentRepo;
        }

        public async Task<Page<T>> GetContent<T>(string hub, string slug, ContentType contentType)
        {

            string content = "";

            switch (contentType)
            {
                case ContentType.infopage:
                    content = await _contentRepo.GetContentByHubAndSlug<InfoPage>(hub, slug, 1);
                    break;
            }

            return JsonConvert.DeserializeObject<Page<T>>(content);

        }

        public async Task<Page<T>> GetContent<T>(string slug, ContentType contentType)
        {
            string content = "";

            switch (contentType)
            {
                case ContentType.infopage:
                    content = await _contentRepo.GetContentBySlug<InfoPage>(slug, 1);
                    break;
            }

            return JsonConvert.DeserializeObject<Page<T>>(content);
        }



        public enum ContentType
        {
            infopage = 0,
            article = 1
        }
    }
}