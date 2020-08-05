using System.Linq;
using Sfa.Das.Sas.Core.Domain.Model;
using Sfa.Das.Sas.Shared.Components.ViewComponents.ApprenticeshipDetails;

namespace Sfa.Das.Sas.Shared.Components.Mapping
{
    public class FrameworkDetailsViewModelMapper : IFrameworkDetailsViewModelMapper
    {
        public FrameworkDetailsViewModel Map(Framework item)
        {
            if (item == null) return null;

            var framework = new FrameworkDetailsViewModel();

            framework.Level = item.Level;
            framework.Title = item.Title;
            framework.Duration = item.Duration;
            framework.EffectiveTo = item.EffectiveTo;
            framework.CompletionQualifications = item.CompletionQualifications;
            framework.CombinedQualification = item.CombinedQualification;
            framework.CompetencyQualification = item.CompetencyQualification;
            framework.KnowledgeQualification = item.KnowledgeQualification;
            framework.ProfessionalRegistration = item.ProfessionalRegistration;
            framework.JobRoles = item.JobRoleItems.Select(s => s.Title);
            framework.Overview = item.FrameworkOverview;
            framework.Id = item.FrameworkId;
            framework.MaxFunding = item.MaxFunding;

            return framework;
        }
    }
}
