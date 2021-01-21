using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Sfa.Das.Sas.Core.Domain;
using SFA.DAS.Apprenticeships.Api.Types.Exceptions;

namespace Sfa.Das.Sas.ApplicationServices.Handlers
{
    using Core.Domain.Services;
    using MediatR;
    using Queries;
    using Responses;
    using SFA.DAS.NLog.Logger;

    public class GetStandardHandler : IRequestHandler<GetStandardQuery, GetStandardResponse>
    {
        private readonly IGetStandards _getStandards;
        private readonly IGetAssessmentOrganisations _getAssessmentOrganisations;
        private readonly ILog _logger;

        public GetStandardHandler(
            IGetStandards getStandards,
            ILog logger,
            IGetAssessmentOrganisations getAssessmentOrganisations)
        {
            _getStandards = getStandards;
            _logger = logger;
            _getAssessmentOrganisations = getAssessmentOrganisations;
        }

        public async Task<GetStandardResponse> Handle(GetStandardQuery message, CancellationToken cancellationToken)
        {
            var response = new GetStandardResponse();
            int standardId;
            int.TryParse(message.Id, out standardId);
            if (standardId < 0)
            {
                response.StatusCode = GetStandardResponse.ResponseCodes.InvalidStandardId;
                return response;
            }

            var standard = _getStandards.GetStandardById(message.Id);

            if (standard == null)
            {
                response.StatusCode = GetStandardResponse.ResponseCodes.StandardNotFound;
                return response;
            }

            if (!standard.IsActiveStandard)
            {
                response.StatusCode = GetStandardResponse.ResponseCodes.Gone;
                return response;
            }

            response.Standard = standard;
            response.SearchTerms = message.Keywords;

            try
            {
                response.AssessmentOrganisations = await  _getAssessmentOrganisations.GetByStandardId(standardId);

            }
            catch (EntityNotFoundException ex)
            {
                _logger.Warn(ex, $"{typeof(EntityNotFoundException)} when trying to get assessment org by standard id");
                response.StatusCode = GetStandardResponse.ResponseCodes.AssessmentOrgsEntityNotFound;
                response.AssessmentOrganisations = new List<AssessmentOrganisation>();

            }
            catch (HttpRequestException ex)
            {
                _logger.Warn(ex, $"{typeof(HttpRequestException)} when trying to get assessment org by standard id");
                response.StatusCode = GetStandardResponse.ResponseCodes.HttpRequestException;
            }

            return response;
        }
    }
}
