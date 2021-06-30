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
    public class WhenYouTubeControlRenderer
    {
        [Test, AutoData]
        public void Is_Passed_An_Object_Of_IHtmlControl_That_Is_Of_YouTube_Then_Supports_Content_Returns_True(YouTube youTube, YouTubeControlRenderer renderer)
        {
            var actual = renderer.SupportsContent(youTube);

            actual.Should().BeTrue();
        }

        [Test, AutoData]
        public void Is_Passed_An_Object_Of_IHtmlControl_That_Is_Not_Of_YouTube_Then_Supports_Content_Returns_False(Table table, YouTubeControlRenderer renderer)
        {
            var actual = renderer.SupportsContent(table);

            actual.Should().BeFalse();
        }

        [Test, AutoData]
        public void Is_Passed_An_Object_Of_YouTube_Then_Render_Returns_The_Html(YouTubeControlRenderer renderer)
        {
            var heading = new YouTube()
            {
               Url = "http://yt.video"
            };
            

            var actual = renderer.Render(heading);

            actual.Value.Should().NotBeNullOrWhiteSpace();
            actual.Value.Should().Be("<div class=\"fiu-video-player\"><div class=\"fiu-video-player__inner-wrap\"><iframe class=\"fiu-video-player__size-100\" src=\"http://yt.video\" allow=\"accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture\" allowfullscreen></iframe></div></div>");
        }
    }
}
