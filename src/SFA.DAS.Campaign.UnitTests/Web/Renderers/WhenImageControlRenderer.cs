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
    public class WhenImageControlRenderer
    {
        [Test, AutoData]
        public void Is_Passed_An_Object_Of_IHtmlControl_That_Is_Of_Image_Then_Supports_Content_Returns_True(Image image, ImageControlRenderer renderer)
        {
            var actual = renderer.SupportsContent(image);

            actual.Should().BeTrue();
        }

        [Test, AutoData]
        public void Is_Passed_An_Object_Of_IHtmlControl_That_Is_Not_Of_Image_Then_Supports_Content_Returns_False(Table table, ImageControlRenderer renderer)
        {
            var actual = renderer.SupportsContent(table);

            actual.Should().BeFalse();
        }

        [Test, AutoData]
        public void Is_Passed_An_Object_Of_Image_Then_Render_Returns_The_Html(ImageControlRenderer renderer)
        {
            var image = new Image
            {
                Description = "description",
                Title = "an image",
                Url = "http://image.com"
            };

            var actual = renderer.Render(image);
            actual.Value.Should().NotBeNullOrWhiteSpace();
            actual.Value.Should().Be("<img alt=\"\" class=\"fiu-article-image\" role=\"presentation\" src=\"http://image.com\" title=\"an image\" />");
            //var paragraph = ParagraphBuilder.New().AddText("a line").Build();
            //var actual = renderer.Render(paragraph);

            //actual.Value.Should().NotBeNullOrWhiteSpace();
            //actual.Value.Should().Be("<p>a line</p>");
        }

    }
}
