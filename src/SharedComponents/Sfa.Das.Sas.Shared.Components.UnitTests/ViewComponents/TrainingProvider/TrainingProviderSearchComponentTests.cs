using FluentAssertions;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using NUnit.Framework;
using System.Threading.Tasks;
using Sfa.Das.Sas.Shared.Components.ViewComponents.Fat;
using Sfa.Das.Sas.Shared.Components.ViewComponents.TrainingProvider.Search;

namespace Sfa.Das.Sas.Shared.Components.UnitTests.ViewComponents.Fat
{
    [TestFixture]
    public class TrainingProviderSearchViewComponentTests : ViewComponentTestsBase
    {
        private TrainingProviderSearchViewComponent _sut;
        private string _apprenticeshipId;

        [SetUp]
        public void Setup()
        {
            base.Setup();

            _sut = new TrainingProviderSearchViewComponent();
            _sut.ViewComponentContext = _viewComponentContext;
        }

        [Test]
        public async Task Then_TrainingProviderSearchViewModel_Is_Returned()
        {
            _apprenticeshipId = "123";

            var result = await _sut.InvokeAsync(_apprenticeshipId) as ViewViewComponentResult;

            result.Should().BeOfType<ViewViewComponentResult>();

            result.ViewData.Model.Should().BeOfType<TrainingProviderSearchViewModel>();
        }

        [Test]
        public async Task When_ApprenticeshipId_Is_Provided_Then_Is_Returned_In_Model()
        {
            _apprenticeshipId = "123";

            var result = await _sut.InvokeAsync(_apprenticeshipId) as ViewViewComponentResult;

            result.Should().BeOfType<ViewViewComponentResult>();

            result.ViewData.Model.Should().BeOfType<TrainingProviderSearchViewModel>();
            var model = result.ViewData.Model as TrainingProviderSearchViewModel;
            model.ApprenticeshipId.Should().Be(_apprenticeshipId);
        }
      
    }
}
