using SFA.DAS.Campaign.Models.Vacancy;
using SFA.DAS.Vacancies.Api.Models;

namespace SFA.DAS.Campaign.Domain.ApprenticeshipCourses
{
    public interface IVacanciesMapper
    {
        VacancySearchResultItem Map(Result item);
    }
}