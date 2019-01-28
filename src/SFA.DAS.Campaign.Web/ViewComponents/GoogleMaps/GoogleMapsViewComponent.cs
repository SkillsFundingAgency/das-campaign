using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.ViewComponents.GoogleMaps
{
    public class GoogleMapsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(GoogleMapsViewModel googleMapsViewModel)
        {
          
            return View("Default", googleMapsViewModel);
        }
    }

   

}