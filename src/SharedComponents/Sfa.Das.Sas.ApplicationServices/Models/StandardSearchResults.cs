using System.Collections.Generic;

namespace Sfa.Das.Sas.ApplicationServices.Models
{
    public sealed class StandardSearchResults
    {
        public long TotalResults { get; set; }

        public string SearchTerm { get; set; }

        public IEnumerable<StandardSearchResultsItem> Results { get; set; }

        public bool HasError { get; set; }
    }
}