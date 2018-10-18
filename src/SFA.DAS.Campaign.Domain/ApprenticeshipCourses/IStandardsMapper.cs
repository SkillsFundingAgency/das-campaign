using SFA.DAS.Apprenticeships.Api.Types;
using SFA.DAS.Campaign.Models.ApprenticeshipCourses;

namespace SFA.DAS.Campaign.Domain.ApprenticeshipCourses
{
    public interface IStandardsMapper
    {
        StandardResultItem Map(ApprenticeshipSearchResultsItem item);
    }
}