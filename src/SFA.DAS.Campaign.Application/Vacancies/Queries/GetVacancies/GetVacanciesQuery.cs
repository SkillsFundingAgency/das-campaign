using MediatR;

namespace SFA.DAS.Campaign.Application.Vacancies.Queries.GetVacancies
{
    public class GetVacanciesQuery : IRequest<GetVacanciesQueryResult>
    {
        public string Postcode { get ; set ; }
        public string Route { get ; set ; }
        public int Distance { get ; set ; }
    }
}