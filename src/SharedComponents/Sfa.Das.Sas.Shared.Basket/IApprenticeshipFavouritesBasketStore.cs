using System;
using System.Threading.Tasks;
using Sfa.Das.Sas.Shared.Basket.Models;

namespace Sfa.Das.Sas.Shared.Basket.Interfaces
{
    public interface IApprenticeshipFavouritesBasketStore
    {
        Task<ApprenticeshipFavouritesBasket> GetAsync(Guid basketId);
        Task UpdateAsync(ApprenticeshipFavouritesBasket basket);
        Task RemoveAsync(Guid basketId);
    }
}
