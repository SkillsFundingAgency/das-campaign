using System;
using System.Collections.Generic;
using System.Text;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;

namespace SFA.DAS.Campaign.Domain.Content
{
    public class LandingPage : IContentType
    {
        public LandingPage()
        {
            Cards = new List<Card>();
        }
        public string Summary { get; set; }
        public Image HeaderImage { get; set; }
        public List<Card> Cards { get; set; }

    }
}
