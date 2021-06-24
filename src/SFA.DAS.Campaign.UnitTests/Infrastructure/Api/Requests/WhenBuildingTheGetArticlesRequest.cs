using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Campaign.Infrastructure.Api.Requests;

namespace SFA.DAS.Campaign.UnitTests.Infrastructure.Api.Requests
{
    public class WhenBuildingTheGetArticlesRequest
    {
        [Test]
        public void And_Given_The_Hub_And_Slug_Then_The_Url_Is_Correct()
        {
            //Act
            var actual = new GetArticlesRequest("hub", "slug");

            //Assert
            actual.GetUrl.Should().Be("article/hub/slug");
        }
    }
}
