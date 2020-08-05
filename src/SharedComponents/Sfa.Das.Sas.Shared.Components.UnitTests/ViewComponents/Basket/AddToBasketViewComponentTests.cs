using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using NUnit.Framework;
using Sfa.Das.Sas.ApplicationServices.Queries;
using Sfa.Das.Sas.Shared.Basket.Models;
using Sfa.Das.Sas.Shared.Components.Cookies;
using Sfa.Das.Sas.Shared.Components.UnitTests.ViewComponents.Fat;
using Sfa.Das.Sas.Shared.Components.ViewComponents.Basket;
using System;
using System.Threading;
using System.Threading.Tasks;
using Sfa.Das.Sas.ApplicationServices.Models;

namespace Sfa.Das.Sas.Shared.Components.UnitTests.ViewComponents.Basket
{
    [TestFixture]
    public class AddToBasketViewComponentTests : ViewComponentTestsBase
    {
        private const string APPRENTICESHIP_ID = "100";
        private const int UKPRN = 12345678;
        private const string PROVIDER_NAME = "TestProvider";
        private Mock<ICookieManager> _mockCookieManager;
        private Mock<IMediator> _mockMediator;
        private AddOrRemoveFromBasketViewComponent _sut;

        [SetUp]
        public new void Setup()
        {
            base.Setup();

            var cookieBasketId = Guid.NewGuid();

            _mockCookieManager = new Mock<ICookieManager>();
            _mockCookieManager.Setup(x => x.Get(CookieNames.BasketCookie)).Returns(cookieBasketId.ToString());

            var basket = new ApprenticeshipFavouritesBasket();
            basket.Add("420-2-1", UKPRN,PROVIDER_NAME);

            _mockMediator = new Mock<IMediator>();
            _mockMediator.Setup(x => x.Send(It.Is<GetBasketQuery>(a => a.BasketId == cookieBasketId), default(CancellationToken)))
                .ReturnsAsync(new ApprenticeshipFavouritesBasketRead(basket));

            _sut = new AddOrRemoveFromBasketViewComponent(_mockMediator.Object, _mockCookieManager.Object)
            {
                ViewComponentContext = _viewComponentContext
            };
        }

        [Test]
        public async Task Invoke_ReturnsModelContainingApprenticeshipIdAsItemId_WhenOnlyApprenticeshipIdPassed()
        {
            var result = await _sut.InvokeAsync(APPRENTICESHIP_ID) as ViewViewComponentResult;

            result.Should().BeOfType<ViewViewComponentResult>();
            result.ViewData.Model.Should().BeAssignableTo<AddOrRemoveFromBasketViewModel>();
            var model = result.ViewData.Model as AddOrRemoveFromBasketViewModel;
            model.ItemId.Should().Be(APPRENTICESHIP_ID);
        }

        [Test]
        public async Task Invoke_ReturnsModelContainingUkprnAsItemId_WhenUkPrnPassed()
        {
            var result = await _sut.InvokeAsync(APPRENTICESHIP_ID, UKPRN) as ViewViewComponentResult;

            result.Should().BeOfType<ViewViewComponentResult>();
            result.ViewData.Model.Should().BeAssignableTo<AddOrRemoveFromBasketViewModel>();
            var model = result.ViewData.Model as AddOrRemoveFromBasketViewModel;
            model.ItemId.Should().Be(UKPRN.ToString());
        }

        [Test]
        public async Task Invoke_ReturnsModelContainingIndicatorTrue_IfApprenticehshipAlreadyInBasket()
        {
            var result = await _sut.InvokeAsync("420-2-1") as ViewViewComponentResult;

            result.Should().BeOfType<ViewViewComponentResult>();
            result.ViewData.Model.Should().BeAssignableTo<AddOrRemoveFromBasketViewModel>();
            (result.ViewData.Model as AddOrRemoveFromBasketViewModel).IsInBasket.Should().BeTrue();
        }

        [Test]
        public async Task Invoke_ReturnsModelContainingIndicatorFalse_IfApprenticehshipNotAlreadyInBasket()
        {
            var result = await _sut.InvokeAsync("555-2-1") as ViewViewComponentResult;

            result.Should().BeOfType<ViewViewComponentResult>();
            result.ViewData.Model.Should().BeAssignableTo<AddOrRemoveFromBasketViewModel>();
            (result.ViewData.Model as AddOrRemoveFromBasketViewModel).IsInBasket.Should().BeFalse();
        }

        [Test]
        public async Task Invoke_ReturnsModelContainingIndicatorTrue_IfApprenticehshipWithProviderAlreadyInBasket()
        {
            var result = await _sut.InvokeAsync("420-2-1", UKPRN) as ViewViewComponentResult;

            result.Should().BeOfType<ViewViewComponentResult>();
            result.ViewData.Model.Should().BeAssignableTo<AddOrRemoveFromBasketViewModel>();
            (result.ViewData.Model as AddOrRemoveFromBasketViewModel).IsInBasket.Should().BeTrue();
        }

        [Test]
        public async Task Invoke_ReturnsModelContainingIndicatorFalse_IfApprenticehshipInBasketButNotForProvider()
        {
            var result = await _sut.InvokeAsync("420-2-1", 99999999) as ViewViewComponentResult;

            result.Should().BeOfType<ViewViewComponentResult>();
            result.ViewData.Model.Should().BeAssignableTo<AddOrRemoveFromBasketViewModel>();
            (result.ViewData.Model as AddOrRemoveFromBasketViewModel).IsInBasket.Should().BeFalse();
        }

        [Test]
        public async Task Invoke_ReturnsDefaultView()
        {
            var result = await _sut.InvokeAsync("420-2-1") as ViewViewComponentResult;

            result.ViewName.Should().Be("../Basket/AddOrRemoveFromBasket/Default");
        }
    }
}
