using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Campaign.Domain.Content.HtmlControl
{
    public class ArticleRelated : IHtmlControl
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string HubType { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Url => $"/{HubType.ToString().ToLower()}/{Slug}";
    }
}
