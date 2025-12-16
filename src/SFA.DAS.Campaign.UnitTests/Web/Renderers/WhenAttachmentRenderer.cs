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
    public class WhenAttachmentRenderer
    {
        [Test, AutoData]
        public void Is_Passed_An_Object_Of_IHtmlControl_That_Is_Of_Attachment_Then_Supports_Content_Returns_True(DocumentAttachment attachment, AttachmentControlRenderer renderer)
        {
            var actual = renderer.SupportsContent(attachment);

            actual.Should().BeTrue();
        }

        [Test, AutoData]
        public void Is_Passed_An_Object_Of_IHtmlControl_That_Is_Not_Of_Attachment_Then_Supports_Content_Returns_False(Table table, AttachmentControlRenderer renderer)
        {
            var actual = renderer.SupportsContent(table);

            actual.Should().BeFalse();
        }

        [Test, AutoData]
        public void Is_Passed_An_Object_Of_Attachment_Then_Render_Returns_The_Html(AttachmentControlRenderer renderer)
        {
            var attachment = new DocumentAttachment()
            {
                Url = "http://image.url",
                Description = "description",
                Title = "title",
                FileSize = 1000,
                FileType = "application/pdf"
            };
          
            var actual = renderer.Render(attachment);

            actual.Value.Should().NotBeNullOrWhiteSpace();
            actual.Value.Should().Be("<div class=\"fiu-attachment\"><h2 class=\"govuk-heading-m fiu-attachment__heading\">title</h2><dl class=\"fiu-attachment__meta\"><dt class=\"fiu-attachment__meta-title\">File type</dt><dd class=\"fiu-attachment__meta-description\">pdf</dd><dt class=\"fiu-attachment__meta-title\">File size</dt><dd class=\"fiu-attachment__meta-description\">1KB</dd></dl><p class=\"govuk-body fiu-attachment__description\">description</p><p class=\"govuk-body fiu-attachment__link-wrap\"><a href=\"http://image.url\" class=\"govuk-link fiu-attachment__link\" target=\"_blank\">Download <span class=\"fiu-vh\">title</span></a></p></div>");
        }
    }
}
