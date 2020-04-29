using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Web.Controllers.EmployerInform;
using SFA.DAS.Campaign.Web.Helpers;

namespace SFA.DAS.Campaign.UnitTests.Web.Controllers.EmployerInform
{
    [TestFixture]
    public class WhenHowTheyWorkPostCalled
    {
        [Test]
        public void ThenVmIsStoredInSession()
        {
            var sessionService = new Mock<ISessionService>();
            var controller = new HowDoTheyWorkController(sessionService.Object);

            var levyOptionViewModel = new LevyOptionViewModel() {GreaterThanThreeMillion = GreaterThanThreeMillion.Yes};
            controller.Index(levyOptionViewModel);
            
            sessionService.Verify(ss => ss.Set("LevyOptionViewModel", levyOptionViewModel));
        }

        [Test]
        public void ThenRedirectToActionIsReturned()
        {
            var sessionService = new Mock<ISessionService>();
            var controller = new HowDoTheyWorkController(sessionService.Object);

            var levyOptionViewModel = new LevyOptionViewModel() {GreaterThanThreeMillion = GreaterThanThreeMillion.Yes};
            var result = controller.Index(levyOptionViewModel);
            
            result.Should().BeOfType<RedirectToActionResult>();
            result.As<RedirectToActionResult>().ControllerName.Should().Be("HiringAnApprentice");
            result.As<RedirectToActionResult>().ActionName.Should().Be("Index");
        }
    }
}