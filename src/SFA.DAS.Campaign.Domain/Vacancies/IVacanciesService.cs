using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.Campaign.Models.ApprenticeshipCourses;
using SFA.DAS.Campaign.Models.Vacancy;

namespace SFA.DAS.Campaign.Domain.Vacancies
{
    public interface IVacanciesService
    {
        Task<IList<VacancySearchResultItem>> GetByPostcode(string postcode, int distance);
    }
}