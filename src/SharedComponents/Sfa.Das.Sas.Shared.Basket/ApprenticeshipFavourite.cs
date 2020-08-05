using System.Collections.Generic;

namespace Sfa.Das.Sas.Shared.Basket.Models
{
    public class ApprenticeshipFavourite
    {
        public ApprenticeshipFavourite()
        {
            Providers = new List<Provider>();
        }

        public ApprenticeshipFavourite(string apprenticeshipId) : this()
        {
            ApprenticeshipId = apprenticeshipId;
        }

        public ApprenticeshipFavourite(string apprenticeshipId, int ukprn, string providerName) : this(apprenticeshipId)
        {
            Providers.Add(new Provider(ukprn, providerName));
        }
        public ApprenticeshipFavourite(string apprenticeshipId, int ukprn,string providerName, int location) : this(apprenticeshipId)
        {
            Providers.Add(new Provider(ukprn, providerName, location));
        }

        public string ApprenticeshipId { get; set; }
        public IList<Provider> Providers { get; set; }
    }
}
