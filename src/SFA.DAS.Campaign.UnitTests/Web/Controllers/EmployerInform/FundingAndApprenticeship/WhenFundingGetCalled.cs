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
        public void And_Levy_Status_Is_Saved_As_Levy_Then_View_Model_Levy_Status_Is_Levy()
        {
            var sessionService = new Mock<ISessionService>();
            sessionService.Setup(ss => ss.Get<LevyOptionViewModel>(sessionService.Object.LevyOptionViewModelKey)).Returns(new LevyOptionViewModel
            {
                LevyStatus = LevyStatus.Levy
            });
            
            var controller = new FundingAnApprenticeshipController(sessionService.Object);

            var result = controller.Index();

            result.As<ViewResult>().Model.As<LevyOptionViewModel>().LevyStatus.Should().Be(LevyStatus.Levy);
        }

        [Test]
        public void And_Levy_Status_Is_Saved_As_Non_Levy_Then_View_Model_Levy_Status_Is_Non_Levy()
        {
            var sessionService = new Mock<ISessionService>();
            sessionService.Setup(ss => ss.Get<LevyOptionViewModel>(sessionService.Object.LevyOptionViewModelKey)).Returns(new LevyOptionViewModel
            {
                LevyStatus = LevyStatus.NonLevy
            });

            var controller = new FundingAnApprenticeshipController(sessionService.Object);

            var result = controller.Index();

            result.As<ViewResult>().Model.As<LevyOptionViewModel>().LevyStatus.Should().Be(LevyStatus.NonLevy);
        }

        [Test]
        public void And_No_Vm_Stored_In_Session_Then_Default_Vm_Returned_In_View()
        {
            var sessionService = new Mock<ISessionService>();
            sessionService.Setup(ss => ss.Get<LevyOptionViewModel>(sessionService.Object.LevyOptionViewModelKey)).Returns(default(LevyOptionViewModel));
            
            var controller = new FundingAnApprenticeshipController(sessionService.Object);

            var result = controller.Index();

            result.As<ViewResult>().Model.As<LevyOptionViewModel>().OptionChosenByUser.Should().BeFalse();
        }

        [Test]
        public void And_No_Vm_Stored_In_Session_Then_Levy_Status_Should_Be_Non_Levy()
        {
            var sessionService = new Mock<ISessionService>();
            sessionService.Setup(ss => ss.Get<LevyOptionViewModel>(sessionService.Object.LevyOptionViewModelKey)).Returns(default(LevyOptionViewModel));

            var controller = new FundingAnApprenticeshipController(sessionService.Object);

            var result = controller.Index();

            result.As<ViewResult>().Model.As<LevyOptionViewModel>().LevyStatus.Should().BeNull();
        }
    }
}