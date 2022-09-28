using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using System.Collections.Generic;

namespace SFA.DAS.Campaign.Domain.Content
{
    public class Panel : IContentType
    {
        public string Title { get; set; }
        public IEnumerable<IHtmlControl> Content { get; set; }
        public Image MainImage { get; set; }
        public Button Button { get; set; }
    }
}