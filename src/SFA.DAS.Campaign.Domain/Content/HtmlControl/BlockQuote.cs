using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Campaign.Domain.Content.HtmlControl
{
    public class BlockQuote : IHtmlControl
    {
        public BlockQuote()
        {
            Content = new List<string>();
        }
        public List<string> Content { get; set; }
    }
}
