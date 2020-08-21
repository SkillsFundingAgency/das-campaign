using Sfa.Das.Sas.Core.Domain.Model;
using System.Collections.Generic;

namespace Sfa.Das.Sas.Shared.Components.Extensions.Domain
{
    public static class AddressExtensions
    {
        public static string GetCommaDelimitedAddress(this Address address)
        {
            var addressParts = new List<string>(5);
            if (!string.IsNullOrWhiteSpace(address.Address1)) addressParts.Add(address.Address1);
            if (!string.IsNullOrWhiteSpace(address.Address2)) addressParts.Add(address.Address2);
            if (!string.IsNullOrWhiteSpace(address.Town)) addressParts.Add(address.Town);
            if (!string.IsNullOrWhiteSpace(address.County)) addressParts.Add(address.County);

            return string.Join(", ", addressParts);
        }
    }
}
