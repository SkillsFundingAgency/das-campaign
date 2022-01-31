using System.Web;
using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Campaign.Infrastructure.Api.Requests;

namespace SFA.DAS.Campaign.UnitTests.Infrastructure.Api.Requests
{
    public class WhenBuildingGetAdvertsRequest
    {
        [Test, AutoData]
        public void Then_The_Url_Is_Correctly_Constructed_And_Encoded(string postcode, string route, int distance)
        {
            //Arrange
            postcode = $"{postcode} !£$@'''``@%{postcode}";
            route = $"{route} !£$@'''``@%{route}";
            
            //Act
            var actual = new GetAdvertsRequest(postcode, distance, route);
            
            //Assert
            actual.GetUrl.Should().Be($"adverts?postcode={HttpUtility.UrlEncode(postcode)}&route={HttpUtility.UrlEncode(route)}&distance={distance}");
        }
    }
}