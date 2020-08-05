using System.Collections.Generic;

namespace Sfa.Das.Sas.Shared.Components.ViewModels
{
    public class ClosestLocationsViewModel
    {
        public ClosestLocationsViewModel()
        {
            Locations = new List<CloseLocationViewModel>();
        }
        public string ProviderName { get; set; }

        public int LocationId { get; set; }


        public string ApprenticeshipId { get; set; }

        public int Ukprn { get; set; }

        public string PostCode { get; set; }

        public IList<CloseLocationViewModel> Locations { get; set; }
    }
}