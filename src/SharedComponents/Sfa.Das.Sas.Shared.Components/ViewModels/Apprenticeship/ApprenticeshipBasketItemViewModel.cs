using System;
using System.Collections.Generic;
using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.Resources;
using Sfa.Das.Sas.Shared.Components.ViewComponents.Fat;

namespace Sfa.Das.Sas.Shared.Components.ViewModels.Apprenticeship
{
    public class ApprenticeshipBasketItemViewModel : ApprenticeshipItemViewModel
    {
        public IList<TrainingProviderSearchResultsItem> TrainingProvider { get; set; }
        public bool Active { get; set; }
    }
}
