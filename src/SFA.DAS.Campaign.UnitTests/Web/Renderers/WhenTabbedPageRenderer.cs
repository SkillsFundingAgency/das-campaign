using System.Collections.Generic;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.NUnit3;
using FluentAssertions;
using Microsoft.Azure.Cosmos.Table;
using NUnit.Framework;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using SFA.DAS.Campaign.Web.Renderers;

namespace SFA.DAS.Campaign.UnitTests.Web.Renderers
{
    public class WhenTabbedContentRenderer
    {
        [Test, AutoData]
        public void Is_Passed_An_Object_Of_IHtmlControl_That_Is_Of_Tabs_Then_Supports_Content_Returns_True(TabbedContentRenderer renderer)
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization
            {
                ConfigureMembers = true
            });

            var controls = fixture.CreateMany<IHtmlControl>();
            var tabs = new Tabs();
            var tabbedContent = new List<TabbedContent>();
            tabbedContent.Add(new TabbedContent
            {
                Content = controls
            });

            var actual = renderer.SupportsContent(tabs);

            actual.Should().BeTrue();
        }

        [Test, AutoData]
        public void Is_Passed_An_Object_Of_IHtmlControl_That_Is_Not_Of_Tabs_Then_Supports_Content_Returns_False(Table table, TabbedContentRenderer renderer)
        {
            var actual = renderer.SupportsContent(table);
            
            actual.Should().BeFalse();
        }

        [Test, AutoData]
        public void Is_Passed_An_Object_Of_TabbedContent_Then_Render_Returns_The_Html(TabbedContentRenderer renderer)
        {
            var tabs = new Tabs();
            var tabbedContent = new List<TabbedContent>();
            
            tabbedContent.Add(new TabbedContent
            {
                TabTitle = "Title",
                TabName = "TabName",
                Content = new List<IHtmlControl>()
                {
                    new Heading
                    {
                        HeadingSize = 2, 
                        Content = new List<string>()
                        {
                            "a heading"
                        }
                    }
                }
            });

            tabs.TabbedContents = tabbedContent;

            var actual = renderer.Render(tabs);

            actual.Value.Should().NotBeNullOrWhiteSpace();
            actual.Value.Should().Be("<div class=\"fiu-tabs\" data-fiu-tabs=\"True\"><h2 class=\"fiu-tabs__title\">Contents</h2><ul class=\"fiu-tabs__list\"><li class=\"fiu-tabs__list-item fiu-tabs__list-item--selected\"><a class=\"fiu-tabs__tab\" href=\"#Title\">Title</a></li></ul><div class=\"fiu-tabs__panel\" id=\"Title\"><article class=\"fiu-article\"><h2>a heading</h2></article></div></div>");
        }
    }
}
