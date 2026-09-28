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
        public bool HasHeaderColumn { get; set; }

        private int? _columnCount;

        public int ColumnCount
        {
            get
            {
                return _columnCount ?? Headings.Count;
            }
            set
            {
                _columnCount = value;
            }
        }
    }
}
