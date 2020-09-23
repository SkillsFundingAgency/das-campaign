﻿using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sfa.Das.Sas.Shared.Components.Extensions
{
    public static class HtmlExtensions
    {

        public static HtmlString MarkdownToHtml(this IHtmlHelper htmlHelper, string markdownText)
        {
            if (!string.IsNullOrEmpty(markdownText))
            {
                return new HtmlString("" + htmlHelper.Raw(CommonMark.CommonMarkConverter.Convert(markdownText.Replace("\\r", "\r").Replace("\\n", "\n"))) + "");
            }

            return new HtmlString(string.Empty);
        }
    }
}