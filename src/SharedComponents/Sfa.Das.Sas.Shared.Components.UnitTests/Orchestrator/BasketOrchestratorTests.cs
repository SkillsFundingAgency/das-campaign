using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Moq;
using NUnit.Framework;
using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.ApplicationServices.Queries;
using Sfa.Das.Sas.ApplicationServices.Services;
using Sfa.Das.Sas.ApplicationServices.Commands;
using Sfa.Das.Sas.Shared.Basket.Models;
using Sfa.Das.Sas.Shared.Components.Cookies;
using Sfa.Das.Sas.Shared.Components.Mapping;
using Sfa.Das.Sas.Shared.Components.Orchestrators;
using Sfa.Das.Sas.Shared.Components.ViewModels.Apprenticeship;
using Sfa.Das.Sas.Shared.Components.ViewModels.Basket;
using Sfa.Das.Sas.Core.Configuration;

namespace Sfa.Das.Sas.Shared.Components.UnitTests.Orchestrator
{
    [TestFixture]
    public class BasketOrchestratorTests
    {
        private BasketOrchestrator _basketOrchestrator;
        private Mock<IMediator> _mediatorMock;
        private Mock<IBasketViewModelMapper> _basketViewModelMapperMock;
        private Mock<ICookieManager> _cookieManagerMock;
        private Mock<ICacheStorageService> _cacheStorageServiceMock;
        private Mock<ICacheSettings> _cacheSettingsMock;
        private Mock<IFatConfigurationSettings> _configMock;


        private ApprenticeshipFavouritesBasket _apprenticeshipFavouritesBasket = new ApprenticeshipFavouritesBasket()
        {
            {  "123"},
            {"123-12-12"}
        };

        private ApprenticeshipFavouritesBasketRead _apprenticeshipFavouritesBasketRead;

        private BasketViewModel<ApprenticeshipBasketItemViewModel> _basketViewModel;

        private const string APPRENTICESHIP_ID = "123";
        private const int UKPRN = 12345678;
        private const string PROVIDER_NAME = "TestProvider";
        private const int LOCATION_ID = 123123;
        private const int LOCATION_ID_TO_ADD = 5555;
       
        private const string BasketCookieName = "ApprenticeshipBasket";
        private string _basketId = "12345678-abcd-1234-abcd-0123456789ab";

        private SaveBasketFromApprenticeshipDetailsViewModel _addFromApprenticeshipDetailsModel;
        private SaveBasketFromApprenticeshipResultsViewModel _addFromApprenticeshipResultsModel;
        private SaveBasketFromProviderDetailsViewModel _addFromProviderDetailsModel;
        private SaveBasketFromProviderSearchViewModel _addFromProviderSearchModel;
        private DeleteFromBasketViewModel _deleteFromBasketViewModel;


        [SetUp]
        public void Setup()
        {
            _basketViewModel = new BasketViewModel<ApprenticeshipBasketItemViewModel>()
            {
                BasketId = Guid.Parse(_basketId)
            };

            _apprenticeshipFavouritesBasketRead = new ApprenticeshipFavouritesBasketRead(_apprenticeshipFavouritesBasket);

            _mediatorMock = new Mock<IMediator>();

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<AddOrRemoveFavouriteInBasketCommand>(), CancellationToken.None))
                .ReturnsAsync(new AddOrRemoveFavouriteInBasketResponse());
            
            _basketViewModelMapperMock = new Mock<IBasketViewModelMapper>();
            _cookieManagerMock = new Mock<ICookieManager>();
            _cacheStorageServiceMock = new Mock<ICacheStorageService>();
            _cacheSettingsMock = new Mock<ICacheSettings>();
            _configMock = new Mock<IFatConfigurationSettings>();

            _addFromApprenticeshipDetailsModel = GetApprenticeshipDetailsRequestModel();
            _addFromApprenticeshipResultsModel = GetApprenticeshipResultsRequestModel();
            _addFromProviderDetailsModel = GetProviderDetailsRequestModel();
            _addFromProviderSearchModel = GetProviderResultsRequestModel();
            _deleteFromBasketViewModel = GetDeleteFromBasketViewModel();

