using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Application.Content
{
    public interface IContentService
    {
        Task<T> GetContentById<T>(string id, int includeLevel = 1) where T : ContentBase;
        Task<T> GetContentBySlug<T>(string slug, int includeLevel = 1) where T : ContentBase;
        Task<List<T>> GetContentByType<T>() where T : ContentBase;
    }
}