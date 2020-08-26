using System;
using System.Collections.Generic;
using Sfa.Das.Sas.ApplicationServices.Commands;

namespace Sfa.Das.Sas.Shared.Components.ViewModels.Basket
{
    public class BasketViewModel<T> where T : class
    {
        public Guid? BasketId { get; set; }

        public IList<T> Items { get; set; }

        public bool HasItems => Items?.Count > 0;
        
        public AddOrRemoveFavouriteInBasketResponse AddRemoveBasketResponse { get; set; }
    }
}