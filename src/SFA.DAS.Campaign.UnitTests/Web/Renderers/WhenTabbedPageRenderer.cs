using System.Collections.Generic;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.NUnit3;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.VisualBasic;
using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using SFA.DAS.Campaign.Web.Renderers;

namespace SFA.DAS.Campaign.UnitTests.Web.Renderers
{
    public class WhenTabbedContentRenderer
    {
        [Test, AutoData]
        public void Is_Passed_An_Object_Of_IHtmlControl_That_Is_Of_Tabs_Then_Supports_Content_Returns_True()
        {
            TabbedContentRenderer renderer = new TabbedContentRenderer();
          
            var tabs = new Tabs();
            var tabbedContent = new List<TabbedContent>();
            tabbedContent.Add(new TabbedContent());

            var actual = renderer.SupportsContent(tabs);

            actual.Should().BeTrue();
        }

        [Test, AutoData]
        public void Is_Passed_An_Object_Of_IHtmlControl_That_Is_Not_Of_Tabs_Then_Supports_Content_Returns_False(Table table)
        {
            TabbedContentRenderer renderer = new TabbedContentRenderer();
            var actual = renderer.SupportsContent(table);

            actual.Should().BeFalse();
        }

        [Test, AutoData]
        public void Is_Passed_An_Object_Of_TabbedContent_Then_Render_Returns_The_Html()
        {
            var renderer = new TabbedContentRenderer();
            var context = new Mock<HttpContext>();
            var tempDictionary = new Mock<ITempDataDictionary>();

            renderer.HttpContext = context.Object;
            renderer.TempDataDictionary = tempDictionary.Object;

            var tabs = new Tabs();
            var tabbedContent = new List<TabbedContent>
            {
                new TabbedContent
                {
                    TabTitle = "Title",
                    TabName = "TabName",
                    Content = new List<IHtmlControl>()
                    {
                        new Heading {HeadingSize = 2, Content = new List<string>() {"a heading"}}
                    }
                }
            };
            
            tabs.TabbedContents = tabbedContent;

            var actual = renderer.Render(tabs);

            actual.Value.Should().NotBeNullOrWhiteSpace();
            actual.Value.Should().Be("<div class=\"govuk-grid-row app-anchor-links\"><div class=\"govuk-grid-column-one-third\"><h2 class=\"govuk-heading-m\">On this page</h2><ul class=\"govuk-list govuk-list--spaced app-anchor-links__list\"><li class=\"app-anchor-links__list-item\"><a class=\"govuk-link govuk-link--no-visited-state app-anchor-links__link\" href=\"#Title\">Title</a></li></ul></div><div class=\"govuk-grid-column-two-thirds\"><div class=\"app-anchor-links__panel\" id=\"Title\"><article class=\"fiu-article\"><h2>a heading</h2></article></div></div></div>");
        }
    }
}
