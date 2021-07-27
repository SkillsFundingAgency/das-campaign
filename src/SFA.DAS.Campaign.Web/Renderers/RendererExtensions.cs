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

            return ConstructHyperlink(controlValue, linkText, url);
        }

        private static string ConstructHyperlink(string controlValue, Match linkText, Match url)
        {
            var stringOutputs = RetriveHyperlinkFromControlValue(controlValue, linkText?.Groups[0].Value, url?.Groups[0].Value);

            var sb = new StringBuilder();
            sb.Append(stringOutputs.Prepend);
            sb.Append($"<a href=\"{url.Groups[1].Value}\"");

            if (url.Groups[1].Value.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                sb.Append(" title=\"\" rel=\"external\" target=\"_blank\"");
            }

            sb.Append(">");
            sb.Append($"{linkText.Groups[1].Value}</a>");
            sb.Append(stringOutputs.Append);

            return sb.ToString();
        }

        private static StringOutputs RetriveHyperlinkFromControlValue(string controlValue, string linkText, string url)
        {
            string textToPrepend = string.Empty;
            string textToAppend = string.Empty;

            if (controlValue.StartsWith("[", StringComparison.OrdinalIgnoreCase))
            {
                textToAppend = controlValue.Replace(linkText, "");
                textToAppend = textToAppend.Replace(url, "");
            }
            else
            {
                textToPrepend = controlValue.Replace(linkText, "");
                textToPrepend = textToPrepend.Replace(url, "");
            }

            return new StringOutputs
            {
                Append = textToAppend,
                Prepend = textToPrepend
            };
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

        public static string EnsureHrefUsableTitle(this string tabTitle)
        {
            return tabTitle.Replace(" ", "");
        }

        private static string GetHtml5TagNameFromMarkup(string fontEffect)
        {
            if (string.Compare(fontEffect, "bold", StringComparison.OrdinalIgnoreCase) == 0)
            {
                return "strong";
            }

            return "i";
        }

        internal class StringOutputs
        {
            internal string Prepend { get; set; }
            internal string Append { get; set; }
        }
    }
}
