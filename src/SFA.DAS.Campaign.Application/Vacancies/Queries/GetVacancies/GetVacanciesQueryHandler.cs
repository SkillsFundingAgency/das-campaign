using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.Campaign.Domain.Api.Interfaces;
using SFA.DAS.Campaign.Infrastructure.Api.Requests;
using SFA.DAS.Campaign.Infrastructure.Api.Responses;

namespace SFA.DAS.Campaign.Application.Vacancies.Queries.GetVacancies
{
    public class GetVacanciesQueryHandler : IRequestHandler<GetVacanciesQuery, GetVacanciesQueryResult>
    {
        private readonly IApiClient _apiClient;

        public GetVacanciesQueryHandler (IApiClient apiClient)
        {
            _apiClient = apiClient;
        }
            
        public async Task<GetVacanciesQueryResult> Handle(GetVacanciesQuery request, CancellationToken cancellationToken)
        {
            var apiRequest = new GetAdvertsRequest(request.Postcode, request.Distance, request.Route);

            var response = await _apiClient.Get<GetAdvertsResponse>(apiRequest);

            return new GetVacanciesQueryResult
            {
                Routes = response.Routes,
                Location = response.Location,
                Vacancies = response.Vacancies,
                TotalFound = response.TotalFound
            };

        }
    }
}