using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Domain.DataCollection;
using SFA.DAS.Campaign.Web.Controllers;

namespace SFA.DAS.Campaign.Web.UnitTests.Controllers.Unregister
{
    public class WhenUnregisterUserInterest
    {
        private UnregisterInterestController _unregisterInterestController;
        private Mock<IUserDataCollection> _userDataCollectionService;
        private Mock<IUserDataCryptographyService> _userDataCryptographyService;

        private const string ExpectedEncodedEmail = "EDCF457";
        private const string ExpectedEmail = "test@local.com";

        [SetUp]
        public void Arrange()
        {
            _userDataCollectionService = new Mock<IUserDataCollection>();
            _userDataCryptographyService = new Mock<IUserDataCryptographyService>();

            _userDataCryptographyService.Setup(x => x.DecodeUserEmail(ExpectedEncodedEmail)).Returns(ExpectedEmail);
            _unregisterInterestController = new UnregisterInterestController(_userDataCollectionService.Object, _userDataCryptographyService.Object);
        }

        [Test]
        public async Task Then_The_Email_Is_Decrypted_From_The_Url_And_Passed_To_The_DataCollection_Service()
        {
            //Act
            await _unregisterInterestController.Submit(ExpectedEncodedEmail);

            //Assert
            _userDataCollectionService.Verify(x=>x.RemoveUserData(ExpectedEmail), Times.Once);
        }

        [Test]
        public async Task Then_The_Unregister_Interest_Completion_View_Is_Displayed()
        {
            //Act
            await _unregisterInterestController.Submit("F34FG");

            //Assert
            _userDataCollectionService.Verify(x => x.RemoveUserData(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task Then_The_Unregister_Interest_Completion_View_Is_Displayed_If_The_Email_Is_Not_Valid()
        {
            //Act
            var actual = await _unregisterInterestController.Submit("F34FG");

            //Assert
            Assert.IsNotNull(actual);
            var viewResult = actual as RedirectToActionResult;
            Assert.IsNotNull(viewResult);
            Assert.AreEqual("ThankYou", viewResult.ActionName);
        }
        
    }
}
