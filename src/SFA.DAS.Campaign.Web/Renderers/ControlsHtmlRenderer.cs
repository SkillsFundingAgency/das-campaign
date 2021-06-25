using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Html;
using SFA.DAS.Campaign.Domain.Content;

namespace SFA.DAS.Campaign.Web.Renderers
{
    public class ControlsHtmlRenderer
    {
        private readonly IEnumerable<IControlRenderer> _controlRenderers;

        public ControlsHtmlRenderer()
        {
            _controlRenderers = new List<IControlRenderer>()
            {
                new ParagraphControlRenderer(),
                new UnorderedListControlRenderer(),
                new TableControlRenderer(),
                new HeadingControlRenderer(),
                new ImageControlRenderer(),
                new YouTubeControlRenderer(),
                new BlockQuoteControlRenderer()
            };
        }

        public HtmlString ToHtml(IEnumerable<IHtmlControl> controlsToRender)
        {
            var sb = new StringBuilder();

            foreach (var control in controlsToRender)
            {
                var renderer = _controlRenderers.FirstOrDefault(o => o.SupportsContent(control));

                if (renderer == null)
                {
                    continue;
                }

                sb.Append(renderer.Render(control));
            }

            return new HtmlString(sb.ToString());
        }
    }
}
