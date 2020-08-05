using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.ApplicationServices.Responses;
using Sfa.Das.Sas.Shared.Components.Mapping;
using Sfa.Das.Sas.Shared.Components.ViewComponents.Fat;
using Sfa.Das.Sas.Shared.Components.ViewComponents.TrainingProvider;
using Sfa.Das.Sas.Shared.Components.ViewComponents.TrainingProvider.Search;
using Sfa.Das.Sas.Shared.Components.ViewModels;

namespace Sfa.Das.Sas.Shared.Components.UnitTests.Mapping.TrainingProvider
{
    [TestFixture]
    public class GivenFatSearchFilterIsMappedToViewModel
    {
        private FatSearchFilterViewModelMapper _sut;
        private ApprenticeshipSearchResults _resultsItemToMap;
        private SearchQueryViewModel _queryItemToMap;

        [SetUp]
        public void Setup()
        {

            _sut = new FatSearchFilterViewModelMapper();

            _resultsItemToMap = new ApprenticeshipSearchResults()
            {
                ActualPage = 1,
                LastPage = 20,
                SearchTerm = "Apprenticeship",
                ResultsToTake = 20,
                TotalResults = 400,
                SortOrder = "1",
                Results = new List<ApprenticeshipSearchResultsItem>()
                {
                    new ApprenticeshipSearchResultsItem(){Title = "apprenticeship"},
                    new ApprenticeshipSearchResultsItem(){Title = "apprenticeship"}
                },
                LevelAggregation = new Dictionary<int, long?>() { { 3, 20 }, { 4, 10 }, { 5, 40 } },
                SelectedLevels = new List<int>() { 4, 5 }
            };
            _queryItemToMap = new SearchQueryViewModel()
            {
                Keywords = "Apprenticeship",
                Page = 1,
                ResultsToTake = 20,
                SortOrder = 1,
                SelectedLevels = new List<int>() { 4, 5 }
            };
        }

        [Test]
        public void When_Mapping_Then_ApprenticeshipSearchFilterViewModel_Is_Returned()
        {
            var result = _sut.Map(_resultsItemToMap, _queryItemToMap);

            result.Should().BeOfType<FatSearchFilterViewModel>();
            result.Should().NotBeNull();
        }

        [Test]
        public void When_Mapping_Then_Items_Are_Mapped()
        {
            var result = _sut.Map(_resultsItemToMap, _queryItemToMap);
            result.Page.Should().Be(_queryItemToMap.Page);
            result.ResultsToTake.Should().Be(_queryItemToMap.ResultsToTake);
            result.LevelsAggregate.Should().NotBeNull();
            result.LevelsAggregate.Count.Should().Be(3);

            var levelAggregate = result.LevelsAggregate.FirstOrDefault(w => w.Value == "3");
            levelAggregate.Value.Should().Be("3");
            levelAggregate.Count.Should().Be(20);
            levelAggregate.Checked.Should().BeFalse();

        }

        [Test]
        public void When_Mapping_And_Results_Are_Null_Then_Items_Mapped_As_Null()
        {
            _resultsItemToMap.Results = null;

            var result = _sut.Map(_resultsItemToMap, _queryItemToMap);

            result.Page.Should().Be(_queryItemToMap.Page);
            result.ResultsToTake.Should().Be(_queryItemToMap.ResultsToTake);
        }
    }
}
