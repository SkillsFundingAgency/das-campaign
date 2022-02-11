using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Application.DataCollection;
using SFA.DAS.Campaign.Domain.Interfaces;
using SFA.DAS.Campaign.Infrastructure.Configuration;

namespace SFA.DAS.Campaign.Application.UnitTests.DataCollection.UserDataCollectionTests
{
    public class WhenCollectingUserData
    {
        private Mock<IUserDataCollectionValidator> _userDataCollectionValidator;
        private UserDataCollection _userDataCollection;
        private Mock<IQueueService<UserData>> _queueService;
        private Mock<IOptions<UserDataQueueNames>> _options;
        private Mock<IUserDataCryptographyService> _userDataCryptographyService;
        private UserData _userData;
        private const string StoreUserDataQueueName = "store-queue";
        private const string ExpectedEncodedEmail = "ABVF43AD";
        private const string ExpectedEmail = "test@local.com";

        [SetUp]
        public void Arrange()
        {
            _queueService = new Mock<IQueueService<UserData>>();
            _userDataCollectionValidator = new Mock<IUserDataCollectionValidator>();
            _userDataCollectionValidator.Setup(x => x.Validate(It.IsAny<UserData>())).Returns(new ValidationResult());
            _options = new Mock<IOptions<UserDataQueueNames>>();
            _options.Setup(x => x.Value).Returns(new UserDataQueueNames { StoreUserDataQueueName = StoreUserDataQueueName});
            _userDataCryptographyService = new Mock<IUserDataCryptographyService>();
            _userDataCryptographyService.Setup(x => x.GenerateEncodedUserEmail(It.Is<string>(c=>c.Equals(ExpectedEmail)))).Returns(ExpectedEncodedEmail);

            _userData = new UserData {Email = ExpectedEmail};

            _userDataCollection = new UserDataCollection(_userDataCollectionValidator.Object, _queueService.Object, _options.Object, _userDataCryptographyService.Object);
        }

        [Test]
        public async Task Then_The_Parameters_Are_Validated()
        {
            //Act
            await _userDataCollection.StoreUserData(_userData);

            //Arrange
            _userDataCollectionValidator.Verify(x=>x.Validate(_userData), Times.Once);
        }

        [Test]
        public void Then_If_The_Validation_Fails_Store_Is_Not_Called_And_An_Exception_Is_Thrown()
        {
            //Arrange
            var result = new ValidationResult();
            result.AddError("test", ValidationFailure.NotPopulated);
            _userDataCollectionValidator.Setup(x => x.Validate(It.IsAny<UserData>())).Returns(result);

            //Act/Assert
            Assert.ThrowsAsync<ValidationException>(async ()=>await _userDataCollection.StoreUserData(new UserData()));
            _queueService.Verify(x=>x.AddMessageToQueue( It.IsAny<UserData>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task Then_If_The_Validation_Is_Successful_Store_Is_Called()
        {
            //Arrange
            _userDataCollectionValidator.Setup(x => x.Validate(_userData)).Returns(new ValidationResult());

            //Act
            await _userDataCollection.StoreUserData(_userData);

            //Assert
            _queueService.Verify(x => x.AddMessageToQueue(_userData, StoreUserDataQueueName), Times.Once);
        }

        [Test]
        public async Task Then_The_Encoded_Email_Is_Added_To_The_User_Record()
        {
            //Arrange
            _userDataCollectionValidator.Setup(x => x.Validate(_userData)).Returns(new ValidationResult());

            //Act
            await _userDataCollection.StoreUserData(_userData);

            //Assert
            _userDataCryptographyService.Verify(x=>x.GenerateEncodedUserEmail(ExpectedEmail), Times.Once);
            _queueService.Verify(x => x.AddMessageToQueue(It.Is<UserData>(c=>c.EncodedEmail.Equals(ExpectedEncodedEmail)), StoreUserDataQueueName), Times.Once);
        }
    }
}
