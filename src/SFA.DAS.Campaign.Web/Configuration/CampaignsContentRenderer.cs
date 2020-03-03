using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contentful.Core.Models;

namespace SFA.DAS.Campaign.Web.Configuration
{
    public class CampaignsHtmlRenderer
    {
        private readonly ContentRendererCollection _contentRendererCollection;

        public CampaignsHtmlRenderer()
        {
            _contentRendererCollection = new ContentRendererCollection();
            _contentRendererCollection.AddRenderers(new List<IContentRenderer>
            {
                new ParagraphRenderer(_contentRendererCollection),
                new HyperlinkContentRenderer(_contentRendererCollection),
                new TextRenderer(),
                new HorizontalRulerContentRenderer(),
                new CanmpaignHeadingRenderer(),
                new ListContentRenderer(_contentRendererCollection),
                new ListItemContentRenderer(_contentRendererCollection),
                new QuoteContentRenderer(_contentRendererCollection),
                new AssetRenderer(_contentRendererCollection),
                new NullContentRenderer()
            });
        }

        /// <summary>
        /// Renders a document to HTML.
        /// </summary>
        /// <param name="doc">The document to turn into HTML.</param>
        /// <returns>An HTML string.</returns>
        public async Task<string> ToHtml(Document doc)
        {
            var sb = new StringBuilder();
            foreach (var content in doc.Content)
            {
                var renderer = _contentRendererCollection.GetRendererForContent(content);
                sb.Append(await renderer.RenderAsync(content));
            }

            return sb.ToString();
        }

    }

    /// <summary>
    /// A renderer for a heading.
    /// </summary>
    public class CanmpaignHeadingRenderer : IContentRenderer
    {
        private readonly IContentRenderer _rendererCollection;

        /// <summary>
        /// Initializes a new HeadingRenderer.
        /// </summary>
        /// <param name="rendererCollection">The collection of renderer to use for sub-content.</param>
        public CanmpaignHeadingRenderer()
        {
        }

        /// <summary>
        /// The order of this renderer in the collection.
        /// </summary>
        public int Order { get; set; } = 100;

        /// <summary>
        /// Whether or not this renderer supports the provided content.
        /// </summary>
        /// <param name="content">The content to evaluate.</param>
        /// <returns>Returns true if the content is a heading, otherwise false.</returns>
        public bool SupportsContent(IContent content)
        {
            return content is Heading1 || content is Heading2 || content is Heading3 || content is Heading4 || content is Heading5 || content is Heading6;
        }

        /// <summary>
        /// Renders the content to an html h-tag.
        /// </summary>
        /// <param name="content">The content to render.</param>
        /// <returns>The p-tag as a string.</returns>
        public string Render(IContent content)
        {

            var headingSize = 1;
            var headingClass = "";
            switch (content)
            {
                case Heading1 _:
                    headingClass = "heading-l";
                    break;
                case Heading2 _:
                    headingSize = 2;
                    headingClass = "heading-m";
                    break;
                case Heading3 _:
                    headingSize = 3;
                    headingClass = "heading-s";
                    break;
                case Heading4 _:
                    headingSize = 4;
                    headingClass = "heading-xs";
                    break;
                case Heading5 _:
                    headingSize = 5;
                    headingClass = "heading-xs";
                    break;
                case Heading6 _:
                    headingSize = 6;
                    headingClass = "heading-xs";
                    break;
            }

            var heading = content as IHeading;

            var sb = new StringBuilder();
            sb.Append($"<h{headingSize} class=\"{headingClass}\">");

            foreach (var subContent in heading.Content)
            {

                switch (subContent)
                {
                    case Paragraph p:
                    // Do something with the paragraph]
                    break;
                    case Text t:
                    // Do something with the heading
                    sb.Append(t.Value);
                    break;
                   
                }

                //var renderer = _rendererCollection.GetRendererForContent(subContent);
                //sb.Append(renderer.Render(subContent));
            }


            sb.Append($"</h{headingSize}>");
            return sb.ToString();
        }

        /// <summary>
        /// Renders the content asynchronously.
        /// </summary>
        /// <param name="content">The content to render.</param>
        /// <returns>The rendered string.</returns>
        public Task<string> RenderAsync(IContent content)
        {
            return Task.FromResult(Render(content));
        }
    }

    /// <summary>
    /// A renderer for a list.
    /// </summary>
    public class ListContentRenderer : IContentRenderer
    {
        private readonly ContentRendererCollection _rendererCollection;

        /// <summary>
        /// Initializes a new ListContentRenderer.
        /// </summary>
        /// <param name="rendererCollection">The collection of renderer to use for sub-content.</param>
        public ListContentRenderer(ContentRendererCollection rendererCollection)
        {
            _rendererCollection = rendererCollection;
        }

        /// <summary>
        /// The order of this renderer in the collection.
        /// </summary>
        public int Order { get; set; } = 100;

        /// <summary>
        /// Renders the content to a string.
        /// </summary>
        /// <param name="content">The content to render.</param>
        /// <returns>The list as a ul or ol HTML string.</returns>
        public string Render(IContent content)
        {
            var list = content as List;
            var listTagType = "ul";
            if (list.NodeType == "ordered-list")
            {
                listTagType = "ol";
            }

            var sb = new StringBuilder();

            sb.Append($"<{listTagType}>");

            foreach (var subContent in list.Content)
            {
                var renderer = _rendererCollection.GetRendererForContent(subContent);
                sb.Append(renderer.Render(subContent));
            }

            sb.Append($"</{listTagType}>");

            return sb.ToString();
        }

        /// <summary>
        /// Whether or not this renderer supports the provided content.
        /// </summary>
        /// <param name="content">The content to evaluate.</param>
        /// <returns>Returns true if the content is a list, otherwise false.</returns>
        public bool SupportsContent(IContent content)
        {
            return content is List;
        }

        /// <summary>
        /// Renders the content asynchronously.
        /// </summary>
        /// <param name="content">The content to render.</param>
        /// <returns>The rendered string.</returns>
        public Task<string> RenderAsync(IContent content)
        {
            return Task.FromResult(Render(content));
        }
    }

}
