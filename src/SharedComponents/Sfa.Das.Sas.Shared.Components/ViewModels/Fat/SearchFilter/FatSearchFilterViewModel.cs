using System.Collections.Generic;

namespace Sfa.Das.Sas.Shared.Components.ViewModels.Fat.SearchFilter
{
    public class FatSearchFilterViewModel : SearchQueryViewModel
    {
        public FatSearchFilterViewModel()
        {
            LevelsAggregate = new List<LevelAggregationViewModel>();
        }
        public IList<LevelAggregationViewModel> LevelsAggregate { get; set; }
    }   
}
