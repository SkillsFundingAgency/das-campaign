using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Sfa.Das.Sas.ApplicationServices.Commands;
using Sfa.Das.Sas.Core.Configuration;
using Sfa.Das.Sas.Shared.Components.Controllers;
using Sfa.Das.Sas.Shared.Components.Cookies;
using Sfa.Das.Sas.Shared.Components.ViewModels.Basket;
using System;
using System.Threading;
using System.Threading.Tasks;
using Sfa.Das.Sas.ApplicationServices.Queries;

namespace Sfa.Das.Sas.Shared.Components.UnitTests.Controller
{
    [TestFixture]
    public class TrainingProviderControllerTests
    {
        private const string POSTCODE = "PP1 2PP";
        private Mock<IMediator> _mockMediator;
        private TrainingProviderController _sut;

        private bool validationResult = true;

        [SetUp]
        public void Setup()
        {
            _mockMediator = new Mock<IMediator>();
            _mockMediator.Setup(s => s.Send(It.IsAny<ValidatePostcodeQuery>(),It.IsAny<CancellationToken>())).ReturnsAsync(validationResult);
            
            _sut = new TrainingProviderController(_mockMediator.Object);
        }

        [Test]
        public async Task ValidatePostcode_ShouldReturnJson_WithValueFromValidationResult()
        {
            var result = await _sut.ValidatePostcode(POSTCODE);

            result.Should().BeAssignableTo<JsonResult>();
            var json = (JsonResult)result;

            json.Value.Should().Be(validationResult);
        }
    }
}
