using MediatR;
using Sfa.Das.Sas.ApplicationServices.Responses;

namespace Sfa.Das.Sas.ApplicationServices.Queries
{
    public sealed class ValidatePostcodeQuery : IRequest<bool>
    {
        public string Postcode { get; set; }
    }
}