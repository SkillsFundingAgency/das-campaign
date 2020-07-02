using System.Linq;
using System.Threading.Tasks;
using Contentful.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SFA.DAS.Campaign.Application.Content.ContentTypes;

namespace SFA.DAS.Campaign.Application.Content.TagHelpers
{
    [HtmlTargetElement("info-page-sections", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class InfoPageSectionsTagHelper : TagHelper
    {
        private HtmlRenderer _htmlRenderer;
        
        public InfoPageSectionsTagHelper(HtmlRenderer renderer)
        {
            _htmlRenderer = renderer;
        }
        
        public InfoPage InfoPage { get; set; }
        
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if(InfoPage?.Sections is null || !InfoPage.Sections.Any())
            {
                return;
            }

            var headerIndex = 1;
            foreach (var section in InfoPage.Sections)
            {
                if (!string.IsNullOrWhiteSpace(section.Title))
                {
                    var headerBuilder = new TagBuilder("h2");
                    headerBuilder.AddCssClass("heading-m"); 
                    headerBuilder.Attributes.Add("id", "h" + headerIndex);
                    headerBuilder.InnerHtml.Append(section.Title);
                    output.Content.AppendHtml(headerBuilder);
                }
            
                var html = await _htmlRenderer.ToHtml(section.Body);

                output.Content.AppendHtml(html);
                headerIndex++;
            }
        }
    }
}