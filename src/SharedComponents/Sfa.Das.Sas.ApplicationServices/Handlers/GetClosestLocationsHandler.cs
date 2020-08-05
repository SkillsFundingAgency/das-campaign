using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Sfa.Das.Sas.ApplicationServices.Queries;
using Sfa.Das.Sas.ApplicationServices.Responses;
using Sfa.Das.Sas.Core.Domain.Model;

namespace Sfa.Das.Sas.ApplicationServices.Handlers
{
    public class GetClosestLocationsHandler : IRequestHandler<GetClosestLocationsQuery, GetClosestLocationsResponse>
    {
        private readonly ILogger<GetClosestLocationsHandler> _logger;
        private readonly IProviderSearchProvider _searchProvider;
        private readonly ILookupLocations _postCodeLookup;
        private readonly IValidator<GetClosestLocationsQuery> _requestValidator;

        public GetClosestLocationsHandler(
            ILogger<GetClosestLocationsHandler> logger,
            IValidator<GetClosestLocationsQuery> requestValidator,
            IProviderSearchProvider getProvider,
            ILookupLocations postCodeLookup)
        {
            _logger = logger;
            _searchProvider = getProvider;
            _postCodeLookup = postCodeLookup;
            _requestValidator = requestValidator;
        }

        public async Task<GetClosestLocationsResponse> Handle(GetClosestLocationsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Getting closest locations for {request.Ukprn}, {request.ApprenticeshipId}, {request.PostCode}");

            _requestValidator.ValidateAndThrow(request);

            var coordinateResponse = await _postCodeLookup.GetLatLongFromPostCode(request.PostCode);

            //LocationLookupResponse.
            var coordinates = coordinateResponse.Coordinate;

            var result = await _searchProvider.GetClosestLocations(request.ApprenticeshipId, request.Ukprn, coordinates);

            return new GetClosestLocationsResponse
            {
                ProviderName = result.ProviderName,
                Results = result
            };
        }
    }
}
