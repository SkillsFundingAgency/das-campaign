using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using SFA.DAS.Campaign.Infrastructure.Api.Converters;
using SFA.DAS.Campaign.Infrastructure.Api.Factory;
using SFA.DAS.Campaign.UnitTests.Infrastructure.Api.Factory.Builders;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Campaign.UnitTests.Infrastructure.Api.Factory
{
    public class WhenCtaPanelControlFactory
    {
        [Test, MoqAutoData]
        public void Is_Given_An_Item_Of_Embedded_Entry_Block_With_A_Cta_Panel_Then_Is_Valid_Returns_True(ResponseCtaPanel ctaPanel, CtaPanelControlFactory factory)
        {
            var control = new ItemBuilder().SetType("embedded-entry-block").SetCtaPanel(ctaPanel).Build();

            var actual = factory.IsValid(control);

            actual.Should().BeTrue();
        }

        [Test, MoqAutoData]
        public void Is_Given_An_Item_Of_Embedded_Entry_Block_With_No_Cta_Panel_Then_Is_Valid_Returns_False(CtaPanelControlFactory factory)
        {
            var control = new ItemBuilder().SetType("embedded-entry-block").Build();

            var actual = factory.IsValid(control);

            actual.Should().BeFalse();
        }

        [Test, MoqAutoData]
        public void Is_Given_The_Control_Then_Create_Returns_The_Cta_Panel(ResponseCtaPanel ctaPanel, CtaPanelControlFactory factory)
        {
            var control = new ItemBuilder().SetType("embedded-entry-block").SetCtaPanel(ctaPanel).Build();

            var actual = factory.Create(control) as CtaPanel;

            actual.Should().BeEquivalentTo(ctaPanel);
        }

        [Test, MoqAutoData]
        public void Is_Given_A_Paragraph_With_A_Cta_Panel_Then_The_Paragraph_Factory_Maps_The_Cta_Panel(ResponseCtaPanel ctaPanel, ParagraphControlFactory factory)
        {
            var control = new ItemBuilder().SetType("paragraph").SetValue("").SetCtaPanel(ctaPanel).Build();

            var actual = factory.Create(control) as Paragraph;

            actual.CtaPanel.Should().BeEquivalentTo(ctaPanel);
        }
    }
}
