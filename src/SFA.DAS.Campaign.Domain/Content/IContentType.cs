using Contentful.Core.Models;

namespace SFA.DAS.Campaign.Domain.Content
{
    public interface IContentType
    {
        SystemProperties Sys { get; set; }
        string Slug { get; set; }
        string Title { get; set; }
    }
}