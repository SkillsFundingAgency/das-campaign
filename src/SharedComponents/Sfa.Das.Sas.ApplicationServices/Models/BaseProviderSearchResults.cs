namespace Sfa.Das.Sas.ApplicationServices.Models
{
    using System.Collections.Generic;

    public class BaseProviderSearchResults
    {
        public long TotalResults { get; set; }

        public int ResultsToTake { get; set; }

        public int ActualPage { get; set; }

        public int LastPage { get; set; }

        public string PostCode { get; set; }

        public virtual IEnumerable<IApprenticeshipProviderSearchResultsItem> Hits { get; set; }

        public bool PostCodeMissing { get; set; }

        public IEnumerable<string> SelectedTrainingOptions { get; set; }

        public Dictionary<string, long?> TrainingOptionsAggregation { get; set; }

        public bool ShowNationalProvidersOnly { get; set; }

        public Dictionary<string, long?> NationalProviders { get; set; }
    }
}