using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.ViewComponents.GoogleMaps
{
    public class GoogleMapsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(GoogleMapsViewModel googleMapsViewModel)
        {
            return View("Default", googleMapsViewModel);
        }
    }

   

}