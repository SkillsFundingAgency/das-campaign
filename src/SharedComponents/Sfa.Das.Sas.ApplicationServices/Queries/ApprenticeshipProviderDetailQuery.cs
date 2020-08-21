using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.ApplicationServices.Responses;

namespace Sfa.Das.Sas.ApplicationServices.Queries
{
    using MediatR;

    public class ApprenticeshipProviderDetailQuery : IRequest<ApprenticeshipProviderDetailResponse>
    {
        public int UkPrn { get; set; }

        public int LocationId { get; set; }

        public string StandardCode { get; set; }

        public string FrameworkId { get; set; }

        public bool IsLevyPayingEmployer { get; set; }
        public string Keyword { get; set; }
        public string Postcode { get; set; }
        public string ApprenticeshipId { get; set; }
        public ApprenticeshipType ApprenticeshipType { get; set; }
    }
}