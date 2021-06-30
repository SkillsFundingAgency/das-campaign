using System;
using System.Collections.Generic;
using System.Text;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;

namespace SFA.DAS.Campaign.UnitTests.Web.Renderers.Builders
{
    public class TableBuilder
    {
        private readonly Table _table;

        public TableBuilder()
        {
            _table = new Table();
        }

        public static TableBuilder New()
        {
            return new TableBuilder();
        }

        public TableBuilder AddHeading(string heading)
        {
            _table.Headings.Add(heading);

            return this;
        }

        public TableBuilder AddRowValue(string rowValue)
        {
            _table.Rows.Add(rowValue);

            return this;
        }

        public Table Build()
        {
            return _table;
        }
    }
}
