using System;
using System.Globalization;
using Sfa.Das.Sas.Resources;

namespace Sfa.Das.Sas.Shared.Components.ViewComponents.ApprenticeshipDetails
{
    public class ApprenticeshipDetailBase
    {
        private string _professionalRegistration;

        public string Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public int Level { get; set; }
        public string EquivalentText => EquivalenceLevelService.GetApprenticeshipLevel(Level.ToString());
        public int Duration { get; set; }
        public int MaxFunding { get; set; }
        public string FundingCap => MaxFunding.ToString("C0", new CultureInfo("en-GB"));
        public string ProfessionalRegistration
        {
            get => string.IsNullOrEmpty(_professionalRegistration) ? "None specified." : _professionalRegistration;
            set => _professionalRegistration = value;
        }
    }
}