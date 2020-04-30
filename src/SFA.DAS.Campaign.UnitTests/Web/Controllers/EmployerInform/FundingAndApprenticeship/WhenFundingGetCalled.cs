using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Web.Controllers.EmployerInform;
using SFA.DAS.Campaign.Web.Helpers;

namespace SFA.DAS.Campaign.UnitTests.Web.Controllers.EmployerInform.FundingAndApprenticeship
{
    [TestFixture]
    public class WhenFundingGetCalled
    {
        [Test]
        public void ThenViewIsReturned()
        {
            var sessionService = new Mock<ISessionService>();
            sessionService.Setup(ss => ss.Get<LevyOptionViewModel>("LevyOptionViewModel")).Returns(new LevyOptionViewModel
            {
                LevyStatus = LevyStatus.Levy
            });
            
            var controller = new FundingAnApprenticeshipController(sessionService.Object);

            var result = controller.Index();

            result.Should().BeOfType<ViewResult>();
            result.As<ViewResult>().Model.Should().BeOfType<LevyOptionViewModel>();
            result.As<ViewResult>().Model.As<LevyOptionViewModel>().LevyStatus.Should().Be(LevyStatus.Levy);
        }

        [Test]
        public void AndNoVmStoredInSession_ThenDefaultVmReturnedInView()
        {
            var sessionService = new Mock<ISessionService>();
            sessionService.Setup(ss => ss.Get<LevyOptionViewModel>("LevyOptionViewModel")).Returns(default(LevyOptionViewModel));
            
            var controller = new FundingAnApprenticeshipController(sessionService.Object);

            var result = controller.Index();

            result.As<ViewResult>().Model.As<LevyOptionViewModel>().LevyStatus.Should().Be(LevyStatus.NonLevy);
            result.As<ViewResult>().Model.As<LevyOptionViewModel>().PreviouslySet.Should().BeFalse();
        }
    }
}