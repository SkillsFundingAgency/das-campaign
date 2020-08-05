using SFA.DAS.Apprenticeships.Api.Types;

namespace Sfa.Das.Sas.Core.Domain.Model
{
    using System;
    using System.Collections.Generic;

    public sealed class Framework : IApprenticeshipProduct
    {
        public string FrameworkId { get; set; }

        public string Title { get; set; }

        public string FrameworkName { get; set; }

        public string PathwayName { get; set; }

        public int FrameworkCode { get; set; }

        public int PathwayCode { get; set; }

        public int ProgType { get; set; }

        public int Level { get; set; }

        public int MaxFunding { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }

        public string CompletionQualifications { get; set; }

        public string FrameworkOverview { get; set; }

        public string EntryRequirements { get; set; }

        public string ProfessionalRegistration { get; set; }

        public IEnumerable<JobRoleItem> JobRoleItems { get; set; }

        public IEnumerable<string> CompetencyQualification { get; set; }

        public IEnumerable<string> KnowledgeQualification { get; set; }

        public IEnumerable<string> CombinedQualification { get; set; }

        public int Duration { get; set; }

        public bool IsActiveFramework { get; set; }

        public List<FundingPeriod> FundingPeriods { get; set; }

        public DateTime? NextEffectiveFrom { get; set; }

        public int? NextFundingCap { get; set; }
    }
}
