using Sfa.Das.Sas.ApplicationServices.Responses;
using Sfa.Das.Sas.Shared.Components.ViewModels;

namespace Sfa.Das.Sas.Shared.Components.Mapping
{
    public interface ITrainingProviderClosestLocationsViewModelMapper
    {
        ClosestLocationsViewModel Map(string apprenticeshipId, int ukprn, int locationId, string postcode, GetClosestLocationsResponse source);
    }
}
