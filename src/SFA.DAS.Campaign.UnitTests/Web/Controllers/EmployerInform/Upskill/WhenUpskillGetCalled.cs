using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Content;
using SFA.DAS.Campaign.Web.Controllers.EmployerInform;

namespace SFA.DAS.Campaign.UnitTests.Web.Controllers.EmployerInform.Upskill
{
    [TestFixture]
    public class WhenUpskillGetCalled
    {
        [Test]
        public async Task ThenViewIsReturned()
        {
            var controller = new UpskillController(new Mock<IContentService>().Object);

            var result = await controller.Index();

            result.Should().BeOfType<ViewResult>();
            result.As<ViewResult>().ViewName.Should().Be("~/Views/EmployerInform/Upskill.cshtml");
        }
    }
}