using System;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;

namespace SFA.DAS.Campaign.Infrastructure.Api.Factory
{
    public class ParagraphControlFactory : IHtmlControlFactory
    {
        public IHtmlControl Create(Item control)
        {
            var para = new Paragraph {Content = control.Values};

            return para;
        }

        public bool IsValid(Item control)
        {
            if (string.Compare(control.Type, "paragraph", StringComparison.OrdinalIgnoreCase) == 0 && !control.TableValue.Any())
            {
                return true;
            }

            return false;
        }
    }
}