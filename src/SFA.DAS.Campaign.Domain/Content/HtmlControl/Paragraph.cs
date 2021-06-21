using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Campaign.Domain.Content.HtmlControl
{
    public class Paragraph : IHtmlControl
    {
        public IEnumerable<string> Content { get; set; }
    }
}
