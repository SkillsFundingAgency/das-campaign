using System.Linq;
using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.Shared.Components.ViewComponents.Fat;
using Sfa.Das.Sas.Shared.Components.ViewModels.Apprenticeship;

namespace Sfa.Das.Sas.Shared.Components.Mapping
{
    public class ApprenticeshipBasketItemViewModelMapper : IApprenticeshipBasketItemViewModelMapper
    {
        public ApprenticeshipBasketItemViewModel Map(ApprenticeshipFavouriteRead source)
        {
            var item = new ApprenticeshipBasketItemViewModel()
            {
                Id = source.ApprenticeshipId,
                Title = source.Title,
                Level = source.Level,
                Duration = source.Duration,
                EffectiveTo = source.EffectiveTo,
                TrainingProvider = source.Providers.Select(s => new TrainingProviderSearchResultsItem(){Ukprn = s.Ukprn, Name = s.Name, Active = s.Active, LocationId = s.Locations.FirstOrDefault() }).ToList(),
                ApprenticeshipType = source.ApprenticeshipType,
                Active = source.Active
            };
            return item;
        }
    }
}