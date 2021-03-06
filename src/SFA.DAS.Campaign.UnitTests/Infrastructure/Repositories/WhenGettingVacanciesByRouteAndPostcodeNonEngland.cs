﻿using Microsoft.Rest;
using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Application.Geocode;
using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;
using SFA.DAS.Vacancies.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using SFA.DAS.Campaign.Domain.Vacancies;
using VacanciesApi;
using SFA.DAS.Campaign.Infrastructure.Mappers;
using SFA.DAS.Campaign.Infrastructure.Repositories;
using SFA.DAS.Campaign.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace SFA.DAS.Campaign.Infrastructure.UnitTests.Repositories
{
    
    public class WhenGettingVacanciesByRouteAndPostcodeNonEngland
    {
        private Mock<IGeocodeService> _geocodeService;
        private Mock<IMappingService> _mappingService;
        private Mock<ILivevacanciesAPI> _vacanciesApi;
        private Mock<ILogger<VacanciesRepository>> _logger;
        private VacanciesMapper _vacanciesMapper;
        private CountryMapper _countryMapper;

        private IVacanciesRepository sut;

        private string postcode = "CF10 3AT";
        private CoordinatesResponse coordinatesResponse = new CoordinatesResponse()
        {
            Coordinates = new Coordinates() { Lat = 50, Lon = 50 },
            Country = "Wales",
            ResponseCode = "OK"
        };

        private string _standardIds;

        [SetUp]
        public void Arrange()
        {
            _geocodeService = new Mock<IGeocodeService>();
            _mappingService = new Mock<IMappingService>();
            _vacanciesApi = new Mock<ILivevacanciesAPI>();
            _vacanciesMapper = new VacanciesMapper();
            _logger = new Mock<ILogger<VacanciesRepository>>();
            _countryMapper = new CountryMapper();

            _geocodeService.Setup(s => s.GetFromPostCode(It.IsAny<string>())).ReturnsAsync(coordinatesResponse);

            sut = new VacanciesRepository(_vacanciesApi.Object, _vacanciesMapper, _geocodeService.Object, _mappingService.Object, _logger.Object, _countryMapper, Mock.Of<IStandardsRepository>());

            _standardIds = null;
        }

        [TestCase("CF10 3AT")]
        [TestCase("BT7 1NN")]
        [TestCase("EH8 9LE")]
        public async Task GetVacancyListByRoute_Function_Not_Ran(string postcode)
        {
            await sut.GetByRoute("1", postcode, 10);

            _vacanciesApi.Verify(v => v.SearchApprenticeshipVacanciesByLocationAsync(It.IsAny<double>(), It.IsAny<double>(),
                It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.Is<string>(s => s.Equals(_standardIds))), Times.Never());
        }

        [Test]
        public async Task Then_Coordinates_Are_Retrieved_For_Postcode_By_GeocodeService()
        {
            await sut.GetByRoute("1", postcode, 10);

            _geocodeService.Verify(s => s.GetFromPostCode(postcode), Times.Once);
        }
        
        [Test]
        public async Task Then_Location_Is_Returned()
        {
            var results = await sut.GetByRoute("1", postcode, 20);

            Assert.AreEqual(results.searchLocation.Longitude, coordinatesResponse.Coordinates.Lon);
            Assert.AreEqual(results.searchLocation.Latitude, coordinatesResponse.Coordinates.Lat);
        }

        [TestCase("England", Country.England)]
        [TestCase("Wales", Country.Wales)]
        [TestCase("Scotland", Country.Scotland)]
        [TestCase("Northern Ireland",Country.NorthernIreland)]
        public void coordinates_Map_To_Country(string country, Country testCountryEnum)
        {
            var countryEnum = _countryMapper.MapToCountry(country);

            Assert.AreEqual(countryEnum, testCountryEnum);
        }
    }
}
