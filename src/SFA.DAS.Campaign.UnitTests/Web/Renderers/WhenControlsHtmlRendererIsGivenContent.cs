using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using SFA.DAS.Campaign.UnitTests.Web.Renderers.Builders;
using SFA.DAS.Campaign.Web.Renderers;

namespace SFA.DAS.Campaign.UnitTests.Web.Renderers
{
    public class WhenControlsHtmlRendererIsGivenContent
    {
        [Test]
        public void ThenTheToHtmlMethodReturnsTheConstructedHtml()
        {
            var htmlRenderer = new ControlsHtmlRenderer();
            var controlsToRender = new List<IHtmlControl>();

            controlsToRender.Add(new ParagraphBuilder().AddText("some text").Build());

            var actual = htmlRenderer.ToHtml(controlsToRender);

            actual.Value.Should().NotBeNullOrWhiteSpace();
            actual.Value.Should().Be("<p>some text</p>");
        }
    }
}
