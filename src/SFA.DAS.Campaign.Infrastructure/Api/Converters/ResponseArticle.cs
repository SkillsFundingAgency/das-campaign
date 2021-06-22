using System.Collections.Generic;

namespace SFA.DAS.Campaign.Infrastructure.Api.Converters
{
    public class ResponseArticle
    {
        public ResponseArticle()
        {
            RelatedArticles = new List<RelatedArticle>();
        }
        public PageAttributes PageAttributes { get; set; }
        public MainContent MainContent { get; set; }
        public List<RelatedArticle> RelatedArticles { get; set; }
    }
}