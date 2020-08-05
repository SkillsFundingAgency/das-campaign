using System.Threading.Tasks;
using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.Shared.Components.ViewComponents.ApprenticeshipDetails;

namespace Sfa.Das.Sas.Shared.Components.Orchestrators
{
    public interface IApprenticeshipOrchestrator
    {
        Task<FrameworkDetailsViewModel> GetFramework(string id);
        Task<StandardDetailsViewModel> GetStandard(string id);
        ApprenticeshipType GetApprenticeshipType(string id);
    }
}