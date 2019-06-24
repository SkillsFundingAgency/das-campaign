using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Domain.Vacancies
{
    public interface IVacanciesRepository
    {
        Task<IList<VacancySearchResultItem>> GetByPostcode(string postcode, int distance);
        Task<IList<VacancySearchResultItem>> GetByRoute(string routeId,string postcode, int distance);
    }
}