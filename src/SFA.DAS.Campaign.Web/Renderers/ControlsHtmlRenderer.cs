using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Html;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;

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
                new BlockQuoteControlRenderer(),
                new AttachmentControlRenderer(),
                new HorizontalRuleControlRenderer(),
                new OrderedListControlRenderer()
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

        public HtmlString ToHtml(IEnumerable<DocumentAttachment> attachmentsToRender)
        {
            var sb = new StringBuilder();

            foreach (var control in attachmentsToRender)
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
