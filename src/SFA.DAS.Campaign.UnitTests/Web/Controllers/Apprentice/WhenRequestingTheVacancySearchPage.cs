﻿using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Web.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using MediatR;
using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;
using SFA.DAS.Campaign.Web.Models;

namespace SFA.DAS.Campaign.UnitTests.Web.Controllers.Apprentice
{
    public class WhenRequestingTheVacancySearchPage
    {
        public ApprenticeController _sut;
        private Mock<IStandardsRepository> _standardRepository;
        private List<string> _routes;
        private Mock<IMediator> _mediator;

        [SetUp]
        public void Arrange()
        {
            var fixture = new Fixture();
            _routes = fixture.CreateMany<string>().ToList();
            _standardRepository = new Mock<IStandardsRepository>();
            _standardRepository.Setup(x => x.GetRoutes()).ReturnsAsync(_routes);
            _mediator = new Mock<IMediator>();
            _mediator.SetupMockMediator();

             _sut = new ApprenticeController(_standardRepository.Object, _mediator.Object);
        }

        [Test]
        public async Task Then_The_Vacancy_Search_Page_Is_Returned_With_Sectors()
        {
            var result = await _sut.FindAnApprenticeship();

            var viewResult = result as ViewResult;

            Assert.That(viewResult.ViewName, Is.Null);
            var actualModel = viewResult.Model as FindApprenticeshipSearchModel;
            Assert.That(actualModel, Is.Not.Null);
            actualModel.Routes.Should().BeEquivalentTo(_routes);
        }
    }
}
