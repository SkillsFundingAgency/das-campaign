using System;
using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.Shared.Components.ViewComponents.Fat;
using Sfa.Das.Sas.Shared.Components.ViewModels;

namespace Sfa.Das.Sas.Shared.Components.Mapping
{
    public class FatSearchFilterViewModelMapper : IFatSearchFilterViewModelMapper
    {
        public FatSearchFilterViewModel Map(ApprenticeshipSearchResults source, SearchQueryViewModel searchQueryModel)
        {
            var item = new FatSearchFilterViewModel();
            
            foreach (var level in source.LevelAggregation)
            {
                var isChecked = searchQueryModel.SelectedLevels.Contains(level.Key);

                item.LevelsAggregate.Add(new LevelAggregationViewModel()
                {
                    Checked = isChecked,
                    Count = level.Value ?? level.Value.Value,
                    Value = level.Key.ToString()
                });
            }


            return item;
        }
    }
}
