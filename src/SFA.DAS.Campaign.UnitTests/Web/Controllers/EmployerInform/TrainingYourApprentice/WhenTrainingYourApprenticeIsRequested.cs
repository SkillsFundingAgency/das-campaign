using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using SFA.DAS.Campaign.Web.Controllers.EmployerInform;

namespace SFA.DAS.Campaign.UnitTests.Web.Controllers.EmployerInform.TrainingYourApprentice
{
    [TestFixture]
    public class WhenTrainingYourApprenticeIsRequested
    {
        [Test]
        public void ThenTheCorrectViewIsReturned()
        {
            var controller = new TrainingYourApprenticeController();

            var result = controller.Index();

            result.Should().BeOfType<ViewResult>();
            result.As<ViewResult>().ViewName.Should().Be("~/Views/EmployerInform/TrainingYourApprentice.cshtml");
        }
    }
}
