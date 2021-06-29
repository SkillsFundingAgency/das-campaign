using System;
using System.Collections.Generic;
using System.Text;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;

namespace SFA.DAS.Campaign.UnitTests.Web.Renderers.Builders
{
    public class UnorderedListBuilder
    {
        private readonly UnorderedList _list;

        public UnorderedListBuilder()
        {
            _list = new UnorderedList();
        }

        public static UnorderedListBuilder New()
        {
            return new UnorderedListBuilder();
        }

        public UnorderedListBuilder AddItem(string text)
        {
            _list.Items.Add(new List<string>
            {
                text
            });

            return this;
        }

        public UnorderedList Build()
        {
            return _list;
        }
    }
}
