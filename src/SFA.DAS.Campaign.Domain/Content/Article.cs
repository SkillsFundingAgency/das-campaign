using System.Collections.Generic;
using Microsoft.AspNetCore.Html;

namespace SFA.DAS.Campaign.Domain.Content
{
    public class Article : IContent
    {
        public string Slug { get; set; }
        public string Title { get; set; }
        public string PageTitle { get; set; }
        public string Hub { get; set; }
        public List<ArticleSection> Sections { get; set; }
    }

    public class ArticleSection
    {
        public string Title { get; set; }
        public HtmlString Body { get; set; }
        public string Slug { get; set; }
    }
}