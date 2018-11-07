using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Application.DataCollection.Services;
using SFA.DAS.Campaign.Domain.DataCollection;
using SFA.DAS.Campaign.Models.Configuration;
using SFA.DAS.Campaign.Models.DataCollection;

namespace SFA.DAS.Campaign.Application.UnitTests.DataCollection.UserDataCollectionTests
{
    public class WhenCollectingUserData
    {
        private Mock<IUserDataCollectionValidator> _userDataCollectionValidator;
        private UserDataCollection _userDataCollection;
        private Mock<IQueueService<UserData>> _queueService;
        private Mock<IOptions<CampaignConfiguration>> _options;
        private const string StoreUserDataQueueName = "store-queue";

        [SetUp]
        public void Arrange()
        {
            _queueService = new Mock<IQueueService<UserData>>();
            _userDataCollectionValidator = new Mock<IUserDataCollectionValidator>();
            _userDataCollectionValidator.Setup(x => x.Validate(It.IsAny<UserData>())).Returns(true);
            _options = new Mock<IOptions<CampaignConfiguration>>();
            _options.Setup(x => x.Value).Returns(new CampaignConfiguration{StoreUserDataQueueName = StoreUserDataQueueName});
            _userDataCollection = new UserDataCollection(_userDataCollectionValidator.Object, _queueService.Object, _options.Object);
        }

        [Test]
        public async Task Then_The_Parameters_Are_Validated()
        {
            //Arrange
            var userData = new UserData();

            //Act
            await _userDataCollection.StoreUserData(userData);

            //Arrange
            _userDataCollectionValidator.Verify(x=>x.Validate(userData), Times.Once);
        }

        [Test]
        public void Then_If_The_Validation_Fails_Store_Is_Not_Called_And_An_Exception_Is_Thrown()
        {
            //Arrange
            _userDataCollectionValidator.Setup(x => x.Validate(It.IsAny<UserData>())).Returns(false);

            //Act/Assert
            Assert.ThrowsAsync<ArgumentException>(async ()=>await _userDataCollection.StoreUserData(new UserData()));
            _queueService.Verify(x=>x.AddMessageToQueue( It.IsAny<UserData>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task Then_If_The_Validation_Is_Successful_Store_Is_Called()
        {
            //Arrange
            var userData = new UserData();
            _userDataCollectionValidator.Setup(x => x.Validate(userData)).Returns(true);

            //Act
            await _userDataCollection.StoreUserData(userData);

            //Assert
            _queueService.Verify(x => x.AddMessageToQueue(userData, StoreUserDataQueueName), Times.Once);
        }
    }
}
