using System.Threading;
using FluentAssertions;
using MediatR;
using Moq;
using NUnit.Framework;
using SFA.DAS.NLog.Logger;
using Sfa.Das.Sas.ApplicationServices.Queries;
using Sfa.Das.Sas.ApplicationServices.Responses;
using Sfa.Das.Sas.Shared.Components.Mapping;
using Sfa.Das.Sas.Shared.Components.Orchestrators;
using Sfa.Das.Sas.Shared.Components.ViewComponents.Fat;
using Sfa.Das.Sas.Shared.Components.ViewComponents.TrainingProvider;
using Sfa.Das.Sas.Shared.Components.ViewComponents.TrainingProvider.Search;
using Sfa.Das.Sas.Shared.Components.ViewModels;
using Sfa.Das.Sas.ApplicationServices.Services;
using System.Threading.Tasks;
using Sfa.Das.Sas.Core.Configuration;

namespace Sfa.Das.Sas.Shared.Components.UnitTests.Orchestrator
{
    [TestFixture]
    public class TrainingProviderOrchestratorTests
    {
        private TrainingProviderOrchestrator _trainingProviderOrchestrator;
        private Mock<IMediator> _mockMediator;
        private Mock<ISearchResultsViewModelMapper> _mockSearchResultsViewModelMapper;
        private Mock<ITrainingProviderDetailsViewModelMapper> _mockTrainingProviderDetailsViewModelMapper;
        private Mock<ITrainingProviderSearchFilterViewModelMapper> _mockTrainingProviderFilterViewModelMapper;
        private Mock<ITrainingProviderClosestLocationsViewModelMapper> _mockTrainingProviderClosestLocationsViewModelMapper;
        private Mock<ILog> _mockLogger;
        private Mock<ICacheStorageService> _mockCacheService;
        private Mock<ICacheSettings> _mockCacheSettings;

        private readonly TrainingProviderSearchViewModel _searchQueryViewModel = new TrainingProviderSearchViewModel();
        private readonly TrainingProviderDetailQueryViewModel _detailsQueryViewModel = new TrainingProviderDetailQueryViewModel();

