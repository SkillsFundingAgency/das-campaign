using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Campaign.Domain.Content.HtmlControl
{
    public class Paragraph : IHtmlControl
    {
        public Paragraph()
        {
            Content = new List<string>();
        }
        public List<string> Content { get; set; }
    }
}
