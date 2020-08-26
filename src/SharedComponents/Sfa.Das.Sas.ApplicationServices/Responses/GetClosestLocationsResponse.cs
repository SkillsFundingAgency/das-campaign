using Sfa.Das.Sas.ApplicationServices.Models;

namespace Sfa.Das.Sas.ApplicationServices.Responses
{

    public sealed class GetClosestLocationsResponse
    {
        public SearchResult<CloseTrainingLocation> Results { get;  set; }
        public string ProviderName { get; set; }
    }
}
