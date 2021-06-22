using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contentful.Core.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Web.Renderers;

namespace SFA.DAS.Campaign.Web.Helpers
{
    public static class ContentfulExtensions
    {
        public static HtmlString ToHtml(this IEnumerable<IHtmlControl> controls)
        {
            var renderer = new ControlsHtmlRenderer();
            var html = renderer.ToHtml(controls);
            return html;
        }
    }
}
