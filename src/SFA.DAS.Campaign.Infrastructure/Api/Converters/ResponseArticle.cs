using System.Collections.Generic;

namespace SFA.DAS.Campaign.Infrastructure.Api.Converters
{
    public class ResponseArticle
    {
        public ResponseArticle()
        {
            RelatedArticles = new List<RelatedArticle>();
            Attachments = new List<Attachment>();
        }
        public PageAttributes PageAttributes { get; set; }
        public MainContent MainContent { get; set; }
        public List<RelatedArticle> RelatedArticles { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
}