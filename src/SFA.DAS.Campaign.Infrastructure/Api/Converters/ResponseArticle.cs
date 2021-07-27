using System.Collections.Generic;
using Newtonsoft.Json;

namespace SFA.DAS.Campaign.Infrastructure.Api.Converters
{
    public class ResponseArticle
    {
        public ResponseArticle()
        {
            RelatedArticles = new List<RelatedArticle>();
            Attachments = new List<Attachment>();
            TabbedContents = new List<ResponseTabbedContent>();
        }
        public PageAttributes PageAttributes { get; set; }
        public MainContent MainContent { get; set; }
        public List<RelatedArticle> RelatedArticles { get; set; }
        public List<Attachment> Attachments { get; set; }
        public PageAttributes ParentPage { get; set; }
        public MenuContent MenuContent { get; set; }
        public List<ResponseTabbedContent> TabbedContents { get; set; }
    }
}