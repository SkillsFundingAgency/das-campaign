using SFA.DAS.Apprenticeships.Api.Types;
using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;
using SFA.DAS.Campaign.Models.ApprenticeshipCourses;
using SFA.DAS.Campaign.Models.Vacancy;
using SFA.DAS.Vacancies.Api.Models;
using Location = SFA.DAS.Campaign.Models.Vacancy.Location;

namespace SFA.DAS.Campaign.Application.Vacancies
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
                Location = new Location() { Latitude = item.Location.Latitude, Longitude = item.Location.Longitude},
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
