using System.Collections.Generic;

namespace Sfa.Das.Sas.ApplicationServices.Models
{
    public class ProviderSearchResults : BaseProviderSearchResults
    {
        public string ApprenticeshipId { get; set; }
        public int Level { get; internal set; }
        public string Title { get; set; }
        public string ResponseCode { get; set; }
        public new IEnumerable<ProviderSearchResultItem> Hits { get; set; }
    }
}