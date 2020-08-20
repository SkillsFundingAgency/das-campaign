using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SFA.DAS.Campaign.Content.ContentTypes;

namespace SFA.DAS.Campaign.Content.TagHelpers
{
    public class InfoPageSectionsHtmlHelper
    {
        private HtmlRenderer _htmlRenderer;
        
        public InfoPageSectionsHtmlHelper(HtmlRenderer renderer)
        {
            _htmlRenderer = renderer;
        }
        
        public InfoPage InfoPage { get; set; }
        
        public string SectionContainerClass { get; set; }
        
        public async Task<IHtmlContent> ProcessAsync()
        {
            IHtmlContentBuilder htmlBuilder = new HtmlContentBuilder();
            
            if(InfoPage?.Sections is null || !InfoPage.Sections.Any())
            {
                return null;
            }

            var headerIndex = 1;
            foreach (var section in InfoPage.Sections)
            {
                var container = new TagBuilder("");
                
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

                htmlBuilder = htmlBuilder.AppendHtml(container);

                headerIndex++;

                
            }

            return htmlBuilder;
        }
    }
}