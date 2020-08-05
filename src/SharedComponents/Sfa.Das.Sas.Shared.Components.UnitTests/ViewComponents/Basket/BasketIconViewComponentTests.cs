using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using NUnit.Framework;
using Sfa.Das.Sas.Shared.Components.UnitTests.ViewComponents.Fat;
using Sfa.Das.Sas.Shared.Components.ViewComponents.Basket;
using Microsoft.AspNetCore.Mvc;
using Sfa.Das.Sas.Shared.Components.Orchestrators;
using Sfa.Das.Sas.Shared.Components.ViewModels.Basket;
using Sfa.Das.Sas.Shared.Components.ViewModels.Apprenticeship;
using System.Collections.Generic;
using Sfa.Das.Sas.Shared.Components.ViewComponents.Fat;

namespace Sfa.Das.Sas.Shared.Components.UnitTests.ViewComponents.Basket
{
    [TestFixture]
    public class BasketIconViewComponentTests : ViewComponentTestsBase
    {
        private const string ExpectedBasketViewUrl = "url-of-basket-view";
        private Mock<IBasketOrchestrator> _mockBasketOrchestrator;
        private Mock<IMediator> _mockMediator;
        private BasketIconViewComponent _sut;
        private BasketViewModel<ApprenticeshipBasketItemViewModel> _basketViewModel;

        [SetUp]
        public new void Setup()
        {
            base.Setup();

            _basketViewModel = new BasketViewModel<ApprenticeshipBasketItemViewModel> { Items = new List<ApprenticeshipBasketItemViewModel>() };
            _mockBasketOrchestrator = new Mock<IBasketOrchestrator>();
            _mockBasketOrchestrator.Setup(x => x.GetBasket()).ReturnsAsync(_basketViewModel);

            _mockMediator = new Mock<IMediator>();

            _sut = new BasketIconViewComponent(_mockMediator.Object, _mockBasketOrchestrator.Object)
            {
                ViewComponentContext = _viewComponentContext
            };

            // Setup url helper for link generation
            var urlHelper = new Mock<IUrlHelper>();
            urlHelper.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns(ExpectedBasketViewUrl);
            _sut.Url = urlHelper.Object;
        }

        [Test]
        public async Task Invoke_ReturnsModelContainingItemCountOfZero_WhenBasketIsEmpty()
        {
            var result = await _sut.InvokeAsync() as ViewViewComponentResult;

            result.Should().BeOfType<ViewViewComponentResult>();
            result.ViewData.Model.Should().BeAssignableTo<BasketIconViewModel>();
            var model = result.ViewData.Model as BasketIconViewModel;
            model.ItemCount.Should().Be(0);
        }

        [Test]
        public async Task Invoke_ReturnsModelContainingItemCountOfZero_WhenBasketDoesNotExist()
        {
            _basketViewModel.Items = null;

            var result = await _sut.InvokeAsync() as ViewViewComponentResult;

            result.Should().BeOfType<ViewViewComponentResult>();
            result.ViewData.Model.Should().BeAssignableTo<BasketIconViewModel>();
            var model = result.ViewData.Model as BasketIconViewModel;
            model.ItemCount.Should().Be(0);
        }

        [Test]
        public async Task Invoke_ReturnsModelContainingItemCountInBasket_WhenOnlyApprenticeshipsInBasket()
        {
            // Add a couple of items to the basket
            _basketViewModel.Items.Add(new ApprenticeshipBasketItemViewModel());
            _basketViewModel.Items.Add(new ApprenticeshipBasketItemViewModel());

            var result = await _sut.InvokeAsync() as ViewViewComponentResult;

            result.Should().BeOfType<ViewViewComponentResult>();
            result.ViewData.Model.Should().BeAssignableTo<BasketIconViewModel>();
            var model = result.ViewData.Model as BasketIconViewModel;
            model.ItemCount.Should().Be(2);
        }

        [Test]
        public async Task Invoke_ReturnsModelContainingItemCountInBasket_WhenApprenticeshipWithProvidersInBasket()
        {
            // Add a couple of items to the basket
            _basketViewModel.Items.Add(new ApprenticeshipBasketItemViewModel
            {
                TrainingProvider = new List<TrainingProviderSearchResultsItem>()
                {
                     new TrainingProviderSearchResultsItem(),
                     new TrainingProviderSearchResultsItem(),
                     new TrainingProviderSearchResultsItem()
                }
            });

            var result = await _sut.InvokeAsync() as ViewViewComponentResult;

            result.Should().BeOfType<ViewViewComponentResult>();
            result.ViewData.Model.Should().BeAssignableTo<BasketIconViewModel>();
            var model = result.ViewData.Model as BasketIconViewModel;
            model.ItemCount.Should().Be(4);
        }

        [Test]
        public async Task Invoke_ReturnsModelContainingUrlOfBasketPage()
        {
            var result = await _sut.InvokeAsync() as ViewViewComponentResult;

            result.Should().BeOfType<ViewViewComponentResult>();
            result.ViewData.Model.Should().BeAssignableTo<BasketIconViewModel>();
            var model = result.ViewData.Model as BasketIconViewModel;
            model.BasketUrl.Should().Be(ExpectedBasketViewUrl);
        }

        [Test]
        public async Task Invoke_ReturnsDefaultView()
        {
            var result = await _sut.InvokeAsync() as ViewViewComponentResult;

            result.ViewName.Should().Be("../Basket/BasketIcon/Default");
        }
    }
}
