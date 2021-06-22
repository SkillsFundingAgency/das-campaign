using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using SFA.DAS.Campaign.Domain.Content;

namespace SFA.DAS.Campaign.Web.Renderers
{
    public interface IControlRenderer
    {
        bool SupportsContent(IHtmlControl content);

        /// <summary>Renders the provided content as a string.</summary>
        /// <param name="content">The content to render.</param>
        /// <returns></returns>
        HtmlString Render(IHtmlControl content);
    }
}
