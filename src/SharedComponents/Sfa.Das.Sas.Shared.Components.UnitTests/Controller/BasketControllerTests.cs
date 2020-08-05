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
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.ApplicationServices.Queries;
using Sfa.Das.Sas.Shared.Basket.Models;
using Sfa.Das.Sas.Shared.Components.Orchestrators;

namespace Sfa.Das.Sas.Shared.Components.UnitTests.Controller
{
    [TestFixture]
    public class BasketControllerTests
    {
        private const string APPRENTICESHIP_ID = "123";
        private const int UKPRN = 12345678;
        private const string PROVIDER_NAME = "TestProvider";
        private const int LOCATION_ID = 123123;
        private const int LOCATION_ID_TO_ADD = 5555;
        private const string BasketCookieName = "ApprenticeshipBasket";
        private Mock<IMediator> _mockMediator;
        private Mock<ICookieManager> _mockCookieManager;
        private Mock<IBasketOrchestrator> _mockBasketOrchestrator;
        private BasketController _basketController;

        private SaveBasketFromApprenticeshipDetailsViewModel _addFromApprenticeshipDetailsModel;
        private SaveBasketFromApprenticeshipResultsViewModel _addFromApprenticeshipResultsModel;
        private SaveBasketFromProviderDetailsViewModel _addFromProviderDetailsModel;
        private SaveBasketFromProviderSearchViewModel _addFromProviderSearchModel;
        private DeleteFromBasketViewModel _deleteFromBasketViewModel;

        [SetUp]
        public void Setup()
        {
            _addFromApprenticeshipDetailsModel = GetApprenticeshipDetailsRequestModel();
            _addFromApprenticeshipResultsModel = GetApprenticeshipResultsRequestModel();
            _addFromProviderDetailsModel = GetProviderDetailsRequestModel();
            _addFromProviderSearchModel = GetProviderResultsRequestModel();
            _deleteFromBasketViewModel = GetDeleteFromBasketViewModel();

            // Set cookie in http request
            _mockMediator = new Mock<IMediator>();
            _mockCookieManager = new Mock<ICookieManager>();
            _mockBasketOrchestrator = new Mock<IBasketOrchestrator>();

            _mockBasketOrchestrator
                .Setup(orchestrator => 
                    orchestrator.UpdateBasket(It.IsAny<string>(),null,null))
                .ReturnsAsync(new AddOrRemoveFavouriteInBasketResponse());
            
            _mockMediator.Setup(s => s.Send(It.IsAny<GetBasketQuery>(), default(CancellationToken))).ReturnsAsync(GetApprenticeshipFavouritesBasketRead());

            _basketController = new BasketController(_mockMediator.Object, _mockCookieManager.Object, _mockBasketOrchestrator.Object);
            
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            _basketController.TempData = tempData;
        }

        #region AddApprenticeshipFromDetails

        [Test]
        public async Task AddApprenticeshipFromDetails_ReturnsRedirectResult_ToApprenticeshipDetailsPage()
        {
            var result = await _basketController.AddApprenticeshipFromDetails(_addFromApprenticeshipDetailsModel);

            result.Should().BeAssignableTo<RedirectToActionResult>();
            var redirect = (RedirectToActionResult)result;

            redirect.ControllerName.Should().Be("Fat");
            redirect.ActionName.Should().Be("Apprenticeship");
            var routeValues = redirect.RouteValues;
            routeValues["id"].Should().Be(APPRENTICESHIP_ID);
        }

        [Test]
        public async Task AddApprenticeshipFromDetails_InvokesUpdateBasket_WithApprenticeshipIdFromArgument()
        {
            var result = await _basketController.AddApprenticeshipFromDetails(_addFromApprenticeshipDetailsModel);

            _mockBasketOrchestrator.Verify(x => x.UpdateBasket(APPRENTICESHIP_ID, null, null));
        }

        #endregion

        #region AddApprenticeshipFromResults

