using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Web.ViewComponents.Modal;

namespace SFA.DAS.Campaign.Web.ViewComponents.GoogleMaps
{
  

    public class GoogleMapsViewModel
    {
        public GoogleMapsViewModel(string staticMapUrl)
        {
            StaticMapUrl = staticMapUrl;
        }

        

        public string StaticMapUrl { get; internal set; }

        public string Class { get; internal set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string MarkerDataUrl => generateMarkerDataUrl();

        private string generateMarkerDataUrl()
        {
            if (!string.IsNullOrEmpty(Postcode) && Distance > 0)
            {
                return $"/FindApprenticeship/searchResults/Data/{Postcode}/{Distance}";
            }
            return "";
        }

        public int Distance { get; set; }
        public string Postcode { get; set; }
        

    }

}