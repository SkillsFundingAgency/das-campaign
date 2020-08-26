using System.Collections.Generic;
using Sfa.Das.Sas.ApplicationServices.Responses;
using Sfa.Das.Sas.Core.Domain.Model;

namespace Sfa.Das.Sas.ApplicationServices.Models
{
    public class ProviderNameSearchResultsAndPagination
    {
        public long TotalResults { get; set; }

        public int ResultsToTake { get; set; }

        public int ActualPage { get; set; }

        public int LastPage { get; set; }

        public string SearchTerm { get; set; }

        public bool HasError { get; set; }

        public IEnumerable<ProviderNameSearchResult> Results { get; set; }

        public ProviderNameSearchResponseCodes ResponseCode { get; set; }
    }
}
