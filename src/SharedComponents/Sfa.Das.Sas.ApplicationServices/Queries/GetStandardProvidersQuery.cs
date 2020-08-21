using MediatR;
using Sfa.Das.Sas.ApplicationServices.Responses;

namespace Sfa.Das.Sas.ApplicationServices.Queries
{
    public class GetStandardProvidersQuery : IRequest<GetStandardProvidersResponse>
    {
        public string StandardId { get; set; }
        public string Postcode { get; set; }
        public string Keywords { get; set; }
    }
}
