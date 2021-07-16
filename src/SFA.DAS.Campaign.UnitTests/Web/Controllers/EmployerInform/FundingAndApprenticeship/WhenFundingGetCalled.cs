using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
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
        private Mock<ISessionService> _sessionService;
        private Mock<IMediator> _mediator;

        [SetUp]
        public void Setup()
        {
            _sessionService = new Mock<ISessionService>();
            _mediator = new Mock<IMediator>();

            _mediator.SetupMockMediator();
        }

        [Test]
        public async Task And_Levy_Status_Is_Saved_As_Levy_Then_View_Model_Levy_Status_Is_Levy()
        {
            _sessionService.Setup(ss => ss.Get<LevyOptionViewModel>(_sessionService.Object.LevyOptionViewModelKey)).Returns(new LevyOptionViewModel
            {
                LevyStatus = LevyStatus.Levy
            });
            
            var controller = InvokeFundingAnApprenticeshipController(_sessionService, _mediator);

            var result = await controller.Index();

            result.As<ViewResult>().Model.As<LevyOptionViewModel>().LevyStatus.Should().Be(LevyStatus.Levy);
        }

        private static FundingAnApprenticeshipController InvokeFundingAnApprenticeshipController(Mock<ISessionService> sessionService, Mock<IMediator> mediator)
        {
            var controller = new FundingAnApprenticeshipController(sessionService.Object, mediator.Object);
            return controller;
        }

        [Test]
        public async Task And_Levy_Status_Is_Saved_As_Non_Levy_Then_View_Model_Levy_Status_Is_Non_Levy()
        {
            _sessionService.Setup(ss => ss.Get<LevyOptionViewModel>(_sessionService.Object.LevyOptionViewModelKey)).Returns(new LevyOptionViewModel
            {
                LevyStatus = LevyStatus.NonLevy
            });

            var controller = InvokeFundingAnApprenticeshipController(_sessionService, _mediator);

            var result = await controller.Index();

            result.As<ViewResult>().Model.As<LevyOptionViewModel>().LevyStatus.Should().Be(LevyStatus.NonLevy);
        }

        [Test]
        public async Task And_No_Vm_Stored_In_Session_Then_Default_Vm_Returned_In_View()
        {
            _sessionService.Setup(ss => ss.Get<LevyOptionViewModel>(_sessionService.Object.LevyOptionViewModelKey)).Returns(default(LevyOptionViewModel));

            var controller = InvokeFundingAnApprenticeshipController(_sessionService, _mediator);

            var result = await controller.Index();

            result.As<ViewResult>().Model.As<LevyOptionViewModel>().OptionChosenByUser.Should().BeFalse();
        }

        [Test]
        public async Task And_No_Vm_Stored_In_Session_Then_Levy_Status_Should_Be_Non_Levy()
        {
            _sessionService.Setup(ss => ss.Get<LevyOptionViewModel>(_sessionService.Object.LevyOptionViewModelKey)).Returns(default(LevyOptionViewModel));
            var controller = InvokeFundingAnApprenticeshipController(_sessionService, _mediator);
            var result = await controller.Index();

            result.As<ViewResult>().Model.As<LevyOptionViewModel>().LevyStatus.Should().BeNull();
        }
    }
}