using Contentful.Core.Models;

namespace SFA.DAS.Campaign.Application.Content
{
    public class ContentBase
    {
        public SystemProperties Sys { get; set; }
        public string Slug { get; set; }
    }
}