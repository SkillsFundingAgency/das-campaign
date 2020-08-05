using System.Collections.Generic;
using Sfa.Das.Sas.Shared.Components.ViewModels;

namespace Sfa.Das.Sas.Shared.Components.ViewComponents.Fat
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
