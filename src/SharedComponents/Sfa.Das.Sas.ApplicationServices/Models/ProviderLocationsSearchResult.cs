using System.Collections.Generic;

namespace Sfa.Das.Sas.ApplicationServices.Models
{

    public class ProviderLocationsSearchResult : SearchResult<CloseTrainingLocation>
    {
        public string ProviderName { get; set; }
    }
}
