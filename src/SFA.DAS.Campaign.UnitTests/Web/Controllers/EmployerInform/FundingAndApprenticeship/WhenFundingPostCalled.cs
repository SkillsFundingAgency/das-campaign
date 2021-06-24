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
        public void Then_Vm_Is_Stored_In_Session()
        {
            var sessionService = new Mock<ISessionService>();
            var controller = new FundingAnApprenticeshipController(sessionService.Object);

            var levyOptionViewModel = new LevyOptionViewModel() {LevyStatus = LevyStatus.Levy};
            controller.Index(levyOptionViewModel);
            
            sessionService.Verify(ss => ss.Set(sessionService.Object.LevyOptionViewModelKey, levyOptionViewModel), Times.Once());
        }

        
    }
}