using System;
using System.Threading.Tasks;
using Contentful.Core.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SFA.DAS.Campaign.Web.Models;

namespace SFA.DAS.Campaign.Web.TagHelpers
{
    /// <summary>
    /// Taghelper that renders a rich text field.
    /// </summary>
    [HtmlTargetElement("contentful-rich-text-campaigns", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class ContentfulRichTextTagHelper : TagHelper
    {
        private CampaignsHtmlRenderer _htmlRenderer;

        /// <summary>
        /// The document to render.
        /// </summary>
        public Document Document { get; set; }

        /// <summary>
        /// Creates a new instance of ContentfulRichTextTagHelper.
        /// </summary>
        /// <param name="renderer">The HtmlRenderer used to render the document.</param>
        public ContentfulRichTextTagHelper(CampaignsHtmlRenderer renderer)
        {
            _htmlRenderer = renderer;
        }

        /// <summary>
        /// Executes the taghelper.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (Document == null)
            {
                return;
            }

            var html = await _htmlRenderer.ToHtml(Document);

            output.TagName = null;
            output.Content.SetHtmlContent(html);
        }
    }
}