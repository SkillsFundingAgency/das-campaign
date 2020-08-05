using FluentAssertions;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using NUnit.Framework;
using Sfa.Das.Sas.Shared.Components.ViewComponents;
using System.Threading.Tasks;
using Sfa.Das.Sas.Shared.Components.ViewComponents.Fat;
using Sfa.Das.Sas.Shared.Components.ViewModels;

namespace Sfa.Das.Sas.Shared.Components.UnitTests.ViewComponents.Fat
{
    [TestFixture]
    public class FatSearchViewComponentTests : ViewComponentTestsBase
    {
        private FatSearchViewComponent _sut;
        private readonly SearchQueryViewModel _searchQueryViewModel = new SearchQueryViewModel();

        [SetUp]
        public void Setup()
        {
            base.Setup();

            _sut = new FatSearchViewComponent();
            _sut.ViewComponentContext = _viewComponentContext;
        }

        [Test]
        public async Task When_Inline_Option_Is_Not_Provided_Then_Return_Default_View()
        {
            var result = await _sut.InvokeAsync(_searchQueryViewModel) as ViewViewComponentResult;

            result.Should().BeOfType<ViewViewComponentResult>();

            result.ViewName.Should().Be("Default");
        }
        [Test]
        public async Task When_Inline_Option_Is_Provided_And_False_Then_Return_Default_View()
        {
            var result = await _sut.InvokeAsync(_searchQueryViewModel, inline: false) as ViewViewComponentResult;

            result.Should().BeOfType<ViewViewComponentResult>();

            result.ViewName.Should().Be("Default");
        }

        [Test]
        public async Task When_Inline_Option_Is_Provided_And_True_Then_Return_Inline_View()
        {
            var result = await _sut.InvokeAsync(_searchQueryViewModel, inline: true) as ViewViewComponentResult;

            result.Should().BeOfType<ViewViewComponentResult>();

            result.ViewName.Should().Be("Inline");
        }

        [Test]
        public async Task Then_FatSearchViewModel_Is_Returned()
        {
            _searchQueryViewModel.Keywords = "keyword";

            var result = await _sut.InvokeAsync(_searchQueryViewModel) as ViewViewComponentResult;

            result.Should().BeOfType<ViewViewComponentResult>();

            result.ViewData.Model.Should().BeOfType<FatSearchViewModel>();
        }

        [Test]
        public async Task When_Keyword_Is_Provided_Then_Is_Returned_In_Model()
        {
            _searchQueryViewModel.Keywords = "keyword";

            var result = await _sut.InvokeAsync(_searchQueryViewModel) as ViewViewComponentResult;

            result.Should().BeOfType<ViewViewComponentResult>();

            result.ViewData.Model.Should().BeOfType<FatSearchViewModel>();
            var model = result.ViewData.Model as FatSearchViewModel;
            model.Keywords.Should().Be(_searchQueryViewModel.Keywords);
        }
      
    }
}
