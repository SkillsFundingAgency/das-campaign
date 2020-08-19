using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using SFA.DAS.Campaign.Web.Controllers;

namespace SFA.DAS.Campaign.Web.UnitTests.Controllers.Home
{
    public class WhenRenderingTheHomeController
    {
        private OldHomeController _oldHomeController;

        [SetUp]
        public void Arrange()
        {
            _oldHomeController = new OldHomeController();
        }

        [Test]
        public void Then_The_Correct_View_Is_Displayed()
        {
            //Act
            var actual = _oldHomeController.Index();

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
        }
    }
}
