namespace Sfa.Das.Sas.Infrastructure.Mapping
{
    using Sfa.Das.FatApi.Client.Model;
    using Sfa.Das.FatApi.Client.Model.V4;
    using Sfa.Das.Sas.ApplicationServices.Models;

    public interface ISearchResultsMapping
    {
        ProviderSearchResult<ProviderSearchResultItem> Map(SFADASApprenticeshipsApiTypesV3ProviderApprenticeshipLocationSearchResult document);
        GroupedProviderSearchResult<GroupedProviderSearchResultItem> Map(V4GroupedProviderSearchResults document);
        ProviderLocationsSearchResult Map(V4ClosestLocationsSearchResults result);
    }
}