using System.Collections.Generic;

namespace Sfa.Das.Sas.Shared.Components.ViewComponents.Fat
{
    public class TrainingProviderSearchResultsItem
    {
        public string Name { get; set; }
        public bool NationalProvider { get; set; }
        public int Ukprn { get; set; }
        public double? Distance { get; set; }
        public double? EmployerSatisfaction { get; set; }
        public double? LearnerSatisfaction { get; set; }
        public double? OverallAchievementRate { get; set; }
        public int LocationId { get; set; }
        public LocationAddress LocationAddress { get; set; }
        public bool HasOtherLocations { get; internal set; }
        public bool Active { get; set; }
    }

    public class LocationAddress
    {
        public string PostCode { get; set; }
        public string AddressWithoutPostCode { get; internal set; }
    }
}
