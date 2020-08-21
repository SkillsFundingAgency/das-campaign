using MediatR;
using Sfa.Das.Sas.ApplicationServices.Responses;

namespace Sfa.Das.Sas.ApplicationServices.Queries
{
    public class GroupedProviderSearchQuery : IRequest<GroupedProviderSearchResponse>
    {
        private string _postcode;

        public string PostCode
        {
            get { return _postcode; }
            set { _postcode = value?.Trim(); }
        }

        public bool IsLevyPayingEmployer { get; set; }

        public int Page { get; set; }

        public int Take { get; set; } = 20;

        public bool NationalProvidersOnly { get; set; }

        public bool ShowAll { get; set; }

        public string Keywords { get; set; }

        public string ApprenticeshipId { get; set; }

        public string Ukprn { get; set; }
    }
}
