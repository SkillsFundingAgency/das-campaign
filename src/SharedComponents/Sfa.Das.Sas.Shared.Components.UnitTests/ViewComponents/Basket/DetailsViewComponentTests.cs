using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using NUnit.Framework;
using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.ApplicationServices.Queries;
using Sfa.Das.Sas.Shared.Components.Cookies;
using Sfa.Das.Sas.Shared.Components.Orchestrators;
using Sfa.Das.Sas.Shared.Components.UnitTests.ViewComponents.Fat;
using Sfa.Das.Sas.Shared.Components.ViewComponents.Basket;
using Sfa.Das.Sas.Shared.Components.ViewModels.Apprenticeship;
using Sfa.Das.Sas.Shared.Components.ViewModels.Basket;

namespace Sfa.Das.Sas.Shared.Components.UnitTests.ViewComponents.Basket
{
    [TestFixture]
    public class BasketDetailsViewComponentTests : ViewComponentTestsBase
    {
        private Mock<IBasketOrchestrator> _mockBasketOrchestrator;
        private BasketDetailsViewComponent _sut;

        private Guid _basketId = Guid.NewGuid();

        [SetUp]
        public new void Setup()
        {
            base.Setup();
            _mockBasketOrchestrator = new Mock<IBasketOrchestrator>();

            _mockBasketOrchestrator.Setup(s => s.GetBasket()).ReturnsAsync(new BasketViewModel<ApprenticeshipBasketItemViewModel>() {BasketId = _basketId});

            _sut = new BasketDetailsViewComponent(_mockBasketOrchestrator.Object)
            {
                ViewComponentContext = _viewComponentContext
            };
        }

        [Test]
        public async Task Invoke_ReturnsModelContainingBasketId()
        {
            var result = await _sut.InvokeAsync() as ViewViewComponentResult;

            result.ViewData.Model.Should().BeAssignableTo<BasketViewModel<ApprenticeshipBasketItemViewModel>>();

            var model = result.ViewData.Model as BasketViewModel<ApprenticeshipBasketItemViewModel>;

            model.BasketId.Should().NotBeNull();

        }


    }
}
