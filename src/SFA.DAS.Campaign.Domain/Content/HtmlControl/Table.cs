using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Campaign.Domain.Content.HtmlControl
{
    public class Table : IHtmlControl
    {
        public Table()
        {
            Headings = new List<string>();
            Rows = new List<string>();
        }
        public List<string> Headings { get; set; }
        public List<string> Rows { get; set; }

        public int ColumnCount
        {
            get
            {
                return Headings.Count;
            }
        }
    }
}
