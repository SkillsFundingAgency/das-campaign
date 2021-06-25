using System;
using System.Collections.Generic;
using System.Text;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;

namespace SFA.DAS.Campaign.UnitTests.Web.Renderers.Builders
{
    public class OrderedListBuilder
    {
        private readonly OrderedList _list;

        public OrderedListBuilder()
        {
            _list = new OrderedList();
        }

        public static OrderedListBuilder New()
        {
            return new OrderedListBuilder();
        }

        public OrderedListBuilder AddItem(string text)
        {
            _list.Items.Add(text);

            return this;
        }

        public OrderedList Build()
        {
            return _list;
        }
    }
}
