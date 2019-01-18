﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Application.Geocode;
using SFA.DAS.Campaign.Domain.Configuration;
using SFA.DAS.Campaign.Domain.Configuration.Models;
using SFA.DAS.Campaign.Models.Vacancy;

namespace SFA.DAS.Campaign.Application.UnitTests.Geocode
{
    [TestFixture]
    public class WhenGettingStaticMapUrl
    {

        private Mock<IMappingConfiguration> _mappingConfiguration;
        private GoogleMappingService sut;
        private IList<Location> locationList;

        [SetUp]
        public void Arrange()
        {
            _mappingConfiguration = new Mock<IMappingConfiguration>();
            _mappingConfiguration.Setup(s => s.ApiKey).Returns("12345");
            _mappingConfiguration.Setup(s => s.StaticHeight).Returns("100");
            _mappingConfiguration.Setup(s => s.StaticWidth).Returns("100");

            locationList = new List<Location>();

            locationList.Add(new Location(){ Latitude = 0.000, Longitude = 52.000 });
            locationList.Add(new Location() { Latitude = 1.000, Longitude = 53.000 });

            sut = new GoogleMappingService(_mappingConfiguration.Object);
        }

        [Test]
        public async Task WhenASingleLocationThenMapWithMarker()
        {
            var expectedResult = "https://maps.googleapis.com/maps/api/staticmap?markers=0,52&size=100x100&scale=2&zoom=10&key=12345";

            var result = sut.GetStaticMapsUrl(new Location() {Latitude = 0.000, Longitude = 52.000});

            Assert.IsNotNull(result);
            Assert.AreEqual(result,expectedResult);

        }

        [Test]
        public async Task WhenMultipleLocationsThenMapWithMarker()
        {
            var expectedResult = "https://maps.googleapis.com/maps/api/staticmap?markers=0,52|1,53&size=100x100&scale=2&zoom=10&key=12345";
            
            var result = sut.GetStaticMapsUrl(locationList);

            Assert.IsNotNull(result);
            Assert.AreEqual(result, expectedResult);

        }
        [Test]
        public async Task WhenMultipleLocationsWithSizeThenMapWithMarker()
        {
            var expectedResult = "https://maps.googleapis.com/maps/api/staticmap?markers=0,52|1,53&size=300x300&scale=2&zoom=10&key=12345";

            var result = sut.GetStaticMapsUrl(locationList,"300","300");

            Assert.IsNotNull(result);
            Assert.AreEqual(result, expectedResult);

        }
    }
}
