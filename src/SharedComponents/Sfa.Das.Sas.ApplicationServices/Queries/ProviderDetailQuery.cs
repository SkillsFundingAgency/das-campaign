using MediatR;
using Sfa.Das.Sas.ApplicationServices.Responses;

namespace Sfa.Das.Sas.ApplicationServices.Queries
{
    public sealed class ProviderDetailQuery : IRequest<ProviderDetailResponse>
    {
        public long UkPrn { get; set; }
        public int Page { get; set; }
    }
}
