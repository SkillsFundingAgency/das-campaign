using SFA.DAS.Campaign.Domain.Enums;
using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Domain.Vacancies
{
    public interface IVacanciesRepository
    {
        Task<VacancySearchResult> GetByPostcode(string postcode, int distance);
        Task<VacancySearchResult> GetByRoute(string routeId,string postcode, int distance);

        Country MapToCountry(string country);
    }
}