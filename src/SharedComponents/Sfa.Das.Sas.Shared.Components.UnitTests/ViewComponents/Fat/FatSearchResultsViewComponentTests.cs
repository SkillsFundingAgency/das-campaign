using FluentAssertions;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using NUnit.Framework;
using Sfa.Das.Sas.Shared.Components.ViewComponents.Fat;
using Sfa.Das.Sas.Shared.Components.ViewComponents.Fat.SearchResults;
using System.Threading.Tasks;
using Sfa.Das.Sas.Shared.Components.Orchestrators;
using Sfa.Das.Sas.Shared.Components.ViewModels;

namespace Sfa.Das.Sas.Shared.Components.UnitTests.ViewComponents.Fat
{
    [TestFixture]
    public class FatSearchResultsViewComponentTests : ViewComponentTestsBase
    {
        private FatSearchResultsViewComponent _sut;
        private Mock<IFatOrchestrator> _fatOrchestratorMock;

        private readonly SearchQueryViewModel _searchQueryViewModel = new SearchQueryViewModel();
        private readonly FatSearchResultsViewModel _searchResultsViewModel = new FatSearchResultsViewModel();

        [SetUp]
        public void Setup()
        {
            base.Setup();

            _fatOrchestratorMock = new Mock<IFatOrchestrator>(MockBehavior.Strict);

            _fatOrchestratorMock.Setup(s => s.GetSearchResults(It.IsAny<SearchQueryViewModel>())).ReturnsAsync(_searchResultsViewModel);


            _sut = new FatSearchResultsViewComponent(_fatOrchestratorMock.Object)
            {
                ViewComponentContext = _viewComponentContext
            };
        }

        [Test]
        public async Task Then_FatSearchResultsViewModel_Is_Returned()
        {
            _searchQueryViewModel.Keywords = "keyword";

            var result = await _sut.InvokeAsync(_searchQueryViewModel) as ViewViewComponentResult;

            result.Should().BeOfType<ViewViewComponentResult>();

            result.ViewData.Model.Should().BeOfType<FatSearchResultsViewModel>();
            result.ViewData.Model.Should().Be(_searchResultsViewModel);
        }

        [Test]
        public async Task Then_Apprenticeships_Are_Searched()
        {
            _searchQueryViewModel.Keywords = "keyword";

            var result = await _sut.InvokeAsync(_searchQueryViewModel) as ViewViewComponentResult;

            _fatOrchestratorMock.Verify(s => s.GetSearchResults(_searchQueryViewModel));
        }
    }
}
