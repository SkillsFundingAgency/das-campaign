using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using SFA.DAS.Campaign.Infrastructure.Api.Queries;
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

        public static HtmlString CardToHtml(this IHtmlControl control)
        {
            var renderer = new CardControlRenderer();
            return renderer.Render(control);
        }

        public static async Task<Page<Menu>> GetMenuForStaticContent(this IMediator mediator)
        {
            var menu = await mediator.Send(new GetMenuQuery());

            return menu.Page;
        }

        public static HtmlString SiteMapLinksToHtml(this IEnumerable<Url> control)
        {
            var renderer = new SiteMapUrlRenderer();
            var siteMapUrls = new SiteMapUrls
            {
                Urls = control.ToList()
            };

            return renderer.Render(siteMapUrls);
        }

        public static HtmlString TabbedContentToHtml(this IEnumerable<TabbedContent> control)
        {
            var renderer = new TabbedContentRenderer();
            var tabs = new Tabs()
            {
                TabbedContents = control
            };

            return renderer.Render(tabs);
        }
    }
}
