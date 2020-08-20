using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SFA.DAS.Campaign.Content.ContentTypes;

namespace SFA.DAS.Campaign.Content.TagHelpers
{
    public class InfoPageSectionHtmlHelper
    {
        private HtmlRenderer _htmlRenderer;

        /// <summary>
        /// The InfoPageSection to render.
        /// </summary>
        public InfoPage InfoPage { get; set; }
        
        /// <summary>
        /// The InfoPageSection to render.
        /// </summary>
        public string SectionSlug { get; set; }
        public string SectionContainerClass { get; set; }

        public int HeaderIndex { get; set; }

        /// <summary>
        /// Creates a new instance of InfoPageSectionTagHelper.
        /// </summary>
        /// <param name="renderer">The HtmlRenderer used to render the document.</param>
        public InfoPageSectionHtmlHelper(HtmlRenderer renderer)
        {
            _htmlRenderer = renderer;
        }

        public async Task<IHtmlContent> ProcessAsync(TagHelperContext context)
        {
            if (InfoPage == null || string.IsNullOrWhiteSpace(SectionSlug))
            {
                return null;
            }

            var section = InfoPage.Sections.FirstOrDefault(s => s.Slug == SectionSlug);

            if (section is null)
            {
                return null;
            }

            IHtmlContentBuilder htmlBuilder = new HtmlContentBuilder();

            var container = new TagBuilder("div");
          
            
            if (SectionContainerClass != null)
            {
                container.Attributes.Add("class", SectionContainerClass);   
            }
            
            if (!string.IsNullOrWhiteSpace(section.Title))
            {
                var headerBuilder = new TagBuilder("h2");
                headerBuilder.AddCssClass("heading-m"); 
                headerBuilder.Attributes.Add("id", "h" + HeaderIndex);
                headerBuilder.InnerHtml.Append(section.Title);
                htmlBuilder.AppendHtml(headerBuilder);
            }
            
            var html = await _htmlRenderer.ToHtml(section.Body);

            return htmlBuilder.AppendHtml(html);

        }
    }
}