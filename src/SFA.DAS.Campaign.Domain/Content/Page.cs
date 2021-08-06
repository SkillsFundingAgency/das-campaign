using System.Collections.Generic;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;

namespace SFA.DAS.Campaign.Domain.Content
{
    public class Page<T>
    {
        public string Slug { get; set; }
        public string Title { get; set; }
        public MetaContent MetaContent { get; set; }
        public HubType HubType { get; set; }
        public T Content { get; set; } 
        public IEnumerable<ArticleRelated> RelatedPages { get; set; }
        public IEnumerable<DocumentAttachment> Attachments { get; set; }
        public Breadcrumbs Breadcrumbs { get; set; }
        public Menu Menu { get; set; }
        public IEnumerable<Banner> BannerModels { get; set; }
    }
}