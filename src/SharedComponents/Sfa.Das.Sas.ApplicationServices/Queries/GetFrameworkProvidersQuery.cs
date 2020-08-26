using MediatR;
using Sfa.Das.Sas.ApplicationServices.Responses;

namespace Sfa.Das.Sas.ApplicationServices.Queries
{
    public class GetFrameworkProvidersQuery : IRequest<GetFrameworkProvidersResponse>
    {
        public string FrameworkId { get; set; }
        public string Postcode { get; set; }
        public string Keywords { get; set; }
    }
}
