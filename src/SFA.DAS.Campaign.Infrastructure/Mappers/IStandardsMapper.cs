using Ifa.Api.Model;
using SFA.DAS.Apprenticeships.Api.Types;
using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;

namespace SFA.DAS.Campaign.Infrastructure.Mappers
{
    public interface IStandardsMapper
    {
        StandardResultItem Map(ApprenticeshipSearchResultsItem item);
        StandardResultItem Map(ApiApprenticeshipStandard item);
    }
}