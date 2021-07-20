using System.Collections.Generic;

namespace SFA.DAS.Campaign.Domain.Content.HtmlControl
{
    public class SiteMapUrls : IHtmlControl
    {
        public SiteMapUrls()
        {
            Urls = new List<Url>();
        }
        public List<Url> Urls { get; set; }
    }
}
