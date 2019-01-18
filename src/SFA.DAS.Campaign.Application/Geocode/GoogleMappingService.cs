using SFA.DAS.Campaign.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using SFA.DAS.Campaign.Models.Vacancy;

namespace SFA.DAS.Campaign.Application.Geocode
{
    public class GoogleMappingService : IMappingService
    {
        private readonly string _privateKey;
        private readonly string _usablePrivateKey;
        private readonly string _staticMapHeight;
        private readonly string _staticMapWidth;
        private readonly string _clientId;
        private readonly string _apiKey;

        public GoogleMappingService(IMappingConfiguration mappingConfiguration)
        {
            _staticMapHeight = mappingConfiguration.StaticHeight;
            _staticMapWidth = mappingConfiguration.StaticWidth;
            _clientId = mappingConfiguration.ClientId;
            _privateKey = mappingConfiguration.PrivateKey;
            _apiKey = mappingConfiguration.ApiKey;

            if (!string.IsNullOrEmpty(_privateKey))
            {
                _usablePrivateKey = _privateKey.Replace("-", "+").Replace("_", "/");
            }
        }

        public string GetStaticMapsUrl(Location coordinates)
        {
            if (coordinates == null) return "";

            var markers = $"{coordinates.Latitude},{coordinates.Longitude}";

            return GetStaticMapsUrl(markers);
        }

        public string GetStaticMapsUrl(IEnumerable<Location> locations)
        {
            if (locations == null) return "";

            var markers = string.Join('|', locations.Select(p => $"{p.Latitude},{p.Longitude}"));

            return GetStaticMapsUrl(markers);
        }

        public string GetStaticMapsUrl(IEnumerable<Location> locations, string height, string width)
        {
            if (locations == null) return "";

            var markers = string.Join('|', locations.Select(p => $"{p.Latitude},{p.Longitude}"));

            return GetStaticMapsUrl(markers,height,width);
        }

        private string GetStaticMapsUrl(string markers, string height = null, string width = null)
        {

            if (height == null)
            {
                height = _staticMapHeight;
            }

            if (width == null)
            {
                width = _staticMapWidth;
            }
            var staticMapsUrl =
                $"https://maps.googleapis.com/maps/api/staticmap?markers={markers}&size={width}x{height}&scale=2&zoom=10&key={_apiKey}";

            if (string.IsNullOrEmpty(_usablePrivateKey))
            {
                return staticMapsUrl;
            }

            var encoding = new ASCIIEncoding();

            // converting key to bytes will throw an exception, need to replace '-' and '_' characters first.
            var privateKeyBytes = Convert.FromBase64String(_usablePrivateKey);
            if (!string.IsNullOrEmpty(_clientId))
            {
                staticMapsUrl += $"&client={_clientId}";
            }

            var uri = new Uri(staticMapsUrl);
            var encodedPathAndQueryBytes = encoding.GetBytes(uri.LocalPath + uri.Query);

            // compute the hash
            var algorithm = new HMACSHA1(privateKeyBytes);
            var hash = algorithm.ComputeHash(encodedPathAndQueryBytes);

            // convert the bytes to string and make url-safe by replacing '+' and '/' characters
            var signature = Convert.ToBase64String(hash).Replace("+", "-").Replace("/", "_");

            // Add the signature to the existing URI.
            return uri.Scheme + "://" + uri.Host + uri.LocalPath + uri.Query + "&signature=" + signature;
        }
    }
}
