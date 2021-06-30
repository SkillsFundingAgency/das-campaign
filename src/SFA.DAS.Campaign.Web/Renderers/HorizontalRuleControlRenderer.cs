using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;

namespace SFA.DAS.Campaign.Web.Renderers
{
    public class HorizontalRuleControlRenderer : IControlRenderer
    {
        public bool SupportsContent(IHtmlControl content)
        {
            return content is HorizontalRule;
        }

        public HtmlString Render(IHtmlControl content)
        {
            var control = content as HorizontalRule;
            
            var rule = new TagBuilder($"hr");
            rule.TagRenderMode = TagRenderMode.SelfClosing;

            string result = rule.WriteString();

            return new HtmlString(result);
        }
    }
}
