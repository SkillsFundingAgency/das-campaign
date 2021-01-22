using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Campaign.Infrastructure.Api.Requests;

namespace SFA.DAS.Campaign.UnitTests.Infrastructure.Api.Requests
{
    public class WhenBuildingTheGetStandardsBySectorRequest
    {
        [Test, AutoData]
        public void Then_The_Url_Is_Correct(string sector)
        {
            //Act
            var actual = new GetStandardsBySectorRequest(sector);
            
            //Assert
            actual.GetUrl.Should().Be($"trainingcourses?sector={sector}");
        }
    }
}