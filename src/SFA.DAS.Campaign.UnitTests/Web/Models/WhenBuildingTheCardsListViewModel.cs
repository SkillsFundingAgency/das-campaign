using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Campaign.Web.Models;

namespace SFA.DAS.Campaign.UnitTests.Web.Models
{
    public class WhenBuildingTheCardsListViewModel
    {
        [Test]
        public void Then_Items_Without_A_Url_Are_Not_Rendered()
        {
            var model = new CardsListViewModel
            {
                Items = new List<CardsListItem>
                {
                    new CardsListItem { Text = "Has url", Url = "/a" },
                    new CardsListItem { Text = "No url", Url = " " },
                    null
                }
            };

            model.RenderableItems.Should().ContainSingle(i => i.Text == "Has url");
            model.HasItems.Should().BeTrue();
        }

        [Test]
        public void Then_An_Empty_List_Has_No_Items()
        {
            new CardsListViewModel().HasItems.Should().BeFalse();
            new CardsListViewModel { Items = null }.HasItems.Should().BeFalse();
            new CardsListViewModel { Items = new List<CardsListItem>() }.HasItems.Should().BeFalse();
        }

        [TestCase(2, "h2")]
        [TestCase(3, "h3")]
        [TestCase(0, "h3")]
        [TestCase(9, "h3")]
        public void Then_The_Sub_Heading_Tag_Is_Clamped_To_A_Valid_Level(int level, string expected)
        {
            new CardsListViewModel { SubHeadingLevel = level }.SubHeadingTag.Should().Be(expected);
        }
    }
}
