using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.Campaign.Content.ContentTypes;

namespace SFA.DAS.Campaign.Content
{
    public interface IContentRepo
    {
        Task<string> GetContentById<T>(string id, int includeLevel = 1) where T : ContentBase;
        Task<string> GetContentByHubAndSlug<T>(string hub, string slug, int includeLevel = 1) where T : ContentBase;
        Task<string> GetContentBySlug<T>(string slug, int includeLevel = 1) where T : ContentBase;
        Task<List<T>> GetContentByType<T>() where T : ContentBase;
        Task<NavigationBar> GetNavigationFor(string hub);
    }
}