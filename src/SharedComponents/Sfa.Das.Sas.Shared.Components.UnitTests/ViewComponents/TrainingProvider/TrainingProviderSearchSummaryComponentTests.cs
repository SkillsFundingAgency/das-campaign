using FluentAssertions;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using NUnit.Framework;
using System.Threading.Tasks;
using Moq;
using Sfa.Das.Sas.ApplicationServices.Responses;
using Sfa.Das.Sas.Shared.Components.Orchestrators;
using Sfa.Das.Sas.Shared.Components.ViewComponents.ApprenticeshipDetails;
using Sfa.Das.Sas.Shared.Components.ViewComponents.Fat;
using Sfa.Das.Sas.Shared.Components.ViewComponents.TrainingProvider.Search;
using Sfa.Das.Sas.Shared.Components.ViewComponents.TrainingProvider.SearchSummary;
using Sfa.Das.Sas.Shared.Components.ViewModels;

namespace Sfa.Das.Sas.Shared.Components.UnitTests.ViewComponents.Fat
{
    [TestFixture]
    public class TrainingProviderSearchSummaryViewComponentTests : ViewComponentTestsBase
    {
        private TrainingProviderSearchSummaryViewComponent _sut;
        private string _apprenticeshipId;
        private Mock<ITrainingProviderOrchestrator> _mockTrainingProviderOrchestrator;
        private Mock<IApprenticeshipOrchestrator> _mockApprenticeshipOrchestrator;

        [SetUp]
        public void Setup()
        {
            base.Setup();

            _mockTrainingProviderOrchestrator = new Mock<ITrainingProviderOrchestrator>();
            _mockApprenticeshipOrchestrator = new Mock<IApprenticeshipOrchestrator>();

            _mockApprenticeshipOrchestrator.Setup(s => s.GetFramework(It.IsAny<string>())).ReturnsAsync(new FrameworkDetailsViewModel());
            _mockApprenticeshipOrchestrator.Setup(s => s.GetStandard(It.IsAny<string>())).ReturnsAsync(new StandardDetailsViewModel());


            _sut = new TrainingProviderSearchSummaryViewComponent(_mockTrainingProviderOrchestrator.Object,_mockApprenticeshipOrchestrator.Object);
            _sut.ViewComponentContext = _viewComponentContext;
        }

        [Test]
        public async Task When_Success_Then_Default_View_Is_Returned()
        {
            _mockTrainingProviderOrchestrator.Setup(s => s.GetSearchResults(It.IsAny<TrainingProviderSearchViewModel>()))
                .ReturnsAsync(new SearchResultsViewModel<TrainingProviderSearchResultsItem, TrainingProviderSearchViewModel>() {Status = ProviderSearchResponseCodes.Success});

            var result = await _sut.InvokeAsync(new TrainingProviderSearchViewModel()) as ViewViewComponentResult;

            result.Should().BeOfType<ViewViewComponentResult>();
            result.ViewName.Should().Be("../TrainingProvider/SearchSummary/Default");
        }

        [Test]
        public async Task When_Success_Then_TrainingProviderSearchSummaryViewModel_Is_Returned()
        {
            _mockTrainingProviderOrchestrator.Setup(s => s.GetSearchResults(It.IsAny<TrainingProviderSearchViewModel>()))
                .ReturnsAsync(new SearchResultsViewModel<TrainingProviderSearchResultsItem, TrainingProviderSearchViewModel>() { Status = ProviderSearchResponseCodes.Success });

            var result = await _sut.InvokeAsync(new TrainingProviderSearchViewModel()) as ViewViewComponentResult;

            result.Should().BeOfType<ViewViewComponentResult>();

            result.ViewData.Model.Should().BeOfType<TrainingProviderSearchSummaryViewModel>();
        }

        [Test]
        public async Task When_WalesPostcode_Then_No_Content_Is_Returned()
        {
            _mockTrainingProviderOrchestrator.Setup(s => s.GetSearchResults(It.IsAny<TrainingProviderSearchViewModel>()))
                .ReturnsAsync(new SearchResultsViewModel<TrainingProviderSearchResultsItem, TrainingProviderSearchViewModel>() { Status = ProviderSearchResponseCodes.WalesPostcode });

            var result = await _sut.InvokeAsync(new TrainingProviderSearchViewModel()) as ContentViewComponentResult;

            result.Should().BeOfType<ContentViewComponentResult>();
            result.Content.Should().BeEmpty();
        }

        [Test]
        public async Task When_ScotlandPostcode_Then_No_Content_Is_Returned()
        {
            _mockTrainingProviderOrchestrator.Setup(s => s.GetSearchResults(It.IsAny<TrainingProviderSearchViewModel>()))
                .ReturnsAsync(new SearchResultsViewModel<TrainingProviderSearchResultsItem, TrainingProviderSearchViewModel>() { Status = ProviderSearchResponseCodes.ScotlandPostcode });

            var result = await _sut.InvokeAsync(new TrainingProviderSearchViewModel()) as ContentViewComponentResult;

            result.Should().BeOfType<ContentViewComponentResult>();
            result.Content.Should().BeEmpty();
        }

        [Test]
        public async Task When_NorthernIrelandPostcode_Then_No_Content_Is_Returned()
        {
            _mockTrainingProviderOrchestrator.Setup(s => s.GetSearchResults(It.IsAny<TrainingProviderSearchViewModel>()))
                .ReturnsAsync(new SearchResultsViewModel<TrainingProviderSearchResultsItem, TrainingProviderSearchViewModel>() { Status = ProviderSearchResponseCodes.NorthernIrelandPostcode });

            var result = await _sut.InvokeAsync(new TrainingProviderSearchViewModel()) as ContentViewComponentResult;

            result.Should().BeOfType<ContentViewComponentResult>();
            result.Content.Should().BeEmpty();
        }

        [Test]
        public async Task When_PostcodeInvalidFormat_Then_No_Content_Is_Returned()
        {
            _mockTrainingProviderOrchestrator.Setup(s => s.GetSearchResults(It.IsAny<TrainingProviderSearchViewModel>()))
                .ReturnsAsync(new SearchResultsViewModel<TrainingProviderSearchResultsItem, TrainingProviderSearchViewModel>() { Status = ProviderSearchResponseCodes.PostCodeInvalidFormat });

            var result = await _sut.InvokeAsync(new TrainingProviderSearchViewModel()) as ContentViewComponentResult;

            result.Should().BeOfType<ContentViewComponentResult>();
            result.Content.Should().BeEmpty();
        }
    }
}
