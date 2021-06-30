using System;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using SFA.DAS.Campaign.Infrastructure.Api.Converters;

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
            if (string.Compare(control.Type, "paragraph", StringComparison.OrdinalIgnoreCase) == 0 && !control.TableValue.Any() && !control.Values.FirstOrDefault().StartsWith("https://www.youtube.com"))
            {
                return true;
            }

            return false;
        }
    }
}