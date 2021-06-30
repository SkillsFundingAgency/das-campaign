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
    public class WhenImageControlFactory
    {
        [Test, MoqAutoData]
        public void Is_Given_An_Item_Of_Type_Embedded_Asset_Block_Then_Is_Valid_Returns_True(ImageControlFactory factory)
        {
            var control = BuildControl();

            var actual = factory.IsValid(control);

            actual.Should().BeTrue();
        }

        [Test, MoqAutoData]
        public void Is_Given_An_Item_Of_Type_Embedded_Asset_Block_Then_Create_Returns_Image(ImageControlFactory factory)
        {
            var control = BuildControl();

            var actual = factory.Create(control) as Image;

            actual.Should().NotBeNull();
            actual.Title.Should().Be("apprentice-hozanna");
            actual.Url.Should().Be("https://images");
        }
        private static Item BuildControl()
        {
            var control = new ItemBuilder().SetType("embedded-asset-block")
                .AddEmbeddedResource(new EmbeddedResource
                {
                    ContentType = "image/jpeg",
                    FileName = "apprentice-hozanna.jpg",
                    Description = "",
                    Id = "",
                    Title = "apprentice-hozanna",
                    Url = "https://images"
                }).Build();
            return control;
        }
    }
}
