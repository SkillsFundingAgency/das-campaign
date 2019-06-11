using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.Campaign.Models.Vacancy;

namespace SFA.DAS.Campaign.Domain.Vacancies
{
    public interface IVacanciesService
    {
        Task<VacancySearchResult> GetByPostcode(string postcode, int distance);
        Task<VacancySearchResult> GetByRoute(string routeId,string postcode, int distance);
    }
}