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
            sessionService.Setup(ss => ss.Get<LevyOption>(sessionService.Object.LevyOptionKey)).Returns(new LevyOption
            {
                LevyStatus = LevyStatus.Levy,
                OptionChosenByUser = true
            });
            
            var controller = new HowDoTheyWorkController(sessionService.Object);

            var result = controller.Index();

            result.As<ViewResult>().Model.As<LevyOption>().LevyStatus.Should().Be(LevyStatus.Levy);
            result.As<ViewResult>().Model.As<LevyOption>().OptionChosenByUser.Should().BeTrue();
        }

        [Test]
        public void AndNoVmStoredInSession_ThenDefaultVmIsReturnedInView()
        {
            var sessionService = new Mock<ISessionService>();
            sessionService.Setup(ss => ss.Get<LevyOption>(sessionService.Object.LevyOptionKey)).Returns(default(LevyOption));
            
            var controller = new HowDoTheyWorkController(sessionService.Object);

            var result = controller.Index();

            result.As<ViewResult>().Model.As<LevyOption>().LevyStatus.Should().Be(LevyStatus.NonLevy);
            result.As<ViewResult>().Model.As<LevyOption>().OptionChosenByUser.Should().BeFalse();
        }
    }
}