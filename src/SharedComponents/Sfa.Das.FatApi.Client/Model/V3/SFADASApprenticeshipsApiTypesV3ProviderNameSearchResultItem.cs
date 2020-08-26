using System.Collections.Generic;

namespace Sfa.Das.FatApi.Client.Model
{
    public class SFADASApprenticeshipsApiTypesV3ProviderNameSearchResultItem
    {
        public int Ukprn { get; set; }
        public string ProviderName { get; set; }
        public bool NationalProvider { get; set; }
        public string OverallCohort { get; set; }
        public bool HasNonLevyContract { get; set; }
        public bool IsLevyPayerOnly { get; set; }
        public bool CurrentlyNotStartingNewApprentices { get; set; }
        public bool IsHigherEducationInstitute { get; set; }
        public IList<string> Aliases { get; set; }
        public string Uri { get; set; }
    }
}
