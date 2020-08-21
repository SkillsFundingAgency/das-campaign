using System;
using MediatR;

namespace Sfa.Das.Sas.ApplicationServices.Commands
{
    public class AddOrRemoveFavouriteInBasketCommand : IRequest<AddOrRemoveFavouriteInBasketResponse>
    {
        public Guid? BasketId { get; set; }
        public string ApprenticeshipId { get; set; }
        public int? Ukprn { get; set; }
        public int? LocationId { get; set; }
    }

    public class AddOrRemoveFavouriteInBasketResponse
    {
        public Guid BasketId { get; set; }
        public string ApprenticeshipName { get; set; }
        public BasketOperation BasketOperation { get; set; }
    }

    public enum BasketOperation
    {
        Added,
        Removed
    }
}
