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
        public void AndLevyStatusIsSavedAsLevy_ThenViewModelLevyStatusIsLevy()
        {
            var sessionService = new Mock<ISessionService>();
            sessionService.Setup(ss => ss.Get<LevyOption>(sessionService.Object.LevyOptionKey)).Returns(new LevyOption
            {
                LevyStatus = LevyStatus.Levy
            });
            
            var controller = new FundingAnApprenticeshipController(sessionService.Object);

            var result = controller.Index();

            result.As<ViewResult>().Model.As<LevyOption>().LevyStatus.Should().Be(LevyStatus.Levy);
        }

        [Test]
        public void AndLevyStatusIsSavedAsNonLevy_ThenViewModelLevyStatusIsNonLevy()
        {
            var sessionService = new Mock<ISessionService>();
            sessionService.Setup(ss => ss.Get<LevyOption>(sessionService.Object.LevyOptionKey)).Returns(new LevyOption
            {
                LevyStatus = LevyStatus.NonLevy
            });

            var controller = new FundingAnApprenticeshipController(sessionService.Object);

            var result = controller.Index();

            result.As<ViewResult>().Model.As<LevyOption>().LevyStatus.Should().Be(LevyStatus.NonLevy);
        }

        [Test]
        public void AndNoVmStoredInSession_ThenDefaultVmReturnedInView()
        {
            var sessionService = new Mock<ISessionService>();
            sessionService.Setup(ss => ss.Get<LevyOption>(sessionService.Object.LevyOptionKey)).Returns(default(LevyOption));
            
            var controller = new FundingAnApprenticeshipController(sessionService.Object);

            var result = controller.Index();

            result.As<ViewResult>().Model.As<LevyOption>().OptionChosenByUser.Should().BeFalse();
        }

        [Test]
        public void AndNoVmStoredInSession_ThenLevyStatusShouldBeNonLevy()
        {
            var sessionService = new Mock<ISessionService>();
            sessionService.Setup(ss => ss.Get<LevyOption>(sessionService.Object.LevyOptionKey)).Returns(default(LevyOption));

            var controller = new FundingAnApprenticeshipController(sessionService.Object);

            var result = controller.Index();

            result.As<ViewResult>().Model.As<LevyOption>().LevyStatus.Should().Be(LevyStatus.NonLevy);
        }
    }
}