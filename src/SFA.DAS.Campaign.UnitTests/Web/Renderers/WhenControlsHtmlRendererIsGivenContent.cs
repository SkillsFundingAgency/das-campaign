using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using SFA.DAS.Campaign.UnitTests.Web.Renderers.Builders;
using SFA.DAS.Campaign.Web.Renderers;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Campaign.UnitTests.Web.Renderers
{
    public class WhenControlsHtmlRendererIsGivenContent
    {
        [Test, MoqAutoData]
        public void Then_The_To_Html_Method_Returns_The_Constructed_Html(ControlsHtmlRenderer htmlRenderer)
        {
            var controlsToRender = new List<IHtmlControl>
            {
                new ParagraphBuilder().AddText("some text").Build()
            };

            var actual = htmlRenderer.ToHtml(controlsToRender);

            actual.Value.Should().NotBeNullOrWhiteSpace();
            actual.Value.Should().Be("<p>some text</p>");
        }
    }
}
