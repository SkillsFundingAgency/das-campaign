using Ifa.Api.Model;
using SFA.DAS.Apprenticeships.Api.Types;
using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;
using SFA.DAS.Campaign.Models.ApprenticeshipCourses;

namespace SFA.DAS.Campaign.Application.ApprenticeshipCourses.Services
{
    public class StandardsMapper : IStandardsMapper
    {
        public StandardResultItem Map(ApprenticeshipSearchResultsItem item)
        {
            return new StandardResultItem
            {
                Id = int.Parse(item.StandardId),
                Duration = item.Duration,
                Title = item.Title,
                Level = item.Level
            };
        }
        public StandardResultItem Map(TempApprenticeshipStandard item)
        {
            return new StandardResultItem
            {
                Id = item.ID,
                Duration = item.Duration,
                Title = item.Title,
                Level = item.Level
            };
        }
    }
}
