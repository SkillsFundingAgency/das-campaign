using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Campaign.Domain.Content
{
    public class SiteMap : IContentType
    {
        public IEnumerable<SiteMapUrl> Urls { get; set; }
    }
}


public class SiteMapUrl
{
    public string Slug { get; set; }
    public string Title { get; set; }
    public string Hub { get; set; }
    public string PageType { get; set; }
}
