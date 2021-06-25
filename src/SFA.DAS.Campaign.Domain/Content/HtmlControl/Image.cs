using System;
using System.Collections.Generic;
using System.Text;
using StackExchange.Redis;

namespace SFA.DAS.Campaign.Domain.Content.HtmlControl
{
    public class Image : IHtmlControl
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
    }
}
