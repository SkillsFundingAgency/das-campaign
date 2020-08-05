using System.Collections.Generic;
using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.Core.Domain.Model;
using Sfa.Das.Sas.Shared.Components.Extensions.Domain;
using Sfa.Das.Sas.Shared.Components.ViewComponents.Fat;

namespace Sfa.Das.Sas.Shared.Components.Mapping
{
    public class TrainingProviderSearchResultsItemViewModelMapper : ITrainingProviderSearchResultsItemViewModelMapper
    {

        public TrainingProviderSearchResultsItem Map(GroupedProviderSearchResultItem source)
        {
            var item = new TrainingProviderSearchResultsItem()
            {
                Ukprn = source.Ukprn,
                Distance = source.Distance,
                EmployerSatisfaction = source.EmployerSatisfaction,
                LearnerSatisfaction = source.LearnerSatisfaction,
                NationalProvider = source.NationalProvider,
                OverallAchievementRate = source.OverallAchievementRate,
                Name = source.ProviderName,
                LocationId = source.LocationId,
                HasOtherLocations = source.HasOtherMatchingLocations,
                LocationAddress = GetAddress(source.Address)
            };

            return item;
        }

        private static LocationAddress GetAddress(Address address)
        {
            return new LocationAddress
            {
                AddressWithoutPostCode = address.GetCommaDelimitedAddress(),
                PostCode = address.Postcode
            };
        }
    }
}