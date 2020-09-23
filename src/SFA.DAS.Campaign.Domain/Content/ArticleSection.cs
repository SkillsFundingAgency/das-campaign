using Microsoft.AspNetCore.Html;

namespace SFA.DAS.Campaign.Domain.Content
{
    public class ArticleSection
    {
        public string Title { get; set; }
        public HtmlString Body { get; set; }
        public string Slug { get; set; }
    }
}