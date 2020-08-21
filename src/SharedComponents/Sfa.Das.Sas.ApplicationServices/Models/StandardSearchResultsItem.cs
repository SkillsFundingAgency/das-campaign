using System;
using System.Collections.Generic;

namespace Sfa.Das.Sas.ApplicationServices.Models
{
    public sealed class StandardSearchResultsItem
    {
        public string StandardId { get; set; }

        public string Title { get; set; }

        public int Level { get; set; }

        public bool Published { get; set; }

        public List<string> JobRoles { get; set; }

        public List<string> Keywords { get; set; }

        public int Duration { get; set; }

        public string IntroductoryText { get; set; }

        public string EntryRequirements { get; set; }

        public string WhatApprenticesWillLearn { get; set; }

        public string Qualifications { get; set; }

        public string ProfessionalRegistration { get; set; }

        public string OverviewOfRole { get; set; }

        public DateTime? EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }
    }
}