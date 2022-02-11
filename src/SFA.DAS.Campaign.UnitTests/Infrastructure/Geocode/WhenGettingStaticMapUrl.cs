using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Application.Geocode;
using SFA.DAS.Campaign.Infrastructure.Geocode.Configuration;

namespace SFA.DAS.Campaign.Infrastructure.UnitTests.Geocode
{
    [TestFixture]
    public class WhenGettingStaticMapUrl
    {

        private Mock<IMappingConfiguration> _mappingConfiguration;
        private GoogleMappingService _sut;

        [SetUp]
        public void Arrange()
        {
            _mappingConfiguration = new Mock<IMappingConfiguration>();
            _mappingConfiguration.Setup(s => s.ApiKey).Returns("12345");
            _mappingConfiguration.Setup(s => s.StaticHeight).Returns("100");
            _mappingConfiguration.Setup(s => s.StaticWidth).Returns("100");


            _sut = new GoogleMappingService(_mappingConfiguration.Object);
        }

        [Test]
        public void WhenASingleLocationThenMapWithMarker()
        {
            var expectedResult = "https://maps.googleapis.com/maps/api/staticmap?markers=0,52&size=100x100&scale=2&zoom=10&key=12345";

            var result = _sut.GetStaticMapsUrl(0.000, 52.000);

            Assert.IsNotNull(result);
            Assert.AreEqual(result,expectedResult);

        }
    }
}
