using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sfa.Das.Sas.ApplicationServices.Queries;
using Sfa.Das.Sas.ApplicationServices.Responses;
using Sfa.Das.Sas.Core.Domain.Services;

namespace Sfa.Das.Sas.ApplicationServices.Handlers
{
    public class GetFrameworkProvidersHandler : IRequestHandler<GetFrameworkProvidersQuery, GetFrameworkProvidersResponse>
    {
        private readonly IGetFrameworks _getFrameworks;

        public GetFrameworkProvidersHandler(IGetFrameworks getFrameworks)
        {
            _getFrameworks = getFrameworks;
        }

        public async Task<GetFrameworkProvidersResponse> Handle(GetFrameworkProvidersQuery message, CancellationToken cancellationToken)
        {
            var framework = _getFrameworks.GetFrameworkById(message.FrameworkId);

            if (framework == null)
            {
                return new GetFrameworkProvidersResponse
                {
                    StatusCode = GetFrameworkProvidersResponse.ResponseCodes.NoFrameworkFound
                };
            }

            return new GetFrameworkProvidersResponse
            {
                FrameworkId = framework.FrameworkId,
                Title = framework.Title,
                Keywords = message.Keywords,
                Postcode = message.Postcode,
                Level = framework.Level
            };
        }
    }
}
