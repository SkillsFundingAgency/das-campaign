using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Infrastructure.HealthChecks;
using SFA.DAS.Campaign.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.Campaign.UnitTests.Web.Controllers.Apprentice
{
    public class WhenRequestingTheVacancySearchPage
    {
        public ApprenticeController sut;

        private Mock<IVacancyServiceApiHealthCheck> _vacancyServiceHealthCheck;

        [SetUp]
        public void Arrange()
        {
            _vacancyServiceHealthCheck = new Mock<IVacancyServiceApiHealthCheck>();

            sut = new ApprenticeController(_vacancyServiceHealthCheck.Object);
        }

        [Test]
        public async Task AndTheHealthCheckReturnsHealthy_ThenTheVacancySearchPageIsReturned()
        {
            _vacancyServiceHealthCheck.Setup(v => v.CheckHealthAsync(It.IsAny<HealthCheckContext>(), CancellationToken.None)).ReturnsAsync(new HealthCheckResult(HealthStatus.Healthy));
            
            var result = await sut.FindAnApprenticeship();

            var viewResult = result as ViewResult;

            Assert.AreEqual(null, viewResult.ViewName);
        }

        [Test]
        public async Task AndTheHealthCheckReturnsUnhealthy_ThenTheRedirectToFaaPageIsReturned()
        {
            _vacancyServiceHealthCheck.Setup(v => v.CheckHealthAsync(It.IsAny<HealthCheckContext>(), CancellationToken.None)).ReturnsAsync(new HealthCheckResult(HealthStatus.Unhealthy));

            var result = await sut.FindAnApprenticeship();

            var viewResult = result as ViewResult;

            Assert.AreEqual("~/Views/Apprentice/RedirectToFAA.cshtml", viewResult.ViewName);
        }
    }
}
