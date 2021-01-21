using System.Linq;
using Sfa.Das.FatApi.Client.Model;

namespace Sfa.Das.Sas.Infrastructure.Mapping
{
    using Sfa.Das.FatApi.Client.Model.V4;
    using Sfa.Das.Sas.ApplicationServices.Models;

    public class SearchResultsMapping : ISearchResultsMapping
    {
        private readonly IProviderSearchResultsMapper _providerSearchResultsMapper;

        public SearchResultsMapping( IProviderSearchResultsMapper providerSearchResultsMapper)
        {
            _providerSearchResultsMapper = providerSearchResultsMapper;
        }

        public ProviderSearchResult<ProviderSearchResultItem> Map(SFADASApprenticeshipsApiTypesV3ProviderApprenticeshipLocationSearchResult document)
        {
            if (document == null)
            {
                return null;
            }

            var result = new ProviderSearchResult<ProviderSearchResultItem>();
            result.Total = document.TotalResults;
            result.Hits = document.Results?.Select(s => _providerSearchResultsMapper.Map(s));

            result.NationalProvidersAggregation = document.NationalProvidersAggregation.ToDictionary(s => s.Key, s => s.Value as long?);
            result.TrainingOptionsAggregation = document.TrainingOptionsAggregation.ToDictionary(s => s.Key, s => s.Value as long?);

            return result;
        }

        public GroupedProviderSearchResult<GroupedProviderSearchResultItem> Map(V4GroupedProviderSearchResults document)
        {
            if (document == null)
            {
                return null;
            }

            var result = new GroupedProviderSearchResult<GroupedProviderSearchResultItem>();
            result.Total = document.TotalResults;
            result.Hits = document.Results?.Select(s => _providerSearchResultsMapper.Map(s));

            result.HasNationalProviders = document.HasNationalProviders;

            return result;
        }

        public ProviderLocationsSearchResult Map(V4ClosestLocationsSearchResults document)
        {
            if (document == null)
            {
                return null;
            }

            var result = new ProviderLocationsSearchResult();
            result.Total = document.TotalResults;
            result.Hits = document.Results.Select(x => _providerSearchResultsMapper.Map(x));
            result.ProviderName = document.ProviderName;

            return result;
        }
    }
}