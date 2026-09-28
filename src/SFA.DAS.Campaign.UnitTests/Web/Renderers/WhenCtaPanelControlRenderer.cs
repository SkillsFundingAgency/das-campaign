using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using SFA.DAS.Campaign.UnitTests.Web.Renderers.Builders;
using SFA.DAS.Campaign.Web.Renderers;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Campaign.UnitTests.Web.Renderers
{
    public class WhenCtaPanelControlRenderer
    {
        private const string ExpectedPanelHtml =
            "<a class=\"fiu-panel fiu-panel--left-align fiu-cta-panel\" href=\"https://www.gov.uk/find-training\">" +
            "<span aria-hidden=\"true\" class=\"fiu-cta-panel__icon\"><svg aria-hidden=\"true\" class=\"fiu-icon\" focusable=\"false\"><use href=\"/images/sprite.svg#search_user\"></use></svg></span>" +
            "<h2 class=\"govuk-heading-m govuk-!-margin--top-0\">Find training</h2><p>Search for providers</p>" +
            "<span class=\"fiu-cta-panel__button\">Start now</span>" +
            "</a>";

        [Test, MoqAutoData]
        public void Is_Passed_An_Object_Of_IHtmlControl_That_Is_Of_CtaPanel_Then_Supports_Content_Returns_True(CtaPanel ctaPanel, CtaPanelControlRenderer renderer)
        {
            var actual = renderer.SupportsContent(ctaPanel);

            actual.Should().BeTrue();
        }

        [Test, MoqAutoData]
        public void Is_Passed_An_Object_Of_IHtmlControl_That_Is_Not_Of_CtaPanel_Then_Supports_Content_Returns_False(Table table, CtaPanelControlRenderer renderer)
        {
            var actual = renderer.SupportsContent(table);

            actual.Should().BeFalse();
        }

        [Test, MoqAutoData]
        public void Is_Passed_An_Object_Of_CtaPanel_Then_Render_Returns_The_Html(CtaPanelControlRenderer renderer)
        {
            var actual = renderer.Render(BuildCtaPanel());

            actual.Value.Should().Be(ExpectedPanelHtml);
        }

        [Test, MoqAutoData]
        public void Is_Passed_An_Object_Of_CtaPanel_With_No_Icon_Or_Button_Then_They_Are_Not_Rendered(CtaPanelControlRenderer renderer)
        {
            var ctaPanel = BuildCtaPanel();
            ctaPanel.Icon = null;
            ctaPanel.ButtonText = " ";

            var actual = renderer.Render(ctaPanel);

            actual.Value.Should().NotContain("fiu-cta-panel__icon");
            actual.Value.Should().NotContain("fiu-cta-panel__button");
        }

        [Test, MoqAutoData]
        public void Is_Passed_An_Object_Of_CtaPanel_Then_The_Content_Is_Html_Encoded(CtaPanelControlRenderer renderer)
        {
            var ctaPanel = BuildCtaPanel();
            ctaPanel.Heading = "<script>alert(1)</script>";

            var actual = renderer.Render(ctaPanel);

            actual.Value.Should().NotContain("<script>");
        }

        [Test, MoqAutoData]
        public void Is_Passed_A_Paragraph_That_Only_Has_A_Cta_Panel_Then_Only_The_Panel_Is_Rendered(ParagraphControlRenderer renderer)
        {
            var paragraph = ParagraphBuilder.New().AddText("").AddText("").Build();
            paragraph.CtaPanel = BuildCtaPanel();

            var actual = renderer.Render(paragraph);

            actual.Value.Should().Be(ExpectedPanelHtml);
        }

        [Test, MoqAutoData]
        public void Is_Passed_A_Paragraph_With_Text_And_A_Cta_Panel_Then_Both_Are_Rendered(ParagraphControlRenderer renderer)
        {
            var paragraph = ParagraphBuilder.New().AddText("a line").Build();
            paragraph.CtaPanel = BuildCtaPanel();

            var actual = renderer.Render(paragraph);

            actual.Value.Should().Be("<p>a line</p>" + ExpectedPanelHtml);
        }

        private static CtaPanel BuildCtaPanel()
        {
            return new CtaPanel
            {
                Heading = "Find training",
                Description = "Search for providers",
                Icon = "search_user",
                ButtonText = "Start now",
                Url = "https://www.gov.uk/find-training"
            };
        }
    }
}
