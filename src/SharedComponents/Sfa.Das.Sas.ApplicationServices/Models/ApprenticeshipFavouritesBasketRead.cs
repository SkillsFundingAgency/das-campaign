using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sfa.Das.Sas.Shared.Basket.Models;

namespace Sfa.Das.Sas.ApplicationServices.Models
{
    public class ApprenticeshipFavouritesBasketRead : IEnumerable<ApprenticeshipFavouriteRead>
    {

        private readonly List<ApprenticeshipFavouriteRead> _items = new List<ApprenticeshipFavouriteRead>();

        public ApprenticeshipFavouritesBasketRead()
        {
        }

        public ApprenticeshipFavouritesBasketRead(ApprenticeshipFavouritesBasket basket)
        {
            _items.AddRange(basket.Select(s => new ApprenticeshipFavouriteRead(s.ApprenticeshipId)
            {
                Providers = s.Providers.Select(t => new ApprenticeshipProviderFavourite(t.Ukprn,t.Name,t.Locations)).ToList()
            }));
        }

        public bool IsInBasket(string apprenticeshipId, int? ukprn = null)
        {
            if (!ukprn.HasValue)
            {
                return _items.Any(x => x.ApprenticeshipId == apprenticeshipId);
            }
            else
            {
                var apprenticeship = _items.SingleOrDefault(x => x.ApprenticeshipId == apprenticeshipId);

                if (apprenticeship == null)
                    return false;
                else
                {
                    return ukprn.HasValue ? apprenticeship.Providers.Select(s => s.Ukprn).Contains(ukprn.Value) : false;
                }
            }
        }

        public bool IsInBasket(string apprenticeshipId, int ukprn, int locationId)
        {
            if (IsInBasket(apprenticeshipId, ukprn))
            {
                var provider = _items.FirstOrDefault(x => x.ApprenticeshipId == apprenticeshipId)?.Providers.SingleOrDefault(w => w.Ukprn == ukprn);

                return provider.Locations.Any(a => a == locationId);
            }

            return false;
        }

        public IEnumerator<ApprenticeshipFavouriteRead> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
    }
}
