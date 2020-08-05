using Sfa.Das.Sas.ApplicationServices.Responses;
using Sfa.Das.Sas.Shared.Components.ViewComponents.TrainingProvider.Search;
using Sfa.Das.Sas.Shared.Components.ViewComponents.TrainingProvider;

namespace Sfa.Das.Sas.Shared.Components.Mapping
{
    public class TrainingProviderSearchFilterViewModelMapper : ITrainingProviderSearchFilterViewModelMapper
    {
        public TrainingProviderSearchFilterViewModel Map(GroupedProviderSearchResponse item, TrainingProviderSearchViewModel searchQueryModel)
        {
            var result = new TrainingProviderSearchFilterViewModel();

            result.ApprenticeshipId = searchQueryModel.ApprenticeshipId;
            result.Keywords = searchQueryModel.Keywords;
            result.SortOrder = searchQueryModel.SortOrder;
            result.Postcode = searchQueryModel.Postcode;
            result.Status = item.StatusCode;
            result.HasNationalProviders = item.Results?.HasNationalProviders ?? false;
            result.NationalProvidersOnly = searchQueryModel.NationalProvidersOnly;

            return result;
        }
    }
}
