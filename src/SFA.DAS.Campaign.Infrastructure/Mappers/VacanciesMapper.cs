using SFA.DAS.Campaign.Domain.Vacancies;
using SFA.DAS.Vacancies.Api.Models;

namespace SFA.DAS.Campaign.Infrastructure.Mappers
{
    public class VacanciesMapper : IVacanciesMapper
    {
        public VacancySearchResultItem Map(Result item)
        {
            return new VacancySearchResultItem
            {
                Title = item.Title,
                TrainingTitle = item.TrainingTitle,
                ShortDescription = item.ShortDescription,
                DistanceInMiles = item.DistanceInMiles,
                Location = new Domain.Vacancies.Location { Latitude = item.Location.Latitude, Longitude = item.Location.Longitude},
                EmployerName = item.EmployerName,
                PostedDate = item.PostedDate.UtcDateTime,
                ExpectedStartDate = item.ExpectedStartDate.UtcDateTime,
                ApplicationClosingDate = item.ApplicationClosingDate.UtcDateTime,
                VacancyReference = item.VacancyReference,
                VacancyUrl = item.VacancyUrl
            };
        }
    }
}
