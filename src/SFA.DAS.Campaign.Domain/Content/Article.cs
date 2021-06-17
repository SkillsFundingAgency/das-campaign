using System.Collections.Generic;

namespace SFA.DAS.Campaign.Domain.Content
{
    public class Article : IContentType
    {
        public string Summary { get; set; }
        public List<ArticleSection> Sections { get; set; }
        public LandingPage LandingPage { get; set; }
    }
}