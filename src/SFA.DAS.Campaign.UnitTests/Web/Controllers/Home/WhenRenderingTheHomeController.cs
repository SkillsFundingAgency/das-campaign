using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using SFA.DAS.Campaign.Web.Controllers;

namespace SFA.DAS.Campaign.Web.UnitTests.Controllers.Home
{
    public class WhenRenderingTheHomeController
    {
        private HomeController _homeController;

        [SetUp]
        public void Arrange()
        {
            _homeController = new HomeController();
        }

        [Test]
        public void Then_The_Correct_View_Is_Displayed()
        {
            //Act
            var actual = _homeController.Index();

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
        }
    }
}
