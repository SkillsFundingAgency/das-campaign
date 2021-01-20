using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers.Fat
{
    public class FatController : Controller
    {
        
        [Route("/employers/find-apprenticeships")]
        public IActionResult Search(SearchQueryViewModel model)
        {
            
            return RedirectPermanent("");
        }

        [Route("/employers/find-apprenticeships/apprenticeship")]
        public IActionResult Apprenticeship(string id)
        {
            return RedirectPermanent("");
        }

    }
}
