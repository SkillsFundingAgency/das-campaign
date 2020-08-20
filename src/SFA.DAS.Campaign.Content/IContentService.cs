using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.Campaign.Content.ContentTypes;
using SFA.DAS.Campaign.Domain.Content;

namespace SFA.DAS.Campaign.Content
{
    public interface IContentService
    {

        Task<Page<T>> GetContent<T>(string hub, string slug, ContentService.ContentType contentType);
        Task<Page<T>> GetContent<T>(string slug, ContentService.ContentType contentType);

    }
}