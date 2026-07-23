using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;

namespace SFA.DAS.Campaign.Web.Renderers
{
    public class ParagraphControlRenderer : IControlRenderer
    {
        public bool SupportsContent(IHtmlControl content)
        {
            return  content is Paragraph;
        }

        public HtmlString Render(IHtmlControl content)
        {
            var control = content as Paragraph;

            var para = new TagBuilder("p");

            foreach (var value in control.Content)
            {
                para.InnerHtml.AppendHtml(value.CheckForFontEffects().CheckForAndConstructHyperlinks());
            }

            var result = new StringBuilder(para.WriteString());

            if (control.VideoTranscripts != null)
            {
                foreach (var transcript in control.VideoTranscripts)
                {
                    result.Append(RenderVideoTranscript(transcript));
                }
            }

            return new HtmlString(result.ToString());
        }

        private static string RenderVideoTranscript(VideoTranscript transcript)
        {
            var details = new TagBuilder("details");
            details.AddCssClass("govuk-details fiu-details");
            details.Attributes.Add("data-module", "govuk-details");

            var summary = new TagBuilder("summary");
            summary.AddCssClass("govuk-details__summary");

            var summaryText = new TagBuilder("span");
            summaryText.AddCssClass("govuk-details__summary-text");
            summaryText.InnerHtml.Append("Show transcript");
            summary.InnerHtml.AppendHtml(summaryText);

            var text = new TagBuilder("div");
            text.AddCssClass("govuk-details__text");

            var paragraphs = (transcript.Text ?? string.Empty)
                .Split('\n')
                .Select(line => line.Trim())
                .Where(line => !string.IsNullOrEmpty(line));

            foreach (var paragraph in paragraphs)
            {
                var p = new TagBuilder("p");
                p.AddCssClass("govuk-body");
                p.InnerHtml.Append(paragraph);
                text.InnerHtml.AppendHtml(p);
            }

            details.InnerHtml.AppendHtml(summary);
            details.InnerHtml.AppendHtml(text);

            return details.WriteString();
        }
    }
}
