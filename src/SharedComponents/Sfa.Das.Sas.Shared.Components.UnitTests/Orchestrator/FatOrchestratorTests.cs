using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Sfa.Das.Sas.ApplicationServices;
using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.ApplicationServices.Services;
using Sfa.Das.Sas.Core.Configuration;
using Sfa.Das.Sas.Shared.Components.Configuration;
using Sfa.Das.Sas.Shared.Components.Mapping;
using Sfa.Das.Sas.Shared.Components.Orchestrators;
using Sfa.Das.Sas.Shared.Components.ViewComponents.Fat;
using Sfa.Das.Sas.Shared.Components.ViewModels;

namespace Sfa.Das.Sas.Shared.Components.UnitTests.Orchestrator
{
    [TestFixture]
    public class FatOrchestratorTests
    {
        private FatOrchestrator _sut;
        private Mock<IApprenticeshipSearchService> _apprenticeshipSearchServicetMock;
        private Mock<IFatSearchResultsViewModelMapper> _FatResultsViewModelMock;
        private Mock<IFatSearchFilterViewModelMapper> _fatSearchFilterViewModelMapper;
        private Mock<ICacheStorageService> _cacheServiceMock;
        private Mock<ICacheSettings> _cacheSettingsMock;

        private  SearchQueryViewModel _searchQueryViewModel = new SearchQueryViewModel();

        private ApprenticeshipSearchResults _searchResults = new ApprenticeshipSearchResults();
        private FatSearchResultsViewModel _searchResultsViewModel = new FatSearchResultsViewModel();
        private FatSearchFilterViewModel _searchFilterViewModel = new FatSearchFilterViewModel();

        [SetUp]
        public void Setup()
        {

            _apprenticeshipSearchServicetMock = new Mock<IApprenticeshipSearchService>(MockBehavior.Strict);
            _FatResultsViewModelMock = new Mock<IFatSearchResultsViewModelMapper>(MockBehavior.Strict);
            _fatSearchFilterViewModelMapper = new Mock<IFatSearchFilterViewModelMapper>();
            _cacheServiceMock = new Mock<ICacheStorageService>();
            _cacheSettingsMock = new Mock<ICacheSettings>();

            _apprenticeshipSearchServicetMock.Setup(s => s.SearchByKeyword(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<List<int>>())).ReturnsAsync(_searchResults);

            _FatResultsViewModelMock.Setup(s => s.Map(_searchResults)).Returns(_searchResultsViewModel);

            _fatSearchFilterViewModelMapper.Setup(s => s.Map(_searchResults,_searchQueryViewModel)).Returns(_searchFilterViewModel);

            _sut = new FatOrchestrator(_apprenticeshipSearchServicetMock.Object, _FatResultsViewModelMock.Object, _fatSearchFilterViewModelMapper.Object, _cacheServiceMock.Object, _cacheSettingsMock.Object);
        }

        [Test]
        public void Then_FatSearchResultsViewModel_Is_Returned()
        {
            _searchQueryViewModel.Keywords = "keyword";

            var result = _sut.GetSearchResults(_searchQueryViewModel).Result;

            result.Should().BeOfType<FatSearchResultsViewModel>();
        }

        [Test]
        public void Then_Apprenticeships_Are_Searched_By_Keyword()
        {
            _searchQueryViewModel.Keywords = "keyword";

            var result = _sut.GetSearchResults(_searchQueryViewModel);

            _apprenticeshipSearchServicetMock.Verify(s => s.SearchByKeyword(_searchQueryViewModel.Keywords, It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<List<int>>()));
        }
        [Test]
        public void Then_Search_Results_Are_Mapped_To_ViewModel()
        {
            _searchQueryViewModel.Keywords = "keyword";

            var result = _sut.GetSearchResults(_searchQueryViewModel).Result;

            result.Should().BeOfType<FatSearchResultsViewModel>();

            _FatResultsViewModelMock.Verify(v => v.Map(_searchResults));


        }

        [Test]
        public async Task Then_FatSearchFilterViewModel_Is_Returned()
        {
            _searchQueryViewModel.Keywords = "keyword";

            var result = await _sut.GetSearchFilters(_searchQueryViewModel);

            result.Should().BeOfType<FatSearchFilterViewModel>();
        }
        [Test]
        public async Task Then_Search_Filter_Is_Mapped_To_ViewModel()
        {
            _searchQueryViewModel.Keywords = "keyword";

            var result = await _sut.GetSearchFilters(_searchQueryViewModel);

            result.Should().BeOfType<FatSearchFilterViewModel>();

            _fatSearchFilterViewModelMapper.Verify(v => v.Map(_searchResults,_searchQueryViewModel));

        }

        [Test]
        public async Task When_Getting_Standard_Then_Retrieve_From_Cache()
        {
            _searchQueryViewModel.Keywords = "keyword";
            _cacheServiceMock.Setup(c => c.RetrieveFromCache<FatSearchResultsViewModel>(It.IsAny<string>())).ReturnsAsync(new FatSearchResultsViewModel() { SearchResults = { } });

            var result = await _sut.GetSearchResults(_searchQueryViewModel);

            _apprenticeshipSearchServicetMock.Verify(s => s.SearchByKeyword(_searchQueryViewModel.Keywords, It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<List<int>>()), Times.Never);
        }
    }
}
