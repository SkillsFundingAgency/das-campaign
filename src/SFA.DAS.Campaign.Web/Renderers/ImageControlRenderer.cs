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
    public class ImageControlRenderer : IControlRenderer
    {
        public bool SupportsContent(IHtmlControl content)
        {
            return content is Image;
        }

        public HtmlString Render(IHtmlControl content)
        {
            var control = content as Image;

            var img = new TagBuilder($"img");
            img.TagRenderMode = TagRenderMode.SelfClosing;
            img.Attributes.Add("title", control.Title);
            img.Attributes.Add("src", control.Url);
            
            string result = img.WriteString();

            return new HtmlString(result);
        }
    }
}
