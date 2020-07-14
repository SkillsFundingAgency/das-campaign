using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.Campaign.Application.Content.ContentTypes;

namespace SFA.DAS.Campaign.Application.Content
{
    public interface IContentService
    {
        Task<T> GetContentById<T>(string id, int includeLevel = 1) where T : ContentBase;
        Task<T> GetContentBySlug<T>(string hub, string slug, int includeLevel = 1) where T : ContentBase;
        Task<List<T>> GetContentByType<T>() where T : ContentBase;
        Task<NavigationBar> GetNavigationFor(string hub);
    }
}