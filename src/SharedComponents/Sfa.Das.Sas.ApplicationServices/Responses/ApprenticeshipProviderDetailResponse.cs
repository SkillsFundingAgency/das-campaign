namespace Sfa.Das.Sas.ApplicationServices.Responses
{
    using Core.Domain.Model;
    using SFA.DAS.Apprenticeships.Api.Types;

    public class ApprenticeshipProviderDetailResponse
    {
        public enum ResponseCodes
        {
            Success,
            ApprenticeshipProviderNotFound,
            InvalidInput
        }

        public ResponseCodes StatusCode { get; set; }

        public ApprenticeshipDetails ApprenticeshipDetails { get; set; }

        public ApprenticeshipTrainingType ApprenticeshipType { get; set; }

        public string ApprenticeshipName { get; set; }

        public string ApprenticeshipLevel { get; set; }

		public bool RegulatedApprenticeship { get; set; }
    }
}