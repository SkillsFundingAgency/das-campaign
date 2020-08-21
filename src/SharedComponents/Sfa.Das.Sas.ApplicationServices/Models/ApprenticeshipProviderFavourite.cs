using System.Collections.Generic;

namespace Sfa.Das.Sas.ApplicationServices.Models
{
    public class ApprenticeshipProviderFavourite
    {
        public ApprenticeshipProviderFavourite(int ukprn,string name, IList<int> locations)
        {
            Ukprn = ukprn;
            Name = name;
            Locations = locations;
        }

        public int Ukprn { get; set; }
        public string Name { get; set; }
        public IEnumerable<int> Locations { get; set; }
        public bool Active { get; set; } = false;
    }
}