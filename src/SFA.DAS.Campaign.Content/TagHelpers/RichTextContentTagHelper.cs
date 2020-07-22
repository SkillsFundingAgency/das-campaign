using System.Threading.Tasks;
using Contentful.Core.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SFA.DAS.Campaign.Content.TagHelpers
{
    [HtmlTargetElement("rich-text-content", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class RichTextContentTagHelper : TagHelper
    {
        private readonly HtmlRenderer _htmlRenderer;

        public RichTextContentTagHelper(HtmlRenderer renderer)
        {
            _htmlRenderer = renderer;
        }

        public Document Document { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (Document is null)
            {
                return;
            }

            output.TagName = "div";
            var html = await _htmlRenderer.ToHtml(Document);
            output.Content.AppendHtml(html);
        }
    }
}