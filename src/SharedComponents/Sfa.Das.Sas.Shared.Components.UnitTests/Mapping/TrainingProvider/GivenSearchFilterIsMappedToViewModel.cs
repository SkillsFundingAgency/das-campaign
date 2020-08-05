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

namespace Sfa.Das.Sas.Shared.Components.UnitTests.Mapping.TrainingProvider
{
    [TestFixture]
    public class GivenSearchFilterIsMappedToViewModel
    {
        private TrainingProviderSearchFilterViewModelMapper _sut;
        private GroupedProviderSearchResponse _resultsItemToMap;
        private TrainingProviderSearchViewModel _queryItemToMap;

        [SetUp]
        public void Setup()
        {

            _sut = new TrainingProviderSearchFilterViewModelMapper();

            _resultsItemToMap = new GroupedProviderSearchResponse()
            {
                CurrentPage = 1,
                SearchTerms = "Terms",
                ShowAllProviders = true,
                ShowOnlyNationalProviders = true,
                StatusCode = ProviderSearchResponseCodes.Success,
                Results = new GroupedProviderSearchResults()
                {
                    ActualPage = 1,
                    Hits = new List<GroupedProviderSearchResultItem>()
                    {

                    },
                    PostCode = "Postcode",
                    PostCodeMissing = false,
                    ResultsToTake = 10,
                    HasNationalProviders = true
                }
            };
            _queryItemToMap = new TrainingProviderSearchViewModel()
            {
                ApprenticeshipId = "157",
                IsLevyPayer = false,
                Keywords = "words",
                NationalProvidersOnly = false,
                Page = 1,
                Postcode = "Postcode",
                ResultsToTake = 20,
                SortOrder = 1
            };
        }

        [Test]
        public void When_Mapping_Then_TrainingProviderSearchFilterViewModel_Is_Returned()
        {
            var result = _sut.Map(_resultsItemToMap, _queryItemToMap);

            result.Should().BeOfType<TrainingProviderSearchFilterViewModel>();
            result.Should().NotBeNull();
        }

        [Test]
        public void When_Mapping_Then_Items_Are_Mapped()
        {
            var result = _sut.Map(_resultsItemToMap, _queryItemToMap);
            result.ApprenticeshipId.Should().Be(_queryItemToMap.ApprenticeshipId);
            result.Page.Should().Be(_queryItemToMap.Page);
            result.Keywords.Should().Be(_queryItemToMap.Keywords);
            result.ResultsToTake.Should().Be(_queryItemToMap.ResultsToTake);
            result.SortOrder.Should().Be(_queryItemToMap.SortOrder);
            result.IsLevyPayer.Should().Be(_queryItemToMap.IsLevyPayer);
            result.Postcode.Should().Be(_queryItemToMap.Postcode);
            result.NationalProvidersOnly.Should().Be(_queryItemToMap.NationalProvidersOnly);
            result.HasNationalProviders.Should().Be(_resultsItemToMap.Results.HasNationalProviders);
            result.NationalProvidersOnly.Should().Be(_queryItemToMap.NationalProvidersOnly);
            result.Status.Should().Be(_resultsItemToMap.StatusCode);
        }

        [Test]
        public void When_Mapping_And_Results_Are_Null_Then_Items_Mapped_As_Null()
        {
            _resultsItemToMap.Results = null;

            var result = _sut.Map(_resultsItemToMap, _queryItemToMap);

            result.Page.Should().Be(_queryItemToMap.Page);
            result.Keywords.Should().Be(_queryItemToMap.Keywords);
            result.ResultsToTake.Should().Be(_queryItemToMap.ResultsToTake);
            result.HasNationalProviders.Should().Be(false);
        }
    }
}
