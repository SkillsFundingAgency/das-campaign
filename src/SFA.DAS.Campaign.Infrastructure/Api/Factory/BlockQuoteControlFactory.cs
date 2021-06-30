using System;
using System.Collections.Generic;
using System.Text;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using SFA.DAS.Campaign.Infrastructure.Api.Converters;

namespace SFA.DAS.Campaign.Infrastructure.Api.Factory
{
    public class BlockQuoteControlFactory : IHtmlControlFactory
    {
        public IHtmlControl Create(Item control)
        {
            var block = new BlockQuote
            {
                Content = control.Values
            };

            return block;
        }

        public bool IsValid(Item control)
        {
            if (control.Type.StartsWith("blockquote", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }
    }
}
