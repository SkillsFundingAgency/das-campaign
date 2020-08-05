using System.Collections.Generic;

namespace Sfa.Das.Sas.Shared.Components.ViewComponents.ApprenticeshipDetails
{
    public class FrameworkDetailsViewModel : ApprenticeshipDetailBase
    {  
        public IEnumerable<string> JobRoles { get; set; }
        public IEnumerable<string> CompetencyQualification{ get; set; }
        public IEnumerable<string> KnowledgeQualification { get; set; }
        public IEnumerable<string> CombinedQualification { get; set; }
        public string CompletionQualifications { get; set; }
    }
}
