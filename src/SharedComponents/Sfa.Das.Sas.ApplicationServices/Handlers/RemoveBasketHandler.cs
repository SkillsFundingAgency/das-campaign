using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sfa.Das.Sas.ApplicationServices.Commands;
using Sfa.Das.Sas.Shared.Basket.Interfaces;
using Sfa.Das.Sas.Shared.Basket.Models;

namespace Sfa.Das.Sas.ApplicationServices.Handlers
{
    
    public class RemoveBasketHandler : IRequestHandler<RemoveBasketCommand, Guid>
    {

        private readonly IApprenticeshipFavouritesBasketStore _basketStore;

        public RemoveBasketHandler(IApprenticeshipFavouritesBasketStore basketStore)
        {
            _basketStore = basketStore;
        }

        public async Task<Guid> Handle(RemoveBasketCommand request, CancellationToken cancellationToken)
        {
            await _basketStore.RemoveAsync(request.BasketId.Value);

            return Guid.Empty;
        }

        private async Task<ApprenticeshipFavouritesBasket> GetBasket(RemoveBasketCommand request)
        {
            if (request.BasketId.HasValue)
            {
                return await _basketStore.GetAsync(request.BasketId.Value);
            }

            return null;
        }
    }
}
