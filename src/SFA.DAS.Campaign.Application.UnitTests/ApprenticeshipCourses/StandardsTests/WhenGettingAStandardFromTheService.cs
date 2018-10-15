using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SFA.DAS.Apprenticeships.Api.Client;
using SFA.DAS.Apprenticeships.Api.Types;
using SFA.DAS.Campaign.Application.ApprenticeshipCourses.Services;
using SFA.DAS.Campaign.Models.ApprenticeshipCourses;

namespace SFA.DAS.Campaign.Application.UnitTests.ApprenticeshipCourses.StandardsTests
{
    public class WhenGettingAStandardFromTheService
    {
        private StandardsService _standardsService;
        private Mock<IApprenticeshipProgrammeApiClient> _apprenticeshipProgrammeApiClient;
        private Mock<IStandardsMapper> _standardsMApper;

        [SetUp]
        public void Arrange()
        {
            _standardsMApper = new Mock<IStandardsMapper>();
            _apprenticeshipProgrammeApiClient = new Mock<IApprenticeshipProgrammeApiClient>();
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


            _standardsService = new StandardsService(_apprenticeshipProgrammeApiClient.Object, _standardsMApper.Object);
        }

        [Test]
        public async Task Then_The_Api_Is_Called_To_Get_Standards_Filtering_On_SearchTerm()
        {
            //Arrange
            var searchTerm = "test standard";

            //Act
            await _standardsService.GetBySearchTerm(searchTerm);

            //Assert
            _apprenticeshipProgrammeApiClient.Verify(x=>x.SearchAsync(searchTerm,1),Times.Once);
        }

        [Test]
        public async Task Then_The_Results_Are_Filtered_For_Active_Standards()
        {
            //Act
            var actual = await _standardsService.GetBySearchTerm("test");

            //Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(1, actual.Count);
        }

        [Test]
        public async Task Then_The_Results_Are_Mapped_To_The_Results_Object()
        {
            //Act
            var actual = await _standardsService.GetBySearchTerm("test");

            //Assert
            Assert.IsAssignableFrom<List<StandardResultItem>>(actual);
        }
    }
}
