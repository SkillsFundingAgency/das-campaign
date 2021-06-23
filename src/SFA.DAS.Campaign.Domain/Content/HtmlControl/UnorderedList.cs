using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Campaign.Domain.Content.HtmlControl
{
    public class UnorderedList : IHtmlControl
    {
        public UnorderedList()
        {
            Items = new List<string>();
        }

        public List<string> Items { get; set; }
    }
}