        [Test]
        public async Task AddApprenticeshipFromResults_ReturnsRedirectResult_ToApprenticeshipDetailsPage()
        {
            var result = await _basketController.AddApprenticeshipFromResults(_addFromApprenticeshipResultsModel);

            result.Should().BeAssignableTo<RedirectToActionResult>();
            var redirect = (RedirectToActionResult)result;

            redirect.ControllerName.Should().Be("Fat");
            redirect.ActionName.Should().Be("Search");
            var routeValues = redirect.RouteValues;
            routeValues["Keywords"].Should().Be(_addFromApprenticeshipResultsModel.SearchQuery.Keywords);
            routeValues["Page"].Should().Be(_addFromApprenticeshipResultsModel.SearchQuery.Page);
            routeValues["ResultsToTake"].Should().Be(_addFromApprenticeshipResultsModel.SearchQuery.ResultsToTake);
            routeValues["SortOrder"].Should().Be(_addFromApprenticeshipResultsModel.SearchQuery.SortOrder);
            routeValues["SelectedLevels"].Should().Be(_addFromApprenticeshipResultsModel.SearchQuery.SelectedLevels);
        }

        [Test]
        public async Task AddApprenticeshipFromResults_InvokesUpdateBasket_WithApprenticeshipIdFromArgument()
        {
            var result = await _basketController.AddApprenticeshipFromResults(_addFromApprenticeshipResultsModel);

            _mockBasketOrchestrator.Verify(x => x.UpdateBasket(APPRENTICESHIP_ID, null, null));
        }


        #endregion

        #region AddProviderFromDetails

        [Test]
        public async Task AddProviderFromDetails_ReturnsRedirectResult_ToProviderDetailsPage()
        {
            var result = await _basketController.AddProviderFromDetails(_addFromProviderDetailsModel);

            result.Should().BeAssignableTo<RedirectToActionResult>();
            var redirect = (RedirectToActionResult)result;

            redirect.ControllerName.Should().Be("TrainingProvider");
            redirect.ActionName.Should().Be("Details");
            var routeValues = redirect.RouteValues;
            routeValues["ukprn"].Should().Be(UKPRN);
            routeValues["apprenticeshipId"].Should().Be(APPRENTICESHIP_ID);
            routeValues["locationId"].Should().Be(LOCATION_ID);
        }

        [Test]
        public async Task AddProviderFromDetails_InvokesUpdateBasket_WithApprenticeshipIdUkprnAndLocationIdFromArgument()
        {
            var result = await _basketController.AddProviderFromDetails(_addFromProviderDetailsModel);

            _mockBasketOrchestrator.Verify(x => x.UpdateBasket(APPRENTICESHIP_ID, UKPRN, LOCATION_ID_TO_ADD));
        }
        #endregion

        #region AddProviderFromResults

        [Test]
        public async Task AddProviderFromResults_ReturnsRedirectResult_ToApprenticeshipDetailsPage()
        {
            var result = await _basketController.AddProviderFromResults(_addFromProviderSearchModel);

            result.Should().BeAssignableTo<RedirectToActionResult>();
            var redirect = (RedirectToActionResult)result;

            redirect.ControllerName.Should().Be("TrainingProvider");
            redirect.ActionName.Should().Be("Search");
            var routeValues = redirect.RouteValues;
            routeValues["ApprenticeshipId"].Should().Be(_addFromProviderSearchModel.SearchQuery.ApprenticeshipId);
            routeValues["IsLevyPayer"].Should().Be(_addFromProviderSearchModel.SearchQuery.IsLevyPayer);
            routeValues["NationalProvidersOnly"].Should().Be(_addFromProviderSearchModel.SearchQuery.NationalProvidersOnly);
            routeValues["Page"].Should().Be(_addFromProviderSearchModel.SearchQuery.Page);
            routeValues["Postcode"].Should().Be(_addFromProviderSearchModel.SearchQuery.Postcode);
            routeValues["SortOrder"].Should().Be(_addFromProviderSearchModel.SearchQuery.SortOrder);
        }

        [Test]
        public async Task AddProviderFromResults_ParsesApprenticeshipIdAndUkprn_FromArgument()
        {
            var result = await _basketController.AddProviderFromResults(_addFromProviderSearchModel);

            _mockBasketOrchestrator.Verify(x => x.UpdateBasket(APPRENTICESHIP_ID, UKPRN, LOCATION_ID_TO_ADD));
        }

        #endregion


        #region RemoveFromBasket

