using System;
using System.Collections.Generic;
using System.Text;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;

namespace SFA.DAS.Campaign.UnitTests.Web.Renderers.Builders
{
    public class ParagraphBuilder
    {
        private readonly Paragraph _paragraph;

        public ParagraphBuilder()
        {
            _paragraph = new Paragraph();
        }

        public static ParagraphBuilder New()
        {
            return new ParagraphBuilder();
        }

        public ParagraphBuilder AddText(string text)
        {
            _paragraph.Content.Add(text);

            return this;
        }

        public Paragraph Build()
        {
            return _paragraph;
        }
    }
}
