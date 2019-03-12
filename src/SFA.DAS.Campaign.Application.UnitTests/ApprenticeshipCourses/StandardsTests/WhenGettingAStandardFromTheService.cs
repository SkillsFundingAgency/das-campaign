using Ifa.Api;
using Ifa.Api.Model;
using Moq;
using NUnit.Framework;
using SFA.DAS.Apprenticeships.Api.Client;
using SFA.DAS.Apprenticeships.Api.Types;
using SFA.DAS.Campaign.Application.ApprenticeshipCourses.Services;
using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;
using SFA.DAS.Campaign.Models.ApprenticeshipCourses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Application.UnitTests.ApprenticeshipCourses.StandardsTests
{
    public class WhenGettingAStandardFromTheService
    {
        private StandardsService _standardsService;
        private Mock<IApprenticeshipProgrammeApiClient> _apprenticeshipProgrammeApiClient;
        private Mock<IStandardsMapper> _standardsMApper;
        private Mock<IFullStandardsApi> _fullStandardsApi;

        //Arrange
        string routeId = "1";

        [SetUp]
        public void Arrange()
        {
            _standardsMApper = new Mock<IStandardsMapper>();
            _apprenticeshipProgrammeApiClient = new Mock<IApprenticeshipProgrammeApiClient>();
            _fullStandardsApi = new Mock<IFullStandardsApi>();
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

            _fullStandardsApi.Setup(s => s.FullStandardsGetAllAsync())
                .ReturnsAsync(new List<TempApprenticeshipStandard>()
                {
                    new TempApprenticeshipStandard
                    {
                        ApprovedForDelivery = DateTime.Now.Subtract(new TimeSpan(3,0,0)),
                        Route = "1",
                        Duration = 24,
                        Title =  "Standard 123",
                        ID = 123,
                        IsPublished = true
                    },
                    new TempApprenticeshipStandard
                    {
                        ApprovedForDelivery = DateTime.Now.Subtract(new TimeSpan(3,0,0)),
                        Route = "2",
                        Duration = 24,
                        Title =  "Standard 234",
                        ID = 234,
                        IsPublished = true
                    },
                    new TempApprenticeshipStandard
                    {
                        ApprovedForDelivery = DateTime.Now.Subtract(new TimeSpan(3,0,0)),
                        Route = "3",
                        Duration = 24,
                        Title =  "Standard 345",
                        ID = 345,
                        IsPublished = true
                    },
                    new TempApprenticeshipStandard
                    {
                        Route = "1",
                        Duration = 24,
                        Title =  "Standard 456",
                        ID = 456,
                        IsPublished = false
                    },
                });


            _standardsService = new StandardsService(_apprenticeshipProgrammeApiClient.Object, _standardsMApper.Object, _fullStandardsApi.Object);
        }

        [Test]
        public async Task And_By_Search_Term_Then_The_Api_Is_Called_To_Get_Standards_Filtering_On_SearchTerm()
        {
            //Arrange
            var searchTerm = "test standard";

            //Act
            await _standardsService.GetBySearchTerm(searchTerm);

            //Assert
            _apprenticeshipProgrammeApiClient.Verify(x => x.SearchAsync(searchTerm, 1), Times.Once);
        }

        [Test]
        public async Task And_By_Search_Term_Then_The_Results_Are_Filtered_For_Active_Standards()
        {
            //Act
            var actual = await _standardsService.GetBySearchTerm("test");

            //Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(1, actual.Count);
        }

        [Test]
        public async Task And_By_Search_Term_Then_The_Results_Are_Mapped_To_The_Results_Object()
        {
            //Act
            var actual = await _standardsService.GetBySearchTerm("test");

            //Assert
            Assert.IsAssignableFrom<List<StandardResultItem>>(actual);
        }

        [Test]
        public async Task And_By_Route_Then_The_IFA_Api_Is_Called_To_Get_Standards()
        {
            //Act
            await _standardsService.GetByRoute(routeId);

            //Assert
            _fullStandardsApi.Verify(x => x.FullStandardsGetAllAsync(), Times.Once);
        }

        [Test]
        public async Task And_By_Route_Then_The_Results_Are_Filtered_For_Route_And_Active_Standards()
        {
            //Act
            var actual = await _standardsService.GetByRoute(routeId);

            //Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(1, actual.Count);
        }

        [Test]
        public async Task And_By_Route_Then_The_Results_Are_Mapped_To_The_Results_Object()
        {
            //Act
            var actual = await _standardsService.GetByRoute(routeId);

            //Assert
            Assert.IsAssignableFrom<List<StandardResultItem>>(actual);
        }
    }
}
