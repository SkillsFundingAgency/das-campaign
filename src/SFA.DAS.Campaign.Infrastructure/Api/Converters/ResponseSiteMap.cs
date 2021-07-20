using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;

namespace SFA.DAS.Campaign.Infrastructure.Api.Converters
{
    public class ResponseSiteMap
    {
        [JsonProperty("mainContent")]
        public SiteMapContent MainContent { get; set; }
    }

    public class SiteMapPage
    {
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Hub { get; set; }
        public string PageType { get; set; }
        public string ParentSlug { get; set; }
    }

    public class SiteMapContent
    {
        public List<SiteMapPage> Pages { get; set; }
    }
}
