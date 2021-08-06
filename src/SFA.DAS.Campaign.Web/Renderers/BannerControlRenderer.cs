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
    public class BannerControlRenderer : IControlRenderer
    {
        public bool SupportsContent(IHtmlControl content)
        {
            return content is Banner;
        }

        public HtmlString Render(IHtmlControl content)
        {
            var control = content as Banner;

            if (control == null)
            {
                return new HtmlString("");
            }

            var tagBuilder = new TagBuilder("div");

            if (!control.AllowUserToHideTheBanner)
            {
                tagBuilder.AddCssClass("fiu-banner");
                var innerDiv = new TagBuilder("div");
                innerDiv.AddCssClass("fiu-width-container");

                var h2 = new TagBuilder("h2");
                h2.AddCssClass("fiu-banner__heading");
                h2.InnerHtml.Append(control.Title);
                innerDiv.InnerHtml.AppendHtml(h2.WriteString());

                //var content = new TagBuilder("div");
                //content.AddCssClass("fiu-banner__body fiu-article");
                //content.InnerHtml.Append(control.)
                tagBuilder.InnerHtml.AppendHtml(innerDiv.WriteString());
            }

            return new HtmlString(tagBuilder.WriteString());
        }
    }
}
