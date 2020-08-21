using Sfa.Das.Sas.Core.Domain.Model;

namespace Sfa.Das.Sas.Core.Domain.Repositories
{
    public interface IApprenticeshipProviderRepository
    {
        ApprenticeshipDetails GetCourseByStandardCode(int ukPrn, int locationId, string standardCode);
        ApprenticeshipDetails GetCourseByFrameworkId(int ukPrn, int locationId, string frameworkId);
        int GetFrameworksAmountWithProviders();
        int GetStandardsAmountWithProviders();
    }
}
