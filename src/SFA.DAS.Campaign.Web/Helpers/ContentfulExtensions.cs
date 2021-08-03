using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.Rest;
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

        public static async Task<Page<BannerContentType>> GetBannersForStaticContent(this IMediator mediator)
        {
            var banners = await mediator.Send(new GetBannerQuery());

            return banners.Page;
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

        public static HtmlString BannerToHtml(this IEnumerable<Banner> banners)
        {
            var renderer = new BannerControlRenderer();
            var html = new StringBuilder();

            foreach (var banner in banners)
            {
                html.Append(renderer.Render(banner));
            }

            return new HtmlString(html.ToString());
        }
        public static HtmlString TabbedContentToHtml(this IEnumerable<TabbedContent> control, HttpContext context, ITempDataDictionary tempDataDictionary)
        {
            var renderer = new TabbedContentRenderer
            {
                HttpContext = context,
                TempDataDictionary = tempDataDictionary
            };
            
            var tabs = new Tabs()
            {
                TabbedContents = control
            };

            return renderer.Render(tabs);
        }

        public static async Task<HtmlString> RenderViewAsync(this TabbedContent tab, string viewName, HttpContext context, ITempDataDictionary tempDataDictionary)
        {
            using (var writer = new StringWriter())
            {
                IViewEngine viewEngine = context.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                var actionContext = new ActionContext(context, new Microsoft.AspNetCore.Routing.RouteData(), new ActionDescriptor());
                ViewEngineResult viewResult = viewEngine.GetView(viewName, viewName, false);

                if (viewResult.Success == false)
                {
                    return new HtmlString("");
                    //return $"A view with the name {viewName} could not be found";
                }

                var viewDictionary =
                    new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary());

                ViewContext viewContext = new ViewContext(
                    actionContext,
                    viewResult.View,
                    viewDictionary,
                    tempDataDictionary,
                    writer,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);

                return new HtmlString(writer.GetStringBuilder().ToString());
            }
        }
    }
}