            _basketViewModelMapperMock.Setup(s => s.Map(new ApprenticeshipFavouritesBasketRead(),It.IsAny<Guid>())).Returns(new BasketViewModel<ApprenticeshipBasketItemViewModel>());
            _basketViewModelMapperMock.Setup(s => s.Map(_apprenticeshipFavouritesBasketRead, It.IsAny<Guid>())).Returns(_basketViewModel);

            _basketOrchestrator = new BasketOrchestrator(_mediatorMock.Object, _cookieManagerMock.Object, _basketViewModelMapperMock.Object, _cacheStorageServiceMock.Object, _cacheSettingsMock.Object, _configMock.Object);
        }


        [Test]
        public void When_getting_basket_without_cookie_Then_Empty_Model_Returned()
        {
            _cookieManagerMock.Setup(s=> s.Get(It.IsAny<string>())).Returns((string)null);

            var result = _basketOrchestrator.GetBasket().Result;

            result.BasketId.Should().NotHaveValue();
        }

        [Test]
        public void When_getting_valid_basket_with_cookie_Then_basket_returned()
        {
            _cookieManagerMock.Setup(s => s.Get(It.IsAny<string>())).Returns(_basketId);
            _mediatorMock.Setup(s => s.Send(It.IsAny<GetBasketQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(_apprenticeshipFavouritesBasketRead);

            var result = _basketOrchestrator.GetBasket().Result;

            _basketViewModelMapperMock.Verify(s => s.Map(_apprenticeshipFavouritesBasketRead, It.IsAny<Guid>()), Times.Once);

            result.BasketId.Should().HaveValue();
            result.Should().BeEquivalentTo(_basketViewModel);
        }

        [Test]
        public void When_getting_invalid_basket_with_cookie_Then_model_with()
        {
            _cookieManagerMock.Setup(s => s.Get(It.IsAny<string>())).Returns(_basketId);
            _mediatorMock.Setup(s => s.Send(It.IsAny<GetBasketQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ApprenticeshipFavouritesBasketRead());

            var result = _basketOrchestrator.GetBasket().Result;

            _basketViewModelMapperMock.Verify(s => s.Map(new ApprenticeshipFavouritesBasketRead(),It.IsAny<Guid>()), Times.Once);


            result.BasketId.Should().NotHaveValue();
        }

        [Test]
        public async Task UpdateBasket_UsesBasketIdFromCookieForUpdate_IfCookieExists()
        {
            var BasketIdFromCookie = Guid.NewGuid();
            _cookieManagerMock.Setup(x => x.Get(BasketCookieName)).Returns(BasketIdFromCookie.ToString());
            _addFromApprenticeshipDetailsModel.ItemId = "333"; // Set to a new apprenticeship value

            await _basketOrchestrator.UpdateBasket(_addFromApprenticeshipDetailsModel.ItemId);

            _mediatorMock.Verify(x => x.Send(It.Is<AddOrRemoveFavouriteInBasketCommand>(a => a.BasketId == BasketIdFromCookie), default(CancellationToken)));
        }

        [Test]
        public async Task UpdateBasket_UsesNullForBasketId_IfNoCookieExists()
        {
            await _basketOrchestrator.UpdateBasket(_addFromApprenticeshipDetailsModel.ItemId);

            _mediatorMock.Verify(x => x.Send(It.Is<AddOrRemoveFavouriteInBasketCommand>(a => a.BasketId == null), default(CancellationToken)));
        }

        [Test]
        public async Task UpdateBasket_UsesBasketIdFromCookie_IfCookieExists()
        {
            var BasketIdFromCookie = Guid.NewGuid();
            _cookieManagerMock.Setup(x => x.Get(BasketCookieName)).Returns(BasketIdFromCookie.ToString());
            _addFromApprenticeshipResultsModel.ItemId = "333"; // Set new apprenticeship value

            await _basketOrchestrator.UpdateBasket(_addFromApprenticeshipResultsModel.ItemId);

            _mediatorMock.Verify(x => x.Send(It.Is<AddOrRemoveFavouriteInBasketCommand>(a => a.BasketId == BasketIdFromCookie), default(CancellationToken)));
        }

        [Test]
        public async Task UpdateBasket_SavesBasketIdToCookie()
        {
            var newBasketId = Guid.NewGuid(); // Setup basket it to be returned by save logic
            _mediatorMock.Setup(x => x.Send(It.Is<AddOrRemoveFavouriteInBasketCommand>(a => a.BasketId == null), default(CancellationToken))).ReturnsAsync(new AddOrRemoveFavouriteInBasketResponse(){BasketId = newBasketId});
            _cookieManagerMock.Setup(x => x.Set(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTimeOffset?>()));

            await _basketOrchestrator.UpdateBasket(_addFromApprenticeshipResultsModel.ItemId);

            _cookieManagerMock.Verify(x => x.Set(BasketCookieName, newBasketId.ToString(), It.IsAny<DateTimeOffset?>()));
        }

        [Test]
        public async Task UpdateBasket_ParsesApprenticeshipIdAndUkprn_FromArgument()
        {
            await _basketOrchestrator.UpdateBasket(_addFromProviderDetailsModel.ApprenticeshipId, _addFromProviderDetailsModel.Ukprn);

            _mediatorMock.Verify(x => x.Send(It.Is<AddOrRemoveFavouriteInBasketCommand>(a => a.ApprenticeshipId == APPRENTICESHIP_ID && a.Ukprn == UKPRN), default(CancellationToken)));
        }


        [Test]
        public async Task AddProviderFromResults_UsesBasketIdFromCookie_IfCookieExists()
        {
            var BasketIdFromCookie = Guid.NewGuid();
            _cookieManagerMock.Setup(x => x.Get(BasketCookieName)).Returns(BasketIdFromCookie.ToString());
            _addFromProviderSearchModel.ItemId = "33,10"; // Set new apprenticeship value

            await _basketOrchestrator.UpdateBasket(_addFromProviderSearchModel.ItemId);

            _mediatorMock.Verify(x => x.Send(It.Is<AddOrRemoveFavouriteInBasketCommand>(a => a.BasketId == BasketIdFromCookie), default(CancellationToken)));
        }

        [Test]
        public async Task AddProviderFromResults_UsesNullForBasketId_IfNoCookieExists()
        {
            await _basketOrchestrator.UpdateBasket(_addFromProviderSearchModel.ItemId);

            _mediatorMock.Verify(x => x.Send(It.Is<AddOrRemoveFavouriteInBasketCommand>(a => a.BasketId == null), default(CancellationToken)));
        }

        [Test]
        public async Task AddProviderFromResults_SavesBasketIdToCookie()
        {
            var newBasketId = Guid.NewGuid(); // Setup basket it to be returned by save logic
            _mediatorMock.Setup(x => x.Send(It.Is<AddOrRemoveFavouriteInBasketCommand>(a => a.BasketId == null), default(CancellationToken))).ReturnsAsync(new AddOrRemoveFavouriteInBasketResponse(){BasketId = newBasketId});
            _cookieManagerMock.Setup(x => x.Set(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTimeOffset?>()));

            await _basketOrchestrator.UpdateBasket(_addFromProviderSearchModel.ItemId);

            _cookieManagerMock.Verify(x => x.Set(BasketCookieName, newBasketId.ToString(), It.IsAny<DateTimeOffset?>()));
        }

        [Test]
        public async Task RemoveFromBasket_UsesBasketIdFromCookie_IfCookieExists()
        {
            var BasketIdFromCookie = Guid.NewGuid();
            _cookieManagerMock.Setup(x => x.Get(BasketCookieName)).Returns(BasketIdFromCookie.ToString());

            await _basketOrchestrator.UpdateBasket(_deleteFromBasketViewModel.ApprenticeshipId);

            _mediatorMock.Verify(x => x.Send(It.Is<AddOrRemoveFavouriteInBasketCommand>(a => a.BasketId == BasketIdFromCookie), default(CancellationToken)));
        }



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

    }
}
