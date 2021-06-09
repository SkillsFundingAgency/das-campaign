using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Domain.Content;

namespace SFA.DAS.Campaign.Web.Controllers.Redesign
{
    public class LandingPageController : Controller
    {
        private readonly IContentService _contentService;

        public LandingPageController(IContentService contentService)
        {
            _contentService = contentService;
        }
        
        [HttpGet("/apprentices/are-they-right-for-you")]
        public IActionResult AreTheyRightForYou()
        {
            var articleCards = _contentService.GetArticleCardsFor("are-they-right-for-you", HubType.Apprentices);
            
            return View("~/Views/LandingPages/Apprentices/AreApprenticeshipsRightForMe.cshtml", articleCards);
        }
        
        [HttpGet("/apprentices/how-do-they-work")]
        public IActionResult HowDoTheyWork()
        {
            return View("~/Views/LandingPages/Apprentices/HowDoTheyWork.cshtml");
        }
        
        [HttpGet("/apprentices/get-started")]
        public IActionResult GettingStarted()
        {
            return View("~/Views/LandingPages/Apprentices/GettingStarted.cshtml");
        }

        [HttpGet("/apprentices/employer-profiles")]
        public IActionResult EmployerProfiles()
        {
            return View("~/Views/LandingPages/Apprentices/EmployerProfiles.cshtml");
        }
        
        [HttpGet("/employers/are-they-right-for-you")]
        public IActionResult EmployersAreTheyRightForYou()
        {
            return View("~/Views/LandingPages/Employers/AreApprenticeshipsRightForMe.cshtml");
        }
        
        [HttpGet("/employers/how-do-they-work")]
        public IActionResult EmployersHowDoTheyWork()
        {
            return View("~/Views/LandingPages/Employers/HowDoTheyWork.cshtml");
        }
        
        [HttpGet("/employers/setting-it-up")]
        public IActionResult SettingItUp()
        {
            return View("~/Views/LandingPages/Employers/SettingItUp.cshtml");
        }

        [HttpGet("/employers/the-road-to-a-quality-apprenticeship")]
        public IActionResult TheRoadToAQualityApprenticeship()
        {
            return View("~/Views/LandingPages/Employers/TheRoadToAQualityApprenticeship.cshtml");
        }

        [HttpGet("/influencers/become-an-ambassador")]
        public IActionResult BecomeAnAmbassador()
        {
            return View("~/Views/LandingPages/Influencers/BecomeAnAmbassador.cshtml");
        }

        [HttpGet("/influencers/browse-apprenticeships")]
        public IActionResult BrowseApprenticeships()
        {
            return View("~/Views/LandingPages/Influencers/BrowseApprenticeships.cshtml");
        }

        [HttpGet("/influencers/how-they-work")]
        public IActionResult HowTheyWork()
        {
            return View("~/Views/LandingPages/Influencers/HowTheyWork.cshtml");
        }

        [HttpGet("/influencers/inspirational-resources")]
        public IActionResult InspirationalResources()
        {
            return View("~/Views/LandingPages/Influencers/InspirationalResources.cshtml");
        }


        [HttpGet("/influencers/request-ask-support")]
        public IActionResult RequestSupport()
        {
            return View("~/Views/LandingPages/Influencers/RequestSupport.cshtml");
        }

    }
}