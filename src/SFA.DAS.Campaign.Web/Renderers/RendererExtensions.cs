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

            return $"<a href=\"{url.Groups[1].Value}\">{linkText.Groups[1].Value}</a>";
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
    }
}
