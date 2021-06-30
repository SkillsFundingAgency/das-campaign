using System;
using System.Collections.Generic;
using System.Text;
using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using SFA.DAS.Campaign.Web.Renderers;

namespace SFA.DAS.Campaign.UnitTests.Web.Renderers
{
    public class WhenHorizontalRuleRenderer
    {
        [Test, AutoData]
        public void Is_Passed_An_Object_Of_IHtmlControl_That_Is_Of_HorizontalRule_Then_Supports_Content_Returns_True(HorizontalRule hr, HorizontalRuleControlRenderer renderer)
        {
            var actual = renderer.SupportsContent(hr);

            actual.Should().BeTrue();
        }

        [Test, AutoData]
        public void Is_Passed_An_Object_Of_IHtmlControl_That_Is_Not_Of_HorizontalRule_Then_Supports_Content_Returns_False(Table table, HorizontalRuleControlRenderer renderer)
        {
            var actual = renderer.SupportsContent(table);

            actual.Should().BeFalse();
        }

        [Test, AutoData]
        public void Is_Passed_An_Object_Of_HorizontalRule_Then_Render_Returns_The_Html(HorizontalRuleControlRenderer renderer)
        {
            var rule = new HorizontalRule();
          
            var actual = renderer.Render(rule);

            actual.Value.Should().NotBeNullOrWhiteSpace();
            actual.Value.Should().Be("<hr />");
        }
    }
}
