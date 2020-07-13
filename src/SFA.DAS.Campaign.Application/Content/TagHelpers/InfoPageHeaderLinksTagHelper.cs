using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SFA.DAS.Campaign.Application.Content.ContentTypes;

namespace SFA.DAS.Campaign.Application.Content.TagHelpers
{
    [HtmlTargetElement("info-page-header-links", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class InfoPageHeaderLinksTagHelper : TagHelper
    {
        public InfoPage InfoPage { get; set; }
        
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if(InfoPage?.Sections is null || !InfoPage.Sections.Any())
            {
                return;
            }

            output.TagName = "ul";
            output.Attributes.Add("class", "list list--arrows");

            var headerIndex = 1;
            foreach (var section in InfoPage.Sections)
            {
                if (!string.IsNullOrWhiteSpace(section.Title) && section.ShowHeaderLink)
                {
                    var linkBuilder = new TagBuilder("a");
                    linkBuilder.AddCssClass("hero__link"); 
                    linkBuilder.Attributes.Add("href", "#h" + headerIndex);
                    linkBuilder.InnerHtml.Append(section.Title);

                    var liBuilder = new TagBuilder("li");
                    liBuilder.InnerHtml.AppendHtml(linkBuilder);
                    
                    output.Content.AppendHtml(liBuilder);
                }
                
                headerIndex++;
            }
        }
    }
}