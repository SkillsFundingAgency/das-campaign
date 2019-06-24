using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Application.Configuration;
using SFA.DAS.Campaign.Application.DataCollection;
using SFA.DAS.Campaign.Application.Core;

namespace SFA.DAS.Campaign.Application.UnitTests.DataCollection.UserDataCollectionTests
{
    public class WhenRemovingUserData
    {
        private Mock<IUserDataCollectionValidator> _userDataCollectionValidator;
        private UserDataCollection _userDataCollection;
        private Mock<IQueueService<UserData>> _queueService;
        private Mock<IOptions<UserDataQueueNames>> _options;
        private Mock<IUserDataCryptographyService> _userDataCryptographyService;
        private const string RemoveUserDataQueueName = "remove-queue";

        [SetUp]
        public void Arrange()
        {
            _queueService = new Mock<IQueueService<UserData>>();
            _userDataCollectionValidator = new Mock<IUserDataCollectionValidator>();
            _userDataCollectionValidator.Setup(x => x.ValidateEmail(It.IsAny<string>())).Returns(true);
            _options = new Mock<IOptions<UserDataQueueNames>>();
            _options.Setup(x => x.Value).Returns(new UserDataQueueNames { RemoveUserDataQueueName = RemoveUserDataQueueName });
            _userDataCryptographyService = new Mock<IUserDataCryptographyService>();

            _userDataCollection = new UserDataCollection(_userDataCollectionValidator.Object, _queueService.Object, _options.Object, _userDataCryptographyService.Object);
        }

        [Test]
        public void Then_The_Message_Is_Not_Processed_If_No_Email_Is_Passed()
        {
            //Act/Assert
            Assert.ThrowsAsync<ArgumentException>(async ()=> await _userDataCollection.RemoveUserData("", true));
            _queueService.Verify(x=>x.AddMessageToQueue(It.IsAny<UserData>(),It.IsAny<string>()),Times.Never);
        }

        [Test]
        public void Then_The_Message_Is_Not_Process_If_The_Email_Is_Not_Valid()
        {
            //Arrange
            _userDataCollectionValidator.Setup(x => x.ValidateEmail(It.IsAny<string>())).Returns(false);

            //Act/Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _userDataCollection.RemoveUserData("Test", true));
            _queueService.Verify(x => x.AddMessageToQueue(It.IsAny<UserData>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task Then_The_Message_Is_Added_To_The_Queue_If_Valid()
        {
            //Arrange
            var expectedEmail = "test@test.local";

            //Act
            await _userDataCollection.RemoveUserData(expectedEmail, true);

            //Assert
            _queueService.Verify(x => x.AddMessageToQueue(It.Is<UserData>(c=>c.Email.Equals(expectedEmail) && c.Consent.Equals(true)), RemoveUserDataQueueName), Times.Once);
        }
    }
}
