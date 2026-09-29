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
    public class WhenStatsSectionControlFactory
    {
        [Test, MoqAutoData]
        public void Is_Given_An_Item_Of_Embedded_Entry_Block_With_A_Stats_Section_Then_Is_Valid_Returns_True(ResponseHubStatistic statsSection, StatsSectionControlFactory factory)
        {
            var control = new ItemBuilder().SetType("embedded-entry-block").SetStatsSection(statsSection).Build();

            var actual = factory.IsValid(control);

            actual.Should().BeTrue();
        }

        [Test, MoqAutoData]
        public void Is_Given_An_Item_Of_Embedded_Entry_Block_With_No_Stats_Section_Then_Is_Valid_Returns_False(StatsSectionControlFactory factory)
        {
            var control = new ItemBuilder().SetType("embedded-entry-block").Build();

            var actual = factory.IsValid(control);

            actual.Should().BeFalse();
        }

        [Test, MoqAutoData]
        public void Is_Given_An_Item_With_An_Empty_Stats_Section_Then_Is_Valid_Returns_False(StatsSectionControlFactory factory)
        {
            var control = new ItemBuilder().SetType("embedded-entry-block").SetStatsSection(new ResponseHubStatistic { ReferenceText = "a source" }).Build();

            var actual = factory.IsValid(control);

            actual.Should().BeFalse();
        }

        [Test, MoqAutoData]
        public void Is_Given_The_Control_Then_Create_Returns_The_Statistic(ResponseHubStatistic statsSection, StatsSectionControlFactory factory)
        {
            var control = new ItemBuilder().SetType("embedded-entry-block").SetStatsSection(statsSection).Build();

            var actual = factory.Create(control) as HubStatistic;

            actual.Should().BeEquivalentTo(statsSection);
        }

        [Test, MoqAutoData]
        public void Is_Given_A_Paragraph_With_A_Stats_Section_Then_The_Paragraph_Factory_Maps_The_Statistic(ResponseHubStatistic statsSection, ParagraphControlFactory factory)
        {
            var control = new ItemBuilder().SetType("paragraph").SetValue("").SetStatsSection(statsSection).Build();

            var actual = factory.Create(control) as Paragraph;

            actual.StatsSection.Should().BeEquivalentTo(statsSection);
        }
    }
}
