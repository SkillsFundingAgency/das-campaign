using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Campaign.Infrastructure.Api.Requests;
using SFA.DAS.Testing.AutoFixture;
using System.Web;

namespace SFA.DAS.Campaign.UnitTests.Infrastructure.Api.Requests
{
    public class WhenBuildingTheGetStandardRequest
    {
        [Test, AutoData]
        public void Then_The_Url_Is_Correct_and_Encoded(string standardUId)
        {
            var queryParam = $"{standardUId}";
            var expected = HttpUtility.UrlEncode(queryParam);

            var actual = new GetStandardRequest(queryParam);

            actual.GetUrl.Should().Be($"trainingcourses/{expected}");
        }
    }
}
