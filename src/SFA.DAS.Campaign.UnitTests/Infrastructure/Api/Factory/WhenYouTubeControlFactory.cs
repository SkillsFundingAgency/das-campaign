using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using SFA.DAS.Campaign.Infrastructure.Api.Converters;
using SFA.DAS.Campaign.Infrastructure.Api.Factory;
using SFA.DAS.Campaign.UnitTests.Infrastructure.Api.Factory.Builders;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Campaign.UnitTests.Infrastructure.Api.Factory
{
    public class WhenYouTubeControlFactory
    {
        [Test, MoqAutoData]
        public void Is_Given_An_Item_Of_Type_Paragraph_And_Value_Contains_A_YouTube_Url_Then_Is_Valid_Returns_True(YouTubeControlFactory factory)
        {
            var control = BuildControl();

            var actual = factory.IsValid(control);

            actual.Should().BeTrue();
        }

        [Test, MoqAutoData]
        public void Is_Given_An_Item_Of_Type_Paragraph_And_Value_Does_Not_Contain_A_YouTube_Url_Then_Is_Valid_Returns_False(YouTubeControlFactory factory)
        {
            var control = new ItemBuilder().SetType("paragraph")
                .SetValue("To become an apprentice, you must: ")
                .SetTableValue(new List<string>())
                .Build();

            var actual = factory.IsValid(control);

            actual.Should().BeFalse();
        }

        [Test, MoqAutoData]
        public void Is_Given_An_Item_Of_Type_Paragraph_And__Value_Contains_A_YouTube_Url_Then_Create_Returns_Paragraph(YouTubeControlFactory factory)
        {
            var control = BuildControl();

            var actual = factory.Create(control) as YouTube;

            actual.Should().NotBeNull();
            actual.Url.Should().Be("https://www.youtube.com/embed/vSLcbljkhwU?modestbranding=1");
        }

        private static Item BuildControl()
        {
            var control = new ItemBuilder().SetType("paragraph")
                .SetValue("https://www.youtube.com/embed/vSLcbljkhwU?modestbranding=1").Build();
            return control;
        }
    }
}
