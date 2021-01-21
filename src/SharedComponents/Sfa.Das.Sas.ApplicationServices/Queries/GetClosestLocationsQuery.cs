using MediatR;
using Sfa.Das.Sas.ApplicationServices.Responses;

namespace Sfa.Das.Sas.ApplicationServices.Queries
{
    public class GetClosestLocationsQuery : IRequest<GetClosestLocationsResponse>
    {
        private string _postcode;

        public string PostCode
        {
            get { return _postcode; }
            set { _postcode = value?.Trim(); }
        }

        public string ApprenticeshipId { get; set; }

        public int Ukprn { get; set; }
    }
}
