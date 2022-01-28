using System.Collections.Generic;
using SFA.DAS.Campaign.Infrastructure.Api.Responses;

namespace SFA.DAS.Campaign.Application.Vacancies.Queries.GetVacancies
{
    public class GetVacanciesQueryResult
    {
        public long TotalFound { get ; set ; }
        public List<Vacancy> Vacancies { get ; set ; }
        public Location Location { get ; set ; }
        public List<Route> Routes { get ; set ; }
    }
}