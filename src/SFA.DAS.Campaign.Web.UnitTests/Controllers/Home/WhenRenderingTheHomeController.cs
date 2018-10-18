using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;
using SFA.DAS.Campaign.Web.Controllers;

namespace SFA.DAS.Campaign.Web.UnitTests.Controllers.Home
{
    public class WhenRenderingTheHomeController
    {
        private HomeController _homeController;
        private Mock<IStandardsService> _standardsService;

        [SetUp]
        public void Arrange()
        {
            _standardsService = new Mock<IStandardsService>();
            _homeController = new HomeController(_standardsService.Object);
        }

        [Test]
        public async Task Then_The_Correct_View_Is_Displayed()
        {
            //Act
            var actual = await _homeController.Index();

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
        }
    }
}
