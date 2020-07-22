using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Application.Services;
using SFA.DAS.Campaign.Content;
using SFA.DAS.Campaign.Web.Controllers.EmployerInform;

namespace SFA.DAS.Campaign.UnitTests.Web.Controllers.EmployerInform.FundingAndApprenticeship
{
    [TestFixture]
    public class WhenFundingGetCalled
    {
        [Test]
        public async Task AndLevyStatusIsSavedAsLevy_ThenViewModelLevyStatusIsLevy()
        {
            var sessionService = new Mock<ISessionService>();
            sessionService.Setup(ss => ss.Get<LevyOptionViewModel>(sessionService.Object.LevyOptionViewModelKey)).Returns(new LevyOptionViewModel
            {
                LevyStatus = LevyStatus.Levy
            });
            
            var controller = new FundingAnApprenticeshipController(sessionService.Object, new Mock<IContentService>().Object);

            var result = await controller.Index();

            result.As<ViewResult>().Model.As<LevyQuestionDrivenViewModel>().LevyOptionViewModel.LevyStatus.Should().Be(LevyStatus.Levy);
        }

        [Test]
        public async Task AndLevyStatusIsSavedAsNonLevy_ThenViewModelLevyStatusIsNonLevy()
        {
            var sessionService = new Mock<ISessionService>();
            sessionService.Setup(ss => ss.Get<LevyOptionViewModel>(sessionService.Object.LevyOptionViewModelKey)).Returns(new LevyOptionViewModel
            {
                LevyStatus = LevyStatus.NonLevy
            });

            var controller = new FundingAnApprenticeshipController(sessionService.Object, new Mock<IContentService>().Object);

            var result = await controller.Index();

            result.As<ViewResult>().Model.As<LevyQuestionDrivenViewModel>().LevyOptionViewModel.LevyStatus.Should().Be(LevyStatus.NonLevy);
        }

        [Test]
        public async Task AndNoVmStoredInSession_ThenDefaultVmReturnedInView()
        {
            var sessionService = new Mock<ISessionService>();
            sessionService.Setup(ss => ss.Get<LevyOptionViewModel>(sessionService.Object.LevyOptionViewModelKey)).Returns(default(LevyOptionViewModel));
            
            var controller = new FundingAnApprenticeshipController(sessionService.Object, new Mock<IContentService>().Object);

            var result = await controller.Index();

            result.As<ViewResult>().Model.As<LevyQuestionDrivenViewModel>().LevyOptionViewModel.OptionChosenByUser.Should().BeFalse();
        }

        [Test]
        public async Task AndNoVmStoredInSession_ThenLevyStatusShouldBeNonLevy()
        {
            var sessionService = new Mock<ISessionService>();
            sessionService.Setup(ss => ss.Get<LevyOptionViewModel>(sessionService.Object.LevyOptionViewModelKey)).Returns(default(LevyOptionViewModel));

            var controller = new FundingAnApprenticeshipController(sessionService.Object, new Mock<IContentService>().Object);

            var result = await controller.Index();

            result.As<ViewResult>().Model.As<LevyQuestionDrivenViewModel>().LevyOptionViewModel.LevyStatus.Should().Be(LevyStatus.NonLevy);
        }
    }
}