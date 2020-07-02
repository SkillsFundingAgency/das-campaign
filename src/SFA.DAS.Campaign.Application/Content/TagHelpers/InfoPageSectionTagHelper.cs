using System.Threading.Tasks;
using Contentful.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SFA.DAS.Campaign.Application.Content.ContentTypes;

namespace SFA.DAS.Campaign.Application.Content.TagHelpers
{
    [HtmlTargetElement("info-page-section", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class InfoPageSectionTagHelper : TagHelper
    {
        private HtmlRenderer _htmlRenderer;

        /// <summary>
        /// The InfoPageSection to render.
        /// </summary>
        public InfoPageSection InfoPageSection { get; set; }

        public int HeaderIndex { get; set; }

        /// <summary>
        /// Creates a new instance of InfoPageSectionTagHelper.
        /// </summary>
        /// <param name="renderer">The HtmlRenderer used to render the document.</param>
        public InfoPageSectionTagHelper(HtmlRenderer renderer)
        {
            _htmlRenderer = renderer;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if(InfoPageSection == null)
            {
                return;
            }

            if (!string.IsNullOrWhiteSpace(InfoPageSection.Title))
            {
                var headerBuilder = new TagBuilder("h2");
                headerBuilder.AddCssClass("heading-m"); 
                headerBuilder.Attributes.Add("id", "h" + HeaderIndex);
                headerBuilder.InnerHtml.Append(InfoPageSection.Title);
                output.Content.AppendHtml(headerBuilder);
            }
            
            var html = await _htmlRenderer.ToHtml(InfoPageSection.Body);

            output.Content.AppendHtml(html);
        }
    }
}