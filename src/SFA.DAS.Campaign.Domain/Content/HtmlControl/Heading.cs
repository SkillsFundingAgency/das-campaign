using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Campaign.Domain.Content.HtmlControl
{
    public class Heading : IHtmlControl
    {
        public Heading()
        {
            Content = new List<string>();
        }
        public List<string> Content { get; set; }
        public int HeadingSize { get; set; }
    }
}
