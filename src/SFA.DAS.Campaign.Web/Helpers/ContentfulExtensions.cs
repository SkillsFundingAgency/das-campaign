using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contentful.Core.Models;
using Microsoft.AspNetCore.Html;

namespace SFA.DAS.Campaign.Web.Helpers
{
    public static class ContentfulExtensions
    {
        public static HtmlString ToHtml(this Document document)
        {
            var renderer = new HtmlRenderer();
            var html = renderer.ToHtml(document).GetAwaiter().GetResult();
            return new HtmlString(html);
        }
    }
}
