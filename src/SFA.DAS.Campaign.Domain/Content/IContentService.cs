using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Domain.Content
{
    public interface IContentService
    {
        Task<Page<T>> GetPage<T>(string slug) where T : IContent;
        List<ArticleCard> GetArticleCardsFor(string landingPageSlug, HubType hubType);
    }
}