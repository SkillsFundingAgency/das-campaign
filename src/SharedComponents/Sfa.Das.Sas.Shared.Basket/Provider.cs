using System.Collections.Generic;
using Newtonsoft.Json;

namespace Sfa.Das.Sas.Shared.Basket.Models
{
    public class Provider
    {
        [JsonConstructor]
        public Provider(int ukprn, string name)
        {
            Ukprn = ukprn;
            Name = name;
            Locations = new List<int>();
        }
        public Provider(int ukprn, string name, int location)
        {
            Ukprn = ukprn;
            Name = name;
            Locations = new List<int>(){location};
        }
      public int Ukprn { get; set; }
      public string Name { get; set; }
      public IList<int> Locations { get; set; }
    }
}
