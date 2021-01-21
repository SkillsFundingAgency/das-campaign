using System;
using System.Linq;
using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.Shared.Components.ViewModels.Apprenticeship;
using Sfa.Das.Sas.Shared.Components.ViewModels.Basket;

namespace Sfa.Das.Sas.Shared.Components.Mapping
{
    public class BasketViewModelMapper : IBasketViewModelMapper
    {
        private readonly IApprenticeshipBasketItemViewModelMapper _apprenticeshipItemViewModelModelMapper;

        public BasketViewModelMapper(IApprenticeshipBasketItemViewModelMapper apprenticeshipItemViewModelModelMapper)
        {
            _apprenticeshipItemViewModelModelMapper = apprenticeshipItemViewModelModelMapper;
        }

        public BasketViewModel<ApprenticeshipBasketItemViewModel> Map(ApprenticeshipFavouritesBasketRead source, Guid basketId)
        {
            var basket = new BasketViewModel<ApprenticeshipBasketItemViewModel>();

            basket.BasketId = basketId;

            basket.Items = source.Select(item => _apprenticeshipItemViewModelModelMapper.Map(item)).ToList();

            return basket;
        }
    }
}
