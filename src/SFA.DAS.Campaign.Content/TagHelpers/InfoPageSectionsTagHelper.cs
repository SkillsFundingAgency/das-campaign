using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SFA.DAS.Campaign.Content.ContentTypes;

namespace SFA.DAS.Campaign.Content.TagHelpers
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
        
        public string SectionContainerClass { get; set; }
        
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "";
            
            if(InfoPage?.Sections is null || !InfoPage.Sections.Any())
            {
                return;
            }

            var headerIndex = 1;
            foreach (var section in InfoPage.Sections)
            {
                var container = new TagBuilder("div");
                container.AddCssClass(SectionContainerClass);
                
                if (!string.IsNullOrWhiteSpace(section.Title))
                {
                    var headerBuilder = new TagBuilder("h2");
                    headerBuilder.AddCssClass("heading-m"); 
                    headerBuilder.Attributes.Add("id", "h" + headerIndex);
                    headerBuilder.InnerHtml.Append(section.Title);
                    container.InnerHtml.AppendHtml(headerBuilder);
                }
            
                var html = await _htmlRenderer.ToHtml(section.Body);

                container.InnerHtml.AppendHtml(html);
                
                output.Content.AppendHtml(container);
                
                headerIndex++;
            }
        }
    }
}