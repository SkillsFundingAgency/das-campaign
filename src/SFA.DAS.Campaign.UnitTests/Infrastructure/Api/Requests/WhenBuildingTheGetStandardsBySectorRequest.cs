using System.Web;
using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Campaign.Infrastructure.Api.Requests;

namespace SFA.DAS.Campaign.UnitTests.Infrastructure.Api.Requests
{
    public class WhenBuildingTheGetStandardsBySectorRequest
    {
        [Test, AutoData]
        public void AndThereIsASector_ThenTheUrlIsCorrectAndEncoded(string sector)
        {
            var queryParam = $"{sector} {sector}";
            var expected = HttpUtility.UrlEncode(queryParam);
            
            var actual = new GetStandardsBySectorRequest(queryParam);
            
            actual.GetUrl.Should().Be($"trainingcourses?sector={expected}");
        }

        [Test, AutoData]
        public void AndThereIsNoSector_ThenTheUrlIsCorrectAndEncoded()
        {
            var actual = new GetStandardsBySectorRequest(null);

            actual.GetUrl.Should().Be($"trainingcourses");
        }
    }
}