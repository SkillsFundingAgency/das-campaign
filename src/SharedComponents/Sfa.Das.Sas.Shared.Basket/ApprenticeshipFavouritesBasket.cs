using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Sfa.Das.Sas.Shared.Basket.Models
{
    public class ApprenticeshipFavouritesBasket : IEnumerable<ApprenticeshipFavourite>
    {
        private readonly List<ApprenticeshipFavourite> _items = new List<ApprenticeshipFavourite>();

        public ApprenticeshipFavouritesBasket()
        {
            Id = Guid.NewGuid();
        }

        internal ApprenticeshipFavouritesBasket(IList<ApprenticeshipFavourite> basketItems)
        {
            _items.AddRange(basketItems);
        }

        public Guid Id { get; set; }

        public bool Add(string apprenticeshipId)    
        {
            if (IsInBasket(apprenticeshipId))
            {
                return false;
            }
            else
            {
                _items.Add(new ApprenticeshipFavourite(apprenticeshipId));

                return true;
            }
        }

        public bool Add(string apprenticeshipId, int ukprn, string providerName)
        {
            if (!IsInBasket(apprenticeshipId))
            {
                _items.Add(new ApprenticeshipFavourite(apprenticeshipId, ukprn, providerName));
                return true;
            }

            if (IsInBasket(apprenticeshipId,ukprn))
            {
                return false;
            }

            _items.FirstOrDefault(x => x.ApprenticeshipId == apprenticeshipId)?.Providers.Add(new Provider(ukprn, providerName));

            return true;
        }

        public bool Add(string apprenticeshipId, int ukprn, string providerName, int location)
        {
            if (!IsInBasket(apprenticeshipId))
            {
                _items.Add(new ApprenticeshipFavourite(apprenticeshipId, ukprn,providerName, location));
                return true;
            }

            if (IsInBasket(apprenticeshipId, ukprn, location))
            {
                return false;
            }

            if (IsInBasket(apprenticeshipId,ukprn))
            {
                var item = (_items.FirstOrDefault(x => x.ApprenticeshipId == apprenticeshipId)?.Providers)?.SingleOrDefault(w => w.Ukprn == ukprn);

                item.Locations.Add(location);
            }
            else
            {
                _items.FirstOrDefault(x => x.ApprenticeshipId == apprenticeshipId)?.Providers.Add(new Provider(ukprn, providerName, location));
            }

            return true;
        }

        public bool IsInBasket(string apprenticeshipId, int ukprn, int location)
        {
            if (IsInBasket(apprenticeshipId, ukprn))
            {
                var provider = _items.FirstOrDefault(x => x.ApprenticeshipId == apprenticeshipId)?.Providers?.SingleOrDefault(w => w.Ukprn == ukprn);

                return provider.Locations.Contains(location);
            }

            return false;
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
                    return apprenticeship.Providers.Any(w => w.Ukprn == ukprn);
                }
            }
        }

        public void Remove(string apprenticeshipId)
        {
            _items.RemoveAll(w => w.ApprenticeshipId == apprenticeshipId);
        }

        public void Remove(string apprenticeshipId, int ukprn)
        {
            var apprenticeship = _items.FirstOrDefault(x => x.ApprenticeshipId == apprenticeshipId);
            var provider = apprenticeship?.Providers.SingleOrDefault(w => w.Ukprn == ukprn);

            apprenticeship?.Providers.Remove(provider);
        }

        public void Remove(string apprenticeshipId, int ukprn, int location)
        {
            var apprenticeship = _items.FirstOrDefault(x => x.ApprenticeshipId == apprenticeshipId);
            var provider = apprenticeship?.Providers.SingleOrDefault(w => w.Ukprn == ukprn);
            provider?.Locations.Remove(location);

            if (provider.Locations.Count == 0)
                apprenticeship.Providers.Remove(provider);
        }

        public IEnumerator<ApprenticeshipFavourite> GetEnumerator() 
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        public ApprenticeshipFavourite this[int i] => _items[i];
    }
}
