using System;
using System.Collections.Generic;
using System.Text;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using SFA.DAS.Campaign.Infrastructure.Api.Converters;

namespace SFA.DAS.Campaign.Infrastructure.Api.Factory
{
    public class HeadingControlFactory : IHtmlControlFactory
    {
        public IHtmlControl Create(Item control)
        {
            var size = control.Type.Substring(control.Type.IndexOf("-", StringComparison.OrdinalIgnoreCase) + 1);
            var para = new Heading
            {
                Content = control.Values,
                HeadingSize = int.Parse(size)
            };

            return para;
        }

        public bool IsValid(Item control)
        {
            if (control.Type.StartsWith("heading-", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }
    }
}
