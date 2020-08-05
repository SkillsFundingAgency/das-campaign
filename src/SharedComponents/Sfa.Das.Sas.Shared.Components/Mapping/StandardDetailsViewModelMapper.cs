using System.Collections.Generic;
using System.Linq;
using Sfa.Das.Sas.Core.Domain;
using Sfa.Das.Sas.Core.Domain.Model;
using Sfa.Das.Sas.Shared.Components.ViewComponents.ApprenticeshipDetails;

namespace Sfa.Das.Sas.Shared.Components.Mapping
{
    public class StandardsDetailsViewModelMapper : IStandardDetailsViewModelMapper
    {
        private readonly IAssessmentOrganisationViewModelMapper _assessmentOrganisationViewModelMapper;

        public StandardsDetailsViewModelMapper(IAssessmentOrganisationViewModelMapper assessmentOrganisationViewModelMapper)
        {
            _assessmentOrganisationViewModelMapper = assessmentOrganisationViewModelMapper;
        }

        public StandardDetailsViewModel Map(Standard item, IEnumerable<AssessmentOrganisation> assessmentOrganisations)
        {
            if (item == null) return null;

            var standard = new StandardDetailsViewModel();
            standard.Id = item.StandardId;
            standard.Title = item.Title;
            standard.RegulatedStandard = item.RegulatedStandard;
            standard.LastDateForNewStarts = item.LastDateForNewStarts;
            standard.Level = item.Level;
            standard.Duration = item.Duration;
            standard.EntryRequirements = item.EntryRequirements;
            standard.Qualifications = item.Qualifications;
            standard.StandardPageUri = item.StandardPageUri;
            standard.WhatApprenticesWillLearn = item.WhatApprenticesWillLearn;
            standard.MaxFunding = item.MaxFunding;
            standard.Overview = item.OverviewOfRole;
            standard.ProfessionalRegistration = item.ProfessionalRegistration;
            standard.AssessmentOrganisations = assessmentOrganisations?.Select(_assessmentOrganisationViewModelMapper.Map);

            return standard;
        }
    }
}
