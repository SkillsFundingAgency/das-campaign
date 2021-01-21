using System;
using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.Shared.Components.ViewModels.Apprenticeship;
using Sfa.Das.Sas.Shared.Components.ViewModels.Basket;

namespace Sfa.Das.Sas.Shared.Components.Mapping
{
    public interface IBasketViewModelMapper
    {
        BasketViewModel<ApprenticeshipBasketItemViewModel> Map(ApprenticeshipFavouritesBasketRead item, Guid basketId);
    }
}
