using Contentful.Core.Models;

namespace SFA.DAS.Campaign.Content
{
    public class ContentBase
    {
        public SystemProperties Sys { get; set; }
        public string Slug { get; set; }
        public string Hub { get; set; }
    }
}