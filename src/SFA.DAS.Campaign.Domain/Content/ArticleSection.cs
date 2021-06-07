using Contentful.Core.Models;
using Microsoft.AspNetCore.Html;

namespace SFA.DAS.Campaign.Domain.Content
{
    public class ArticleSection : IContentType
    {
        public string Title { get; set; }
        public Document Body { get; set; }
        public SystemProperties Sys { get; set; }
        public string Slug { get; set; }
    }
}