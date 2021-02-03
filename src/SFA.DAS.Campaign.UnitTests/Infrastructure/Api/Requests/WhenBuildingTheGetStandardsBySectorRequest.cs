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
        public void Then_The_Url_Is_Correct_and_Encoded(string sector)
        {
            //Arrange
            var queryParam = $"{sector} {sector}";
            var expected = HttpUtility.UrlEncode(queryParam);
            
            //Act
            var actual = new GetStandardsBySectorRequest(queryParam);
            
            //Assert
            actual.GetUrl.Should().Be($"trainingcourses?sector={expected}");
        }
    }
}