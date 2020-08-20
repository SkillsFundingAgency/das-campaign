using System.Collections.Generic;
using Contentful.Core.Models;
using Microsoft.AspNetCore.Html;

namespace SFA.DAS.Campaign.Domain.Content
{
    public class InfoPage
    {

        public IHtmlContent HeaderLinks { get; set; }

        public IList<InfoPageSection> Sections { get; set; }
        
        public Link PrevLink { get; set; }

        public Link NextLink { get; set; }
    }

    public class Link
    {
        public string Title { get; set; }
        public string Href { get; set; }
    }
}
