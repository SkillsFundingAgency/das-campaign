using Sfa.Das.Sas.ApplicationServices.Responses;
using Sfa.Das.Sas.Core.Domain.Model;
using Sfa.Das.Sas.Shared.Components.Extensions.Domain;
using Sfa.Das.Sas.Shared.Components.ViewModels;
using System.Linq;

namespace Sfa.Das.Sas.Shared.Components.Mapping
{
    public class TrainingProviderClosestLocationsViewModelMapper : ITrainingProviderClosestLocationsViewModelMapper
    {
        public ClosestLocationsViewModel Map(string apprenticeshipId, int ukprn, int locationId, string postcode, GetClosestLocationsResponse source)
        {
            var model = new ClosestLocationsViewModel
            {
                ApprenticeshipId = apprenticeshipId,
                ProviderName = source.ProviderName,
                Ukprn = ukprn,
                LocationId = locationId,
                PostCode = postcode,
                Locations = source.Results.Hits.Select(x => new CloseLocationViewModel { LocationId = x.LocationId, Distance = x.Distance, PostCode = x.Address.Postcode, AddressWithoutPostCode = x.Address.GetCommaDelimitedAddress() }).ToList()
            };

            return model;
        }
    }
}
