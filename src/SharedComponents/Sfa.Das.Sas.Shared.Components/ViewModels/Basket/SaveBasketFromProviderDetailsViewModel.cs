using System;

namespace Sfa.Das.Sas.Shared.Components.ViewModels.Basket
{
    public class SaveBasketFromProviderDetailsViewModel : TrainingProviderDetailQueryViewModel
    {
        public string ItemId { get; set; }
        public int LocationIdToAdd => Int32.Parse(ItemId.Split(',')[1]);
    }
}
