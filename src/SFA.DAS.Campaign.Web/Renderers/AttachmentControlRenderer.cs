using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using SFA.DAS.Campaign.Infrastructure.Api.Converters;

namespace SFA.DAS.Campaign.Web.Renderers
{
    public class AttachmentControlRenderer : IControlRenderer
    {
        public bool SupportsContent(IHtmlControl content)
        {
            return content is DocumentAttachment;
        }

        public HtmlString Render(IHtmlControl content)
        {
            var control = content as DocumentAttachment;

            var quote = new TagBuilder($"div");
            quote.AddCssClass("fiu-attachment");

            quote.InnerHtml.AppendHtml($"<h2 class=\"govuk-heading-m fiu-attachment__heading\">{control.Title}</h2>");
            quote.InnerHtml.AppendHtml("<dl class=\"fiu-attachment__meta\"><dt class=\"fiu-attachment__meta-title\">File type</dt>");
            quote.InnerHtml.AppendHtml($"<dd class=\"fiu-attachment__meta-description\">{GetFileTypeFromContentTypeValue(control)}</dd>");
            quote.InnerHtml.AppendHtml("<dt class=\"fiu-attachment__meta-title\">File size</dt>");
            quote.InnerHtml.AppendHtml($"<dd class=\"fiu-attachment__meta-description\">{control.FileSize / 1000}KB</dd>");
            quote.InnerHtml.AppendHtml($"</dl><p class=\"govuk-body fiu-attachment__description\">{control.Description}</p>");
            quote.InnerHtml.AppendHtml($"<p class=\"govuk-body fiu-attachment__link-wrap\"><a href=\"{control.Url}\" class=\"govuk-link fiu-attachment__link\" target=\"_blank\">Download <span class=\"fiu-vh\">{control.Title}</span></a></p>");
            string result = quote.WriteString();

            return new HtmlString(result);
        }

        private static string GetFileTypeFromContentTypeValue(DocumentAttachment control)
        {
            if (control.FileType.Equals("application/vnd.openxmlformats-officedocument.presentationml.presentation"))
            {
                return "ppx";
            }

            return control.FileType.Substring(control.FileType.IndexOf("/") + 1);
        }
    }
}
