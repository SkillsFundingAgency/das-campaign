using System.Collections.Generic;

namespace Sfa.Das.Sas.ApplicationServices.Models
{
    public sealed class GroupedProviderSearchResults
    {
        public IEnumerable<GroupedProviderSearchResultItem> Hits { get; set; }

        public bool HasNationalProviders { get; set; }

        public long TotalResults { get; set; }

        public int ResultsToTake { get; set; }

        public int ActualPage { get; set; }

        public int LastPage { get; set; }

        public string PostCode { get; set; }

        public bool PostCodeMissing { get; set; }

        public bool ShowNationalProvidersOnly { get; set; }

        public string ResponseCode { get; set; }
    }
}