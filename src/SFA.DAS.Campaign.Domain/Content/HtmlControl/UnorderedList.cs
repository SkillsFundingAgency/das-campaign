using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Campaign.Domain.Content.HtmlControl
{
    public class UnorderedList : IHtmlControl
    {
        public IEnumerable<string> Items { get; set; }
    }
}
