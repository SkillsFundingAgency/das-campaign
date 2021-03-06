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
using Microsoft.Extensions.Logging;

namespace SFA.DAS.Campaign.Infrastructure.UnitTests.Repositories
{
    [TestFixture]
    public class WhenGettingVacanciesByRouteAndPostcodeEngland
    {
        private Mock<IGeocodeService> _geocodeService;
        private Mock<IMappingService> _mappingService;
        private Mock<ILivevacanciesAPI> _vacanciesApi;
        private Mock<IStandardsRepository> _standardsService;
        private VacanciesMapper _vacanciesMapper;
        private Mock<ILogger<VacanciesRepository>> _logger;
        private CountryMapper _countryMapper;

        private IVacanciesRepository sut;

        private string postcode = "SW1 2AA";
        private string routeId = "1";
        private int _searchResultCount = 200;
        private CoordinatesResponse coordinatesResponse = new CoordinatesResponse()
        {
            Coordinates = new Coordinates() { Lat = 50, Lon = 50 },
            Country = "England",
            ResponseCode = "OK"
        };

        private List<int> _standards;
        private string _standardIds;

        [SetUp]
        public void Arrange()
        {
            _geocodeService = new Mock<IGeocodeService>();
            _mappingService = new Mock<IMappingService>();
            _vacanciesApi = new Mock<ILivevacanciesAPI>();
            _standardsService = new Mock<IStandardsRepository>();
            _vacanciesMapper = new VacanciesMapper();
            _logger = new Mock<ILogger<VacanciesRepository>>();
            _countryMapper = new CountryMapper();

            _standards = new List<int>() {1, 2};
                

            _standardIds = string.Join(',', _standards);

            _geocodeService.Setup(s => s.GetFromPostCode(It.IsAny<string>())).ReturnsAsync(coordinatesResponse);


            _vacanciesApi.Setup(s => s.SearchApprenticeshipVacanciesByLocationAsync(It.IsAny<double>(), It.IsAny<double>(),
                It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(GetSearchResults.MockSearchResults(coordinatesResponse));

            _standardsService.Setup(s => s.GetByRoute(routeId)).ReturnsAsync(_standards);

            sut = new VacanciesRepository(_vacanciesApi.Object, _vacanciesMapper, _geocodeService.Object, _mappingService.Object,  _logger.Object, _countryMapper, _standardsService.Object);

        }

        [Test]
        public async Task Then_Coordinates_Are_Retrieved_For_Postcode_By_GeocodeService()
        {

            await sut.GetByRoute("1", postcode, 10);

            _geocodeService.Verify(s => s.GetFromPostCode(postcode), Times.Once);

        }

        [Test]
        public async Task And_Less_Than_250_Results_Then_Vacancies_Api_Is_Called_Filtered_On_Postcode_And_Distance()
        {
            _searchResultCount = 200;

            await sut.GetByRoute("1", postcode, 10);

            _vacanciesApi.Verify(v => v.SearchApprenticeshipVacanciesByLocationAsync(It.IsAny<double>(), It.IsAny<double>(),
                It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), Times.Once());
        }


        [Test]
        public async Task Then_Only_Standards_Returned()
        {
            _searchResultCount = 200;

            var results = await sut.GetByRoute("1", postcode, 20);

            Assert.AreEqual(results.Results.Count,100);
        }
        
        [Test]
        public async Task Then_Location_Is_Returned()
        {
            _searchResultCount = 200;

            var results = await sut.GetByRoute("1", postcode, 20);

            Assert.AreEqual(results.searchLocation.Longitude, coordinatesResponse.Coordinates.Lon);
            Assert.AreEqual(results.searchLocation.Latitude, coordinatesResponse.Coordinates.Lat);
        }

        [Test]
        public async Task Then_Only_Standards_With_Expected_Route_Returned()
        {
            _searchResultCount = 200;

            var results = await sut.GetByRoute("1", postcode, 10);

            _vacanciesApi.Verify(v => v.SearchApprenticeshipVacanciesByLocationAsync(It.IsAny<double>(), It.IsAny<double>(),
                It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.Is<string>(s => s.Equals(_standardIds))), Times.Once());
        }
    }
}
