using System;
using System.Collections.Generic;
using System.Text;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using SFA.DAS.Campaign.Infrastructure.Api.Converters;

namespace SFA.DAS.Campaign.Infrastructure.Api.Factory
{
    public class HorizontalRuleControlFactory : IHtmlControlFactory
    {
        public IHtmlControl Create(Item control)
        {
            var hr = new HorizontalRule();

            return hr;
        }

        public bool IsValid(Item control)
        {
            if (string.Compare(control.Type, "hr", StringComparison.OrdinalIgnoreCase) == 0)
            {
                return true;
            }

            return false;
        }
    }
}
