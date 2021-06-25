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
    public class YouTubeControlRenderer : IControlRenderer
    {
        public bool SupportsContent(IHtmlControl content)
        {
            return content is YouTube;
        }

        public HtmlString Render(IHtmlControl content)
        {
            var control = content as YouTube;

            var parentDiv = new TagBuilder($"div");
            
            parentDiv.Attributes.Add("class", "fiu-video-player");
            parentDiv.InnerHtml.AppendHtml(
                $"<div class=\"fiu-video-player__inner-wrap\"><iframe class=\"fiu-video-player__size-100\" src=\"{control.Url}\" allow=\"accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture\" allowfullscreen></iframe></div>");
            string result = parentDiv.WriteString();

            return new HtmlString(result);
        }
    }
}
