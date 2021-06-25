using System;
using System.Collections.Generic;
using System.Text;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using SFA.DAS.Campaign.Infrastructure.Api.Converters;

namespace SFA.DAS.Campaign.Infrastructure.Api.Factory
{
    public class OrderedListControlFactory : IHtmlControlFactory
    {
        public IHtmlControl Create(Item control)
        {
            var ul = new OrderedList { Items = control.Values };

            return ul;
        }

        public bool IsValid(Item control)
        {
            if (string.Compare(control.Type, "ordered-list", StringComparison.OrdinalIgnoreCase) == 0)
            {
                return true;
            }

            return false;
        }
    }
}