        private readonly GroupedProviderSearchResponse _searchResults = new GroupedProviderSearchResponse() { Success = true };
        private readonly SearchResultsViewModel<TrainingProviderSearchResultsItem, TrainingProviderSearchViewModel> _searchResultsViewModel = new SearchResultsViewModel<TrainingProviderSearchResultsItem, TrainingProviderSearchViewModel>();
        private readonly TrainingProviderSearchFilterViewModel _searchFilterViewModel = new TrainingProviderSearchFilterViewModel();
        private readonly ClosestLocationsViewModel _closestLocationsViewModel = new ClosestLocationsViewModel();
        [SetUp]
        public void Setup()
        {

            _mockMediator = new Mock<IMediator>();
            _mockSearchResultsViewModelMapper = new Mock<ISearchResultsViewModelMapper>();
            _mockTrainingProviderDetailsViewModelMapper = new Mock<ITrainingProviderDetailsViewModelMapper>();
            _mockTrainingProviderFilterViewModelMapper = new Mock<ITrainingProviderSearchFilterViewModelMapper>();
            _mockTrainingProviderClosestLocationsViewModelMapper = new Mock<ITrainingProviderClosestLocationsViewModelMapper>();
            _mockLogger = new Mock<ILog>();


            _mockCacheService = new Mock<ICacheStorageService>();
            _mockCacheSettings = new Mock<ICacheSettings>();

            _mockMediator.Setup(s => s.Send(It.IsAny<GroupedProviderSearchQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(_searchResults);
            _mockSearchResultsViewModelMapper.Setup(s => s.Map(It.IsAny<GroupedProviderSearchResponse>(), It.IsAny<TrainingProviderSearchViewModel>())).Returns(_searchResultsViewModel);
            _mockTrainingProviderFilterViewModelMapper.Setup(s => s.Map(It.IsAny<GroupedProviderSearchResponse>(), It.IsAny<TrainingProviderSearchViewModel>())).Returns(_searchFilterViewModel);
            _mockTrainingProviderClosestLocationsViewModelMapper.Setup(s => s.Map(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<GetClosestLocationsResponse>())).Returns(_closestLocationsViewModel);

            _detailsQueryViewModel.ApprenticeshipId = "123";
            _detailsQueryViewModel.Ukprn = 10000020;
            _detailsQueryViewModel.LocationId = 100;

            _trainingProviderOrchestrator = new TrainingProviderOrchestrator(_mockMediator.Object, _mockSearchResultsViewModelMapper.Object,_mockLogger.Object,_mockTrainingProviderDetailsViewModelMapper.Object,_mockTrainingProviderFilterViewModelMapper.Object, _mockCacheService.Object, _mockCacheSettings.Object, _mockTrainingProviderClosestLocationsViewModelMapper.Object);
        }

        [Test]
        public void When_SearchResultsRequested_Then_TrainingProviderSearchResultsViewModel_Is_Returned()
        {
            _searchQueryViewModel.Keywords = "keyword";
            _searchQueryViewModel.ApprenticeshipId = "123";
            _searchQueryViewModel.Postcode = "NN123NN";

            var result = _trainingProviderOrchestrator.GetSearchResults(_searchQueryViewModel).Result;

            result.Should().BeOfType<SearchResultsViewModel<TrainingProviderSearchResultsItem, TrainingProviderSearchViewModel>>();
        }

        [Test]
        public void When_SearchResultsRequested_Then_TrainingProvider_Are_Searched_By_Apprenticeship_And_Location()
        {
            _searchQueryViewModel.Keywords = "keyword";

            _trainingProviderOrchestrator.GetSearchResults(_searchQueryViewModel);

            _mockMediator.Verify(s => s.Send<GroupedProviderSearchResponse>(It.IsAny<GroupedProviderSearchQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }
        [Test]
        public void When_SearchResultsRequested_Then_Search_Results_Are_Mapped_To_ViewModel()
        {
            _searchQueryViewModel.Keywords = "keyword";

            var result = _trainingProviderOrchestrator.GetSearchResults(_searchQueryViewModel).Result;

            result.Should().BeOfType<SearchResultsViewModel<TrainingProviderSearchResultsItem, TrainingProviderSearchViewModel>>();

            _mockSearchResultsViewModelMapper.Verify(v => v.Map(_searchResults, _searchQueryViewModel));
        }

        [Test]
        public void When_SearchResultsRequested_With_LevyPayerOnly_Then_Query_Is_Mapped_To_Handler()
        {
            _searchQueryViewModel.IsLevyPayer = true;

            _trainingProviderOrchestrator.GetSearchResults(_searchQueryViewModel);

            _mockMediator.Verify(s => s.Send<GroupedProviderSearchResponse>(It.Is<GroupedProviderSearchQuery>(x => x.IsLevyPayingEmployer == true), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void When_SearchFilterRequested_Then_TrainingProviderSearchResultsViewModel_Is_Returned()
        {
            _searchQueryViewModel.Keywords = "keyword";
            _searchQueryViewModel.ApprenticeshipId = "123";
            _searchQueryViewModel.Postcode = "NN123NN";

            var result = _trainingProviderOrchestrator.GetSearchFilter(_searchQueryViewModel).Result;

            result.Should().BeOfType<TrainingProviderSearchFilterViewModel>();
        }

        [Test]
        public void When_SearchFilterRequested_Then_TrainingProvider_Are_Searched_By_Apprenticeship_And_Location()
        {
            _searchQueryViewModel.Keywords = "keyword";

            _trainingProviderOrchestrator.GetSearchFilter(_searchQueryViewModel);

            _mockMediator.Verify(s => s.Send<GroupedProviderSearchResponse>(It.IsAny<GroupedProviderSearchQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }
        [Test]
        public void When_SearchFilterRequested_Then_Search_Results_Are_Mapped_To_ViewModel()
        {
            _searchQueryViewModel.Keywords = "keyword";

            var result = _trainingProviderOrchestrator.GetSearchFilter(_searchQueryViewModel).Result;

            result.Should().BeOfType<TrainingProviderSearchFilterViewModel>();

            _mockTrainingProviderFilterViewModelMapper.Verify(v => v.Map(_searchResults, _searchQueryViewModel));
        }

        [Test]
        public async Task When_ClosestLocationsRequested_Then_ClosestLocationsViewModel_Is_Returned()
        {
            var result = await _trainingProviderOrchestrator.GetClosestLocations("123", 12345678, 222, "AB12 3DF");

            result.Should().BeOfType<ClosestLocationsViewModel>();
        }

        [Test]
        public void When_ClosestLocationsRequested_Then_Mediator_Is_Called()
        {
            _trainingProviderOrchestrator.GetClosestLocations("123", 12345678, 222, "AB12 3DF");

            _mockMediator.Verify(s => s.Send<GetClosestLocationsResponse>(It.IsAny<GetClosestLocationsQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }
        [Test]
        public async Task When_ClosestLocationsRequested_Then_Search_Results_Are_Mapped_To_ViewModel()
        {
            await _trainingProviderOrchestrator.GetClosestLocations("123", 12345678, 222, "AB12 3DF");

            _mockTrainingProviderClosestLocationsViewModelMapper.Verify(v => v.Map("123", 12345678, 222, "AB12 3DF", It.IsAny<GetClosestLocationsResponse>()));
            
            _mockMediator.Verify(s => s.Send<ApprenticeshipProviderDetailResponse>(It.IsAny<ApprenticeshipProviderDetailQuery>(), It.IsAny<CancellationToken>()), Times.Never());

        }
        
         [Test]
        public void When_SearchResultsRequested_Then_CallsMediatrWhenCacheIsEmpty()
        {

            var cacheKey = _detailsQueryViewModel.Ukprn + _detailsQueryViewModel.LocationId + _detailsQueryViewModel.ApprenticeshipId;

            _mockCacheService.Setup(s => s.RetrieveFromCache<TrainingProviderDetailsViewModel>(cacheKey)).Returns(GenerateNullCachedProviderViewModel());

            _trainingProviderOrchestrator.GetDetails(_detailsQueryViewModel);

            _mockMediator.Verify(s => s.Send<ApprenticeshipProviderDetailResponse>(It.IsAny<ApprenticeshipProviderDetailQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            
        }

        [Test]
        public void When_SearchResultsRequested_Then_GetResultsFromCache()
        {
           
            var cacheKey = $"FatComponentsCache-providerdetails-{_detailsQueryViewModel.Ukprn}-{_detailsQueryViewModel.LocationId}-{_detailsQueryViewModel.ApprenticeshipId}";

            _mockCacheService.Setup(s => s.RetrieveFromCache<TrainingProviderDetailsViewModel>(cacheKey)).Returns(GenerateMockCachedProviderViewModel());

            _trainingProviderOrchestrator.GetDetails(_detailsQueryViewModel);

            _mockMediator.Verify(s => s.Send<ApprenticeshipProviderDetailResponse>(It.IsAny<ApprenticeshipProviderDetailQuery>(), It.IsAny<CancellationToken>()), Times.Never());

        }

        private async Task<TrainingProviderDetailsViewModel> GenerateMockCachedProviderViewModel()
        {
            return new TrainingProviderDetailsViewModel()
            {
                Name = "Test Provider"
            };

        }

        private async Task<TrainingProviderDetailsViewModel> GenerateNullCachedProviderViewModel()
        {
            return null;
        }
    }
}
