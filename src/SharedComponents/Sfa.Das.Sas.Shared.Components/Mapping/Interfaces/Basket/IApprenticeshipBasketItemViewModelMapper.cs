using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.Shared.Components.ViewModels.Apprenticeship;

namespace Sfa.Das.Sas.Shared.Components.Mapping
{
    public interface IApprenticeshipBasketItemViewModelMapper
    {
        ApprenticeshipBasketItemViewModel Map(ApprenticeshipFavouriteRead source);
    }
}