using System.Threading.Tasks;
using Contentful.Core.Models;
using Microsoft.AspNetCore.Html;
using Newtonsoft.Json;

namespace SFA.DAS.Campaign.Content.ContentTypes
{
    public class InfoPageSection : ContentBase
    {
        private HtmlRenderer _htmlRenderer;

        public InfoPageSection(HtmlRenderer renderer)
        {
            _htmlRenderer = renderer;
        }
        public string Title { get; set; }

        [JsonIgnore]
        public Document Body { get; set; }

        public IHtmlContent RenderedBody => Body != null ? new HtmlString(_htmlRenderer.ToHtml(Body).Result) : null;

        public bool ShowHeaderLink { get; set; }
    }
}