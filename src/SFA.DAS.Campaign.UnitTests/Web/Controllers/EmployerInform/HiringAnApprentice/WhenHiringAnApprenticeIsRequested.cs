using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using SFA.DAS.Campaign.Web.Controllers.EmployerInform;

namespace SFA.DAS.Campaign.UnitTests.Web.Controllers.EmployerInform.HiringAnApprentice
{
    [TestFixture]
    public class WhenHiringAnApprenticeIsRequested
    {
        [Test]
        public void ThenTheCorrectViewIsReturned()
        {
            var controller = new HiringAnApprenticeController();

            var result = controller.Index();

            result.Should().BeOfType<ViewResult>();
            result.As<ViewResult>().ViewName.Should().Be("~/Views/EmployerInform/HiringAnApprentice.cshtml");
        }
    }
}
