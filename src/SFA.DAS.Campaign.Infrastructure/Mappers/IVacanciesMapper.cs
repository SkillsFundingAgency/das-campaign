using SFA.DAS.Campaign.Domain.Vacancies;
using SFA.DAS.Vacancies.Api.Models;

namespace SFA.DAS.Campaign.Infrastructure.Mappers
{
    public interface IVacanciesMapper
    {
        VacancySearchResultItem Map(Result item);
    }
}