using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Infrastructure.HealthChecks;
using SFA.DAS.Campaign.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;
using SFA.DAS.Campaign.Web.Models;

namespace SFA.DAS.Campaign.UnitTests.Web.Controllers.Apprentice
{
    public class WhenRequestingTheVacancySearchPage
    {
        public ApprenticeController sut;
        private Mock<IStandardsRepository> _standardRepository;
        private List<string> _routes;

        [SetUp]
        public void Arrange()
        {
            var fixture = new Fixture();
            _routes = fixture.CreateMany<string>().ToList();
            _standardRepository = new Mock<IStandardsRepository>();
            _standardRepository.Setup(x => x.GetRoutes()).ReturnsAsync(_routes);
            sut = new ApprenticeController(_standardRepository.Object);
        }

        [Test]
        public async Task ThenTheVacancySearchPageIsReturned_WithSectors()
        {
            var result = await sut.FindAnApprenticeship();

            var viewResult = result as ViewResult;

            Assert.AreEqual(null, viewResult.ViewName);
            var actualModel = viewResult.Model as FindApprenticeshipSearchModel;
            Assert.IsNotNull(actualModel);
            actualModel.Routes.Should().BeEquivalentTo(_routes);
        }
    }
}
