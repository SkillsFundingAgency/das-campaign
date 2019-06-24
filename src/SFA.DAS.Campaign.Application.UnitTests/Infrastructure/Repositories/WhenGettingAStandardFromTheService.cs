using Ifa.Api.Model;
using Moq;
using NUnit.Framework;
using SFA.DAS.Apprenticeships.Api.Client;
using SFA.DAS.Apprenticeships.Api.Types;
using SFA.DAS.Campaign.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ifa.Api.Api;
using SFA.DAS.Campaign.Infrastructure.Repositories;
using SFA.DAS.Campaign.Infrastructure.Mappers;
using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;

namespace SFA.DAS.Campaign.Infrastructure.UnitTests.Repositories
{
    public class WhenGettingAStandardFromTheService
    {
        private StandardsRepository _standardsRepository;
        private Mock<IApprenticeshipProgrammeApiClient> _apprenticeshipProgrammeApiClient;
        private Mock<IStandardsMapper> _standardsMApper;
        private Mock<IApprenticeshipStandardsApi> _fullStandardsApi;
        private Mock<ICacheStorageService> _cacheService;

        //Arrange
        string _routeId = "1";
        string _cachedKey = "FullStandardsAPI";

        [SetUp]
        public void Arrange()
        {
            var standardsApiResult = new List<ApiApprenticeshipStandard>()
            {
                new ApiApprenticeshipStandard
                {
                    ApprovedForDelivery = DateTime.Now.Subtract(new TimeSpan(3, 0, 0)),
                    Route = "1",
                    TypicalDuration = 24,
                    Title = "Standard 123",
                    LarsCode = 123,
                    Status = "Approved for delivery"
                },
                new ApiApprenticeshipStandard
                {
                    ApprovedForDelivery = DateTime.Now.Subtract(new TimeSpan(3, 0, 0)),
                    Route = "2",
                    TypicalDuration = 24,
                    Title = "Standard 234",
                    LarsCode = 234,
                    Status = "Approved for delivery"
                },
                new ApiApprenticeshipStandard
                {
                    ApprovedForDelivery = DateTime.Now.Subtract(new TimeSpan(3, 0, 0)),
                    Route = "3",
                    TypicalDuration = 24,
                    Title = "Standard 345",
                    LarsCode = 345,
                    Status = "Approved for delivery"
                },
                new ApiApprenticeshipStandard
                {
                    Route = "1",
                    TypicalDuration = 24,
                    Title = "Standard 456",
                    LarsCode = 456,
                    Status = "Not Approved for delivery"
                },
            };


            _standardsMApper = new Mock<IStandardsMapper>();
            _apprenticeshipProgrammeApiClient = new Mock<IApprenticeshipProgrammeApiClient>();
            _fullStandardsApi = new Mock<IApprenticeshipStandardsApi>();
            _cacheService = new Mock<ICacheStorageService>();
            _apprenticeshipProgrammeApiClient.Setup(c => c.SearchAsync(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(new List<ApprenticeshipSearchResultsItem>
                {
                    new ApprenticeshipSearchResultsItem
                    {
                        Published = true,
                        StandardId = "123",
                        FrameworkId = null,
                        Title = "Test"
                    },
                    new ApprenticeshipSearchResultsItem
                    {
                        Published = false,
                        StandardId = "789",
                        FrameworkId = null,
                        Title = "Test2"
                    },
                    new ApprenticeshipSearchResultsItem
                    {
                        Published = true,
                        StandardId = null,
                        FrameworkId = "456",
                        Title = "Test"
                    },
                });

            _fullStandardsApi.Setup(s => s.ApprenticeshipStandardsGet_3Async())
                .ReturnsAsync(standardsApiResult);

            _cacheService
                .Setup(s =>
                    s.RetrieveFromCache<List<ApiApprenticeshipStandard>>( _cachedKey))
                .ReturnsAsync(standardsApiResult);


            _standardsRepository = new StandardsRepository(_apprenticeshipProgrammeApiClient.Object, _standardsMApper.Object, _fullStandardsApi.Object, _cacheService.Object);
        }

        [Test]
        public async Task And_By_Search_Term_Then_The_Api_Is_Called_To_Get_Standards_Filtering_On_SearchTerm()
        {
            //Arrange
            var searchTerm = "test standard";

            //Act
            await _standardsRepository.GetBySearchTerm(searchTerm);

            //Assert
            _apprenticeshipProgrammeApiClient.Verify(x => x.SearchAsync(searchTerm, 1), Times.Once);
        }

        [Test]
        public async Task And_By_Search_Term_Then_The_Results_Are_Filtered_For_Active_Standards()
        {
            //Act
            var actual = await _standardsRepository.GetBySearchTerm("test");

            //Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(1, actual.Count);
        }

        [Test]
        public async Task And_By_Search_Term_Then_The_Results_Are_Mapped_To_The_Results_Object()
        {
            //Act
            var actual = await _standardsRepository.GetBySearchTerm("test");

            //Assert
            Assert.IsAssignableFrom<List<StandardResultItem>>(actual);
        }

        [Test]
        public async Task And_By_Route_And_First_Call_Then_The_IFA_Api_Is_Called_To_Get_Standards()
        {
         Mock<ICacheStorageService> noncacheService = new Mock<ICacheStorageService>();

        _standardsRepository = new StandardsRepository(_apprenticeshipProgrammeApiClient.Object, _standardsMApper.Object, _fullStandardsApi.Object, noncacheService.Object);

            //Act
            await _standardsRepository.GetByRoute(_routeId);

            //Assert
            _fullStandardsApi.Verify(x => x.ApprenticeshipStandardsGet_3Async(), Times.Once);
        }

        [Test]
        public async Task And_By_Route_And_First_Call_Then_The_Standards_Are_Stored_In_Cache()
        {
            _cacheService.Reset();

            //Act
            var result = await _standardsRepository.GetByRoute(_routeId);

            _cacheService.Verify(x => x.SaveToCache(_cachedKey, It.IsAny<List<ApiApprenticeshipStandard>>(), It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()), Times.Once);

            _fullStandardsApi.Verify(v => v.ApprenticeshipStandardsGet_3Async(), Times.Once);
        }

        [Test]
        public async Task And_By_Route_Then_The_Results_Are_Filtered_For_Route_And_Active_Standards()
        {
            //Act
            var actual = await _standardsRepository.GetByRoute(_routeId);

            //Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(1, actual.Count);
        }

        [Test]
        public async Task And_By_Route_Then_The_Results_Are_Mapped_To_The_Results_Object()
        {
            //Act
            var actual = await _standardsRepository.GetByRoute(_routeId);

            //Assert
            Assert.IsAssignableFrom<List<StandardResultItem>>(actual);
        }
    }
}
