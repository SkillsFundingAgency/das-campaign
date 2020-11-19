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

        [SetUp]
        public void Arrange()
        {
            sut = new ApprenticeController();
        }

        [Test]
        public void ThenTheVacancySearchPageIsReturned()
        {
            var result = sut.FindAnApprenticeship();

            var viewResult = result as ViewResult;

            Assert.AreEqual(null, viewResult.ViewName);
        }
    }
}
