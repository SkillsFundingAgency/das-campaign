using SFA.DAS.Apprenticeships.Api.Types;
using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;
using SFA.DAS.Campaign.Infrastructure.Models;

namespace SFA.DAS.Campaign.Infrastructure.Mappers
{
    public interface IStandardsMapper
    {
        StandardResultItem Map(ApprenticeshipSearchResultsItem item);
        StandardResultItem Map(ApiApprenticeshipStandard item);
    }
}