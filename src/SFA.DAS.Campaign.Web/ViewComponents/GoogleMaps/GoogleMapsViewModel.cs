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

    }

}