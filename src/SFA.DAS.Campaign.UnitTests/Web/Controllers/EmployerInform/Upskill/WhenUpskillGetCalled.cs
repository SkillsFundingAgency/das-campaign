using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Web.Controllers.EmployerInform;
using SFA.DAS.Campaign.Web.Helpers;

namespace SFA.DAS.Campaign.UnitTests.Web.Controllers.EmployerInform.Upskill
{
    [TestFixture]
    public class WhenUpskillGetCalled
    {
        [Test]
        public void ThenViewIsReturned()
        {
            var controller = new UpskillController();

            var result = controller.Index();

            result.Should().BeOfType<ViewResult>();
            result.As<ViewResult>().ViewName.Should().Be("~/Views/EmployerInform/Upskill.cshtml");
        }
    }
}