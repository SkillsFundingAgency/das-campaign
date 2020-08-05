using System;
using System.Collections.Generic;
using System.Linq;

namespace Sfa.Das.Sas.Shared.Components.ViewComponents.ApprenticeshipDetails
{
    public class StandardDetailsViewModel : ApprenticeshipDetailBase
    {

        public string EntryRequirements { get; set; }
        public string WhatApprenticesWillLearn { get; set; }
        public string Qualifications { get; set; }
        public string StandardPageUri { get; set; }

        public IEnumerable<AssessmentOrganisationViewModel> AssessmentOrganisations { get; set; }
        public bool AssessmentOrganisationPresent => AssessmentOrganisations != null && AssessmentOrganisations.Any();
        public bool RegulatedStandard { get; set; }
        public DateTime? LastDateForNewStarts { get; set; }
    }
}
