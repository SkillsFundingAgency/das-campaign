using System;
using System.Collections.Generic;
using System.Text;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;

namespace SFA.DAS.Campaign.Domain.Content
{
    public class Hub : IContentType
    {
        public Hub()
        {
            Cards = new List<Card>();
            Cards2 = new List<Card>();
            Cards3 = new List<Card>();
        }
        public string Summary { get; set; }
        public Image HeaderImage { get; set; }
        public string CardsTitle { get; set; }
        public List<Card> Cards { get; set; }
        public string CardsTitle2 { get; set; }
        public List<Card> Cards2 { get; set; }
        public string CardsTitle3 { get; set; }
        public List<Card> Cards3 { get; set; }
    }
}
