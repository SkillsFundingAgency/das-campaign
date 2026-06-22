using System.Collections.Generic;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;

namespace SFA.DAS.Campaign.Domain.Content
{
    public class Article : IContentType
    {
        public string Summary { get; set; }
        public string ArticleType { get; set; }
        public Image HeaderImage { get; set; }
        public IEnumerable<IHtmlControl> PageControls { get; set; }

        public IEnumerable<TabbedContent> TabbedContents { get; set; }
    }
}