using System;
using Sfa.Das.Sas.Shared.Components.ViewComponents.TrainingProvider.Search;

namespace Sfa.Das.Sas.Shared.Components.ViewModels.Basket
{
    public class SaveBasketFromProviderSearchViewModel
    {
        public string ItemId { get; set; }
        public int Ukprn => Int32.Parse(ItemId.Split(',')[0]);
        public int LocationId => Int32.Parse(ItemId.Split(',')[1]);
        public TrainingProviderSearchViewModel SearchQuery { get; set; }
    }
}
