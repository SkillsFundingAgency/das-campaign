using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using SFA.DAS.Campaign.Infrastructure.Api.Converters;

namespace SFA.DAS.Campaign.Infrastructure.Api.Factory
{
    public class UnorderedListControlFactory : IHtmlControlFactory
    {
        public IHtmlControl Create(Item control)
        {
            var ul = new UnorderedList {Items = control.TableValue};

            return ul;
        }

        public bool IsValid(Item control)
        {
            if (string.Compare(control.Type, "unordered-list", StringComparison.OrdinalIgnoreCase) == 0)
            {
                return true;
            }

            return false;
        }
    }
}
