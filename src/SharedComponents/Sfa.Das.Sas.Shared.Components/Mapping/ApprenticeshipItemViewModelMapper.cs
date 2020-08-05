using System.Linq;
using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.Shared.Components.ViewComponents.Fat;
using Sfa.Das.Sas.Shared.Components.ViewModels.Apprenticeship;

namespace Sfa.Das.Sas.Shared.Components.Mapping
{
    public class ApprenticeshipItemViewModelMapper : IApprenticeshipItemViewModelMapper
    {
        public ApprenticeshipItemViewModel Map(ApprenticeshipSearchResultsItem source)
        {
            var item = new ApprenticeshipItemViewModel()
            {
                Id = source.Id,
                Title = source.Title,
                Level = source.Level,
                Duration = source.Duration,
                EffectiveTo = source.EffectiveTo,
                ApprenticeshipType = source.ApprenticeshipType
            };
            return item;
        }

    }
}
