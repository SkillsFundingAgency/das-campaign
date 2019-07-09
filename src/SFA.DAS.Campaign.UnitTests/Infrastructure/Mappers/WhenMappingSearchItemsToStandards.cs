using Ifa.Api.Model;
using NUnit.Framework;
using SFA.DAS.Apprenticeships.Api.Types;
using SFA.DAS.Campaign.Infrastructure.Mappers;

namespace SFA.DAS.Campaign.Infrastructure.UnitTests.Mappers
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
        public void And_ApprenticeshipSearchResultsItem_Then_The_Values_Are_Correctly_Mapped()
        {
            //Arrange
            var toMap = new ApprenticeshipSearchResultsItem
            {
                StandardId = "123",
                Title = "Test",
                Level = 1,
                Duration = 10
            };

            //Act
            var actual = _mapper.Map(toMap);

            //Assert
            Assert.AreEqual(int.Parse(toMap.StandardId),actual.Id);
            Assert.AreEqual(toMap.Title, actual.Title);
            Assert.AreEqual(toMap.Duration, actual.Duration);
            Assert.AreEqual(toMap.Level, actual.Level);
        }

        [Test]
        public void And_TempApprenticeshipStandard_Then_The_Values_Are_Correctly_Mapped()
        {
            //Arrange
            var toMap = new ApiApprenticeshipStandard()
            {
                LarsCode = 123,
                Title= "Test",
                Level = 1,
                TypicalDuration = 10
            };

            //Act
            var actual = _mapper.Map(toMap);

            //Assert
            Assert.AreEqual(toMap.LarsCode, actual.Id);
            Assert.AreEqual(toMap.Title, actual.Title);
            Assert.AreEqual(toMap.TypicalDuration, actual.Duration);
            Assert.AreEqual(toMap.Level, actual.Level);
        }
    }
}
