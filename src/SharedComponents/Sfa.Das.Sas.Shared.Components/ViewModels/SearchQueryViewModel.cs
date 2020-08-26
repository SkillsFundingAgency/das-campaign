using System.Collections.Generic;
using Sfa.Das.Sas.ApplicationServices.Commands;

namespace Sfa.Das.Sas.Shared.Components.ViewModels
{
    public class SearchQueryViewModel
    {
        public string Keywords { get; set; }
        public int ResultsToTake { get; set; } = 20;
        public int Page { get; set; } = 1;
        public int SortOrder { get; set; } = 0;

        public List<int> SelectedLevels { get; set; } = new List<int>() { 2, 3, 4, 5, 6, 7, 8};
        
        public AddOrRemoveFavouriteInBasketResponse AddRemoveBasketResponse { get; set; }
    }
}