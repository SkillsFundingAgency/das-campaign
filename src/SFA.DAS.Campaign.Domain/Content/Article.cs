using System.Collections.Generic;

namespace SFA.DAS.Campaign.Domain.Content
{
    public class Article : IContent
    {
        public List<ArticleSection> Sections { get; set; }
        public string LandingPageSlug { get; set; }
        public string LandingPageTitle { get; set; }

        public string Summary { get; set; }
    }
}