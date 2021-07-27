using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Campaign.Domain.Content.HtmlControl
{
    public class Tabs : IHtmlControl
    {
        public Tabs()
        {
            TabbedContents = new List<TabbedContent>();
        }
        public IEnumerable<TabbedContent> TabbedContents { get; set; }
    }
}
