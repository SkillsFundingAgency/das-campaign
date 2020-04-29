using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Web.Controllers.EmployerInform;
using SFA.DAS.Campaign.Web.Helpers;

namespace SFA.DAS.Campaign.UnitTests.Web.Controllers.EmployerInform
{
    [TestFixture]
    public class WhenHowTheyWorkGetCalled
    {
        [Test]
        public void ThenViewIsReturned()
        {
            var sessionService = new Mock<ISessionService>();
            sessionService.Setup(ss => ss.Get<LevyOptionViewModel>("LevyOptionViewModel")).Returns(new LevyOptionViewModel
            {
                GreaterThanThreeMillion = GreaterThanThreeMillion.Yes
            });
            
            var controller = new HowDoTheyWorkController(sessionService.Object);

            var result = controller.Index();

            result.Should().BeOfType<ViewResult>();
            result.As<ViewResult>().Model.Should().BeOfType<LevyOptionViewModel>();
            result.As<ViewResult>().Model.As<LevyOptionViewModel>().GreaterThanThreeMillion.Should().Be(GreaterThanThreeMillion.Yes);
        }

        [Test]
        public void AndNoVmStoredInSession_ThenDefaultVmReturnedInView()
        {
            var sessionService = new Mock<ISessionService>();
            sessionService.Setup(ss => ss.Get<LevyOptionViewModel>("LevyOptionViewModel")).Returns(default(LevyOptionViewModel));
            
            var controller = new HowDoTheyWorkController(sessionService.Object);

            var result = controller.Index();


            result.As<ViewResult>().Model.As<LevyOptionViewModel>().GreaterThanThreeMillion.Should().Be(GreaterThanThreeMillion.No);
        }
    }
}