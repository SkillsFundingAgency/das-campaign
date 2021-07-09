using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Campaign.Domain.Content
{
    public class SiteMap : IContentType
    {
        public IEnumerable<Url> Urls { get; set; }
    }
}