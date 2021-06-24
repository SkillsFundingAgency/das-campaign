using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using SFA.DAS.Campaign.Domain.Content;

namespace SFA.DAS.Campaign.Web.Renderers
{
    public static class RendererExtensions
    {
        public static string CheckForAndConstructHyperlinks(this string controlValue)
        {
            var linkTextRegEx = new Regex(@"\[(.*?)\]", RegexOptions.Compiled);
            var urlRegEx = new Regex(@"\(([^]]+)\)", RegexOptions.Compiled);

            var linkText = linkTextRegEx.Matches(controlValue).FirstOrDefault();
            var url = urlRegEx.Matches(controlValue).FirstOrDefault();

            if (string.IsNullOrWhiteSpace(linkText?.Groups[1].Value) || string.IsNullOrWhiteSpace(url?.Groups[1].Value))
            {
                return controlValue;
            }

            var sb = new StringBuilder();
            sb.Append($"<a href=\"{url.Groups[1].Value}\"");

            if (url.Groups[1].Value.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                sb.Append(" title=\"\" rel=\"external\" target=\"_blank\"");
            }

            sb.Append(">");
            sb.Append($"{linkText.Groups[1].Value}</a>");

            return sb.ToString();
        }

        public static string CheckForFontEffects(this string controlValue)
        {
            var fontEffectRegEx = new Regex(@"\[(bold|italic)\]", RegexOptions.Compiled);
            var effect = fontEffectRegEx.Matches(controlValue).FirstOrDefault();

            if (string.IsNullOrWhiteSpace(effect?.Groups[1].Value))
            {
                return controlValue;
            }

            var tag = GetHtml5TagNameFromMarkup(effect?.Groups[1].Value);

            return $"<{tag}>{controlValue.Replace(effect?.Groups[0].Value, "")}</{tag}>";
        }

        public static string WriteString(this TagBuilder builder)
        {
            string result;

            using (var writer = new StringWriter())
            {
                builder.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);

                result = writer.ToString();
            }

            return result;
        }

        private static string GetHtml5TagNameFromMarkup(string fontEffect)
        {
            if (string.Compare(fontEffect, "bold", StringComparison.OrdinalIgnoreCase) == 0)
            {
                return "strong";
            }

            return "i";
        }
    }
}
