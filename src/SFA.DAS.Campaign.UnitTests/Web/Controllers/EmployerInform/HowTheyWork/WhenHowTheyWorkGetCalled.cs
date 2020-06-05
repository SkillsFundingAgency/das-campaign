using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Web.Controllers.EmployerInform;
using SFA.DAS.Campaign.Web.Helpers;

namespace SFA.DAS.Campaign.UnitTests.Web.Controllers.EmployerInform.HowTheyWork
{
    [TestFixture]
    public class WhenHowTheyWorkGetCalled
    {
        [Test]
        public void AndVmStoredInSession_ThenVmIsReturnedFromSessionInView()
        {
            var sessionService = new Mock<ISessionService>();
            sessionService.Setup(ss => ss.Get<LevyOptionViewModel>(sessionService.Object.LevyOptionViewModelKey)).Returns(new LevyOptionViewModel
            {
                LevyStatus = LevyStatus.Levy,
                PreviouslySet = true
            });
            
            var controller = new HowDoTheyWorkController(sessionService.Object);

            var result = controller.Index();

            result.As<ViewResult>().Model.As<LevyOptionViewModel>().LevyStatus.Should().Be(LevyStatus.Levy);
            result.As<ViewResult>().Model.As<LevyOptionViewModel>().PreviouslySet.Should().BeTrue();
        }

        [Test]
        public void AndNoVmStoredInSession_ThenDefaultVmIsReturnedInView()
        {
            var sessionService = new Mock<ISessionService>();
            sessionService.Setup(ss => ss.Get<LevyOptionViewModel>(sessionService.Object.LevyOptionViewModelKey)).Returns(default(LevyOptionViewModel));
            
            var controller = new HowDoTheyWorkController(sessionService.Object);

            var result = controller.Index();

            result.As<ViewResult>().Model.As<LevyOptionViewModel>().LevyStatus.Should().Be(LevyStatus.NonLevy);
            result.As<ViewResult>().Model.As<LevyOptionViewModel>().PreviouslySet.Should().BeFalse();
        }
    }
}