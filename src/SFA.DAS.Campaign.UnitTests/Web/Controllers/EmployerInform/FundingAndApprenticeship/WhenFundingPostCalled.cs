using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Web.Controllers.EmployerInform;
using SFA.DAS.Campaign.Web.Helpers;

namespace SFA.DAS.Campaign.UnitTests.Web.Controllers.EmployerInform.FundingAndApprenticeship
{
    [TestFixture]
    public class WhenFundingPostCalled
    {
        [Test]
        public void ThenVmIsStoredInSession()
        {
            var sessionService = new Mock<ISessionService>();
            var controller = new FundingAnApprenticeshipController(sessionService.Object);

            var levyOptionViewModel = new LevyOptionViewModel() {LevyStatus = LevyStatus.Levy};
            controller.Index(levyOptionViewModel);
            
            sessionService.Verify(ss => ss.Set(sessionService.Object.LevyOptionViewModelKey, levyOptionViewModel), Times.Once());
        }

        [Test]
        public void ThenRedirectToActionIsReturned()
        {
            var sessionService = new Mock<ISessionService>();
            var controller = new FundingAnApprenticeshipController(sessionService.Object);

            var levyOptionViewModel = new LevyOptionViewModel() {LevyStatus = LevyStatus.Levy};
            var result = controller.Index(levyOptionViewModel);
            
            result.Should().BeOfType<RedirectToActionResult>();
            result.As<RedirectToActionResult>().ControllerName.Should().Be("FundingAnApprenticeshipLevy");
            result.As<RedirectToActionResult>().ActionName.Should().Be("Index");
        }
    }
}