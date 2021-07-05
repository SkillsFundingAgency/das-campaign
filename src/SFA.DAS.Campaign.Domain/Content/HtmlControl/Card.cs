using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Campaign.Domain.Content.HtmlControl
{
    public class Card : IHtmlControl
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string HubType { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Url => $"/{HubType}/{Slug}";
        public LandingPage LandingPage { get; set; }
    }

    public class LandingPage
    {
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Hub { get; set; }
        public string Url => $"/{Hub}/{Slug}";
    }
}
