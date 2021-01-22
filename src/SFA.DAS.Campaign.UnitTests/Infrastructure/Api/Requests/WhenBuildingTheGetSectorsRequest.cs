using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Campaign.Infrastructure.Api.Requests;

namespace SFA.DAS.Campaign.UnitTests.Infrastructure.Api.Requests
{
    public class WhenBuildingTheGetSectorsRequest
    {
        [Test]
        public void Then_The_Url_Is_Correct()
        {
            //Act
            var actual = new GetSectorsRequest();
            
            //Assert
            actual.GetUrl.Should().Be("sectors");
        }
    }
}