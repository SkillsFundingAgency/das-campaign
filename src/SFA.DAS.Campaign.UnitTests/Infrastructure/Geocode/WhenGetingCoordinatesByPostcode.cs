using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Application.Geocode;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SFA.DAS.Campaign.Domain.Interfaces;
using SFA.DAS.Campaign.Infrastructure.Geocode;
using SFA.DAS.Campaign.Infrastructure.Geocode.Configuration;
using SFA.DAS.Campaign.Infrastructure.Services;

namespace SFA.DAS.Campaign.Infrastructure.UnitTests.Geocode
{
    [TestFixture]
    public class WhenGetingCoordinatesByPostcode
    {
        private Mock<ILogger<IGeocodeService>> _logger;
        private Mock<IRetryWebRequests> _retryWebRequest;
        private Mock<IPostcodeApiConfiguration> _postcodeApiConfiguration;

        private GeocodeService sut;

        private string postcode = "SW2 1AA";

        private static string postcodeApiResponse =
            "{\"status\":200,\"result\":{\"postcode\":\"SW2 1AA\",\"quality\":1,\"eastings\":530767,\"northings\":174534,\"country\":\"England\",\"nhs_ha\":\"London\",\"longitude\":-0.119331,\"latitude\":51.454693,\"european_electoral_region\":\"London\",\"primary_care_trust\":\"Lambeth\",\"region\":\"London\",\"lsoa\":\"Lambeth 020E\",\"msoa\":\"Lambeth 020\",\"incode\":\"1AA\",\"outcode\":\"SW2\",\"parliamentary_constituency\":\"Streatham\",\"admin_district\":\"Lambeth\",\"parish\":\"Lambeth, unparished area\",\"admin_county\":null,\"admin_ward\":\"Tulse Hill\",\"ccg\":\"NHS Lambeth\",\"nuts\":\"Lambeth\",\"codes\":{\"admin_district\":\"E09000022\",\"admin_county\":\"E99999999\",\"admin_ward\":\"E05000435\",\"parish\":\"E43000212\",\"parliamentary_constituency\":\"E14000978\",\"ccg\":\"E38000092\",\"nuts\":\"UKI45\"}}}";

        private HttpResponseMessage postcodeResponseMessage =
            new HttpResponseMessage(HttpStatusCode.OK) {Content = new StringContent(postcodeApiResponse)};

        [SetUp]
        public void Arrange()
        {
            _logger = new Mock<ILogger<IGeocodeService>>();
            _retryWebRequest = new Mock<IRetryWebRequests>();
            _postcodeApiConfiguration = new Mock<IPostcodeApiConfiguration>();

            _postcodeApiConfiguration.Setup(s => s.Url).Returns("http://test.com");
            sut = new GeocodeService(_logger.Object, _retryWebRequest.Object, _postcodeApiConfiguration.Object);

        }

        [Test]
        public async Task ValidPostcodeReturnsCoordinates()
        {
            _retryWebRequest
                .Setup(s => s.Retry(It.IsAny<Func<Task<HttpResponseMessage>>>(), It.IsAny<Action<Exception>>()))
                .ReturnsAsync(postcodeResponseMessage);

            _retryWebRequest.Setup(s => s.MakeRequestAsync(It.IsAny<string>())).ReturnsAsync(postcodeResponseMessage);

            var result = await sut.GetFromPostCode(postcode);

            Assert.AreEqual(result.ResponseCode,"OK");
            Assert.AreEqual(result.Coordinates.Lat, 51.454693);
            Assert.AreEqual(result.Coordinates.Lon, -0.119331);
        }




    }
}
