using MediatR;
using Sfa.Das.Sas.ApplicationServices.Responses;

namespace Sfa.Das.Sas.ApplicationServices.Queries
{
    public class GetStandardQuery : IRequest<GetStandardResponse>
    {
        public string Id { get; set; }

        public string Keywords { get; set; }
    }
}
