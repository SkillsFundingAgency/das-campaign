using NUnit.Framework;
using SFA.DAS.Apprenticeships.Api.Types;
using SFA.DAS.Campaign.Application.ApprenticeshipCourses.Services;

namespace SFA.DAS.Campaign.Application.UnitTests.ApprenticeshipCourses.StandardsTests
{
    public class WhenMappingSearchItemsToStandards
    {
        private StandardsMapper _mapper;

        [SetUp]
        public void Arrange()
        {
            _mapper = new StandardsMapper();
        }

        [Test]
        public void Then_The_Values_Are_Correctly_Mapped()
        {
            //Arrange
            var toMap = new ApprenticeshipSearchResultsItem
            {
                Title = "Test",
                Level = 1,
                Duration = 10
            };

            //Act
            var actual = _mapper.Map(toMap);

            //Assert
            Assert.AreEqual(toMap.Title, actual.Title);
            Assert.AreEqual(toMap.Duration, actual.Duration);
            Assert.AreEqual(toMap.Level, actual.Level);
        }
    }
}