        [Test]
        public async Task RemoveFromBasket_ReturnsRedirectResult_ToBasketPage()
        {
            var BasketIdFromCookie = Guid.NewGuid();
            _mockCookieManager.Setup(x => x.Get(BasketCookieName)).Returns(BasketIdFromCookie.ToString());

            var result = await _basketController.RemoveFromBasket(_deleteFromBasketViewModel);

            result.Should().BeAssignableTo<RedirectToActionResult>();
            var redirect = (RedirectToActionResult)result;

            redirect.ControllerName.Should().Be("Basket");
            redirect.ActionName.Should().Be("View");
        }

        [Test]
        public async Task RemoveFromBasket_InvokesUpdateBasket_WithApprenticeshipIdAndUkprnFromArgument()
        {
            var BasketIdFromCookie = Guid.NewGuid();
            _mockCookieManager.Setup(x => x.Get(BasketCookieName)).Returns(BasketIdFromCookie.ToString());

            var result = await _basketController.RemoveFromBasket(_deleteFromBasketViewModel);

            _mockBasketOrchestrator.Verify(x => x.UpdateBasket(APPRENTICESHIP_ID, UKPRN, null));
        }
        [Test]
        public async Task RemoveFromBasket_RedirectToBasket_IfNoCookieExists()
        {
            var result = await _basketController.RemoveFromBasket(_deleteFromBasketViewModel);

            result.Should().BeAssignableTo<RedirectToActionResult>();
            var redirect = (RedirectToActionResult)result;

            redirect.ControllerName.Should().Be("Basket");
            redirect.ActionName.Should().Be("View");
        }

        [Test]
        public async Task RemoveFromBasket_RedirectToBasket_IfItemDoesntExistInBakset()
        {
            var removeViewModel = _deleteFromBasketViewModel;

            removeViewModel.Ukprn = 456789012;

            var result = await _basketController.RemoveFromBasket(removeViewModel);

            result.Should().BeAssignableTo<RedirectToActionResult>();
            var redirect = (RedirectToActionResult)result;

            redirect.ControllerName.Should().Be("Basket");
            redirect.ActionName.Should().Be("View");
        }

        #endregion
 

        private static SaveBasketFromApprenticeshipDetailsViewModel GetApprenticeshipDetailsRequestModel() => new SaveBasketFromApprenticeshipDetailsViewModel
        {
            ItemId = APPRENTICESHIP_ID
        };

        private static SaveBasketFromApprenticeshipResultsViewModel GetApprenticeshipResultsRequestModel() => new SaveBasketFromApprenticeshipResultsViewModel
        {
            ItemId = APPRENTICESHIP_ID,
            SearchQuery = new ViewModels.SearchQueryViewModel
            {
                Keywords = "baker",
                Page = 3,
                ResultsToTake = 40,
                SortOrder = 1
            }
        };

        private static SaveBasketFromProviderDetailsViewModel GetProviderDetailsRequestModel() => new SaveBasketFromProviderDetailsViewModel
        {
            ItemId = $"{UKPRN},{LOCATION_ID_TO_ADD}",
            ApprenticeshipId = APPRENTICESHIP_ID,
            ApprenticeshipType = ApplicationServices.Models.ApprenticeshipType.Standard,
            Page = 1,
            Ukprn = UKPRN,
            LocationId = LOCATION_ID,
            ViewType = ViewModels.ViewType.Details
        };

        private static SaveBasketFromProviderSearchViewModel GetProviderResultsRequestModel() => new SaveBasketFromProviderSearchViewModel
        {
            ItemId = $"{UKPRN.ToString()},{LOCATION_ID_TO_ADD}",
            SearchQuery = new Components.ViewComponents.TrainingProvider.Search.TrainingProviderSearchViewModel
            {
                ApprenticeshipId = APPRENTICESHIP_ID,
                IsLevyPayer = true,
                Keywords = "some keywords",
                Page = 1,
                Postcode = "AB12 3TR",
                ResultsToTake = 10,
                SortOrder = 1
            }
        };

        private static DeleteFromBasketViewModel GetDeleteFromBasketViewModel() => new DeleteFromBasketViewModel()
        {
            ApprenticeshipId = APPRENTICESHIP_ID,
            Ukprn = UKPRN
        };

        private static ApprenticeshipFavouritesBasketRead GetApprenticeshipFavouritesBasketRead()
        {

            var basket = new ApprenticeshipFavouritesBasket();
            basket.Add(APPRENTICESHIP_ID, UKPRN, PROVIDER_NAME);

            return new ApprenticeshipFavouritesBasketRead(basket);
        }
    }
}
