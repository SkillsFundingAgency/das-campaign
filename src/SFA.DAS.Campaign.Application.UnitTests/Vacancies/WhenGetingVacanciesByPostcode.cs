using Microsoft.Rest;
using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Application.Geocode;
using SFA.DAS.Campaign.Application.Vacancies;
using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;
using SFA.DAS.Campaign.Domain.Geocode;
using SFA.DAS.Campaign.Models.Geocode;
using SFA.DAS.Vacancies.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using VacanciesApi;

namespace SFA.DAS.Campaign.Application.UnitTests.Vacancies
{
    [TestFixture]
    public class WhenGetingVacanciesByPostcode
    {
        private Mock<IGeocodeService> _geocodeService;
        private Mock<IMappingService> _mappingService;
        private Mock<ILivevacanciesAPI> _vacanciesApi;
        private VacanciesMapper _vacanciesMapper;

        private VacanciesService sut;

        private string postcode = "SW1 2AA";
        private int _searchResultCount = 200;
        private CoordinatesResponse coordinatesResponse = new CoordinatesResponse()
        {
            Coordinates = new Coordinates() { Lat = 50, Lon = 50 },
            ResponseCode = "200"
        };

        [SetUp]
        public void Arrange()
        {
            _geocodeService = new Mock<IGeocodeService>();
            _mappingService = new Mock<IMappingService>();
            _vacanciesApi = new Mock<ILivevacanciesAPI>();
            _vacanciesMapper = new VacanciesMapper();


            _geocodeService.Setup(s => s.GetFromPostCode(It.IsAny<string>())).ReturnsAsync(coordinatesResponse);


            _vacanciesApi.Setup(s => s.SearchApprenticeshipVacanciesByLocationAsync(It.IsAny<double>(), It.IsAny<double>(),
                It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(mockSearchResults());


            sut = new VacanciesService(_vacanciesApi.Object, _vacanciesMapper, _geocodeService.Object, _mappingService.Object);

        }

        [Test]
        public void Then_Coordinates_Are_Retrieved_For_Postcode_By_GeocodeService()
        {

            var results = sut.GetByPostcode(postcode, 10);
            
            _geocodeService.Verify(s => s.GetFromPostCode(postcode), Times.Once);

        }

        [Test]
        public void And_Less_Than_250_Results_Then_Vacancies_Api_Is_Called_Filtered_On_Postcode_And_Distance()
        {
            _searchResultCount = 200;

            var results = sut.GetByPostcode(postcode, 20);

            _vacanciesApi.Verify(v => v.SearchApprenticeshipVacanciesByLocationAsync(It.IsAny<double>(), It.IsAny<double>(),
                It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()),Times.Once());
        }


        //[Test]
        //public void And_300_Results_Then_Vacancies_Api_Is_Called_Twice_Filtered_On_Postcode_And_Distance()
        //{
        //    _searchResultCount = 300;

        //    _vacanciesApi.Setup(s => s.SearchApprenticeshipVacanciesByLocationAsync(It.IsAny<double>(), It.IsAny<double>(),
        //        It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(mockSearchResults());

        //    var results = sut.GetByPostcode(postcode, 20);

        //    _vacanciesApi.Verify(v => v.SearchApprenticeshipVacanciesByLocationAsync(It.IsAny<double>(), It.IsAny<double>(),
        //        It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(2));

        //}

        [Test]
        public async Task Then_Only_Standards_Returned()
         {
            _searchResultCount = 200;

            var results = await sut.GetByPostcode(postcode, 20);

            Assert.AreEqual(results.Count,100);
        }


        private HttpOperationResponse<object> mockSearchResults()
        {
            var _result = new HttpOperationResponse<object>();

            _result.Request = new HttpRequestMessage();
            _result.Response = new HttpResponseMessage(HttpStatusCode.OK);

            var vacancyResults = new VacancySearchResults();

            var results = new List<Result>();


            while (results.Count != _searchResultCount)
            {
                var random = new Random();
                var id = results.Count + 1;
                results.Add(new Result()
                {
                    Location = new Location() { Latitude = coordinatesResponse.Coordinates.Lat, Longitude = coordinatesResponse.Coordinates.Lon },
                    Title = $"Vacancy {id}",
                    ShortDescription = "short description of vacancy",
                    DistanceInMiles = random.NextDouble() * 100,
                    VacancyReference = id,
                    TrainingType = (id % 2 == 0) ? TrainingType.Framework : TrainingType.Standard
                });
            }

            vacancyResults.Results = results.ToArray();
            _result.Body = vacancyResults;

            return _result;
        }

    }
    //[Test]
    //public async Task Then_Static_Map_Urls_Are_Generated_For_Results()
    //{
    //    _searchResultCount = 200;

    //    var results = await sut.GetByPostcode(postcode, 20);

    //    var mapsWithoutStatic = results.Where(w => string.IsNullOrWhiteSpace(w.StaticMapUrl)).ToList();

    //    Assert.AreEqual(mapsWithoutStatic.Count, 0);


    //}
}
