using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Campaign.Infrastructure.Api.Requests;

namespace SFA.DAS.Campaign.UnitTests.Infrastructure.Api.Requests
{
    public class WhenBuildingTheGetHomepageRequest
    {
        [Test]
        public void Then_The_Url_Is_Correct()
        {
            var actual = new GetHomepageRequest();

            actual.GetUrl.Should().Be("homepage");
        }

        [Test]
        public void Then_The_Preview_Url_Is_Correct()
        {
            var actual = new GetHomepagePreviewRequest();

            actual.GetUrl.Should().Be("homepage/preview");
        }
    }
}
