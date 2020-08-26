using Sfa.Das.Sas.ApplicationServices.Models;

namespace Sfa.Das.Sas.Shared.Components.ViewModels
{
    public class TrainingProviderDetailQueryViewModel
    {
        public ViewType ViewType { get; set; }
        public int Ukprn { get; set; }
        public int Page { get; set; }
        public string ApprenticeshipId { get; set; }
        public ApprenticeshipType ApprenticeshipType { get; set; }
        public int LocationId { get; set; }
        public string PostCode { get; set; }
    }

    public enum ViewType
    {
        Details = 0,
        Contact = 1,
        Summary = 2,
        Search = 3
        
    }
}