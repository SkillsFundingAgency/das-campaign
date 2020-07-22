using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Content;
using SFA.DAS.Campaign.Web.Controllers.EmployerInform;

namespace SFA.DAS.Campaign.UnitTests.Web.Controllers.EmployerInform.EndPointAssessments
{
    [TestFixture]
    public class WhenEndPointAssessementsIsRequested
    {
        [Test]
        public async Task ThenTheCorrectViewIsReturned()
        {
            var controller = new EndPointAssessmentsController(new Mock<IContentService>().Object);

            var result = await controller.Index();

            result.Should().BeOfType<ViewResult>();
            result.As<ViewResult>().ViewName.Should().Be("~/Views/EmployerInform/EndPointAssessments.cshtml");
        }
    }
}
