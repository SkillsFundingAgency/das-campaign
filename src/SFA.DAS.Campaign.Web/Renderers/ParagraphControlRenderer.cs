using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;

namespace SFA.DAS.Campaign.Web.Renderers
{
    public class ParagraphControlRenderer : IControlRenderer
    {
        public bool SupportsContent(IHtmlControl content)
        {
            return  content is Paragraph;
        }

        public HtmlString Render(IHtmlControl content)
        {
            var control = content as Paragraph;

            var para = new TagBuilder("p");

            foreach (var value in control.Content)
            {
                para.InnerHtml.AppendHtml(value.CheckForFontEffects().CheckForAndConstructHyperlinks());
            }

            string result = para.WriteString();

            return new HtmlString(result);
        }
    }
}
