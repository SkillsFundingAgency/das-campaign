
using SFA.DAS.Apprenticeships.Api.Types.Providers;

namespace Sfa.Das.Sas.ApplicationServices.Responses
{
    public class ProviderDetailResponse
    {
        public enum ResponseCodes
        {
            Success,
            ProviderNotFound,
            HttpRequestException
        }

        public ResponseCodes StatusCode { get; set; }
        public Provider Provider { get; set; }
        public ApprenticeshipTrainingSummary ApprenticeshipTrainingSummary { get; set; }
    }
}
