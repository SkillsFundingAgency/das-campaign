using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Application.Content;
using SFA.DAS.Campaign.Application.Content.ContentTypes;

namespace SFA.DAS.Campaign.Web.Controllers
{
    [Route("interests")]
    public class InterestsController : Controller
    {
        private readonly IContentService _contentService;

        public InterestsController(IContentService contentService)
        {
            _contentService = contentService;
        }
        
        public async Task<IActionResult> Index()
        {
            var interestPage = await _contentService.GetContentBySlug<InterestsPage>("interests");
            
            return View(interestPage);
        }

        [Route("{slug}")]
        public async Task<IActionResult> Interest(string slug)
        {
            var interest = await _contentService.GetContentBySlug<Interest>(slug);
        
            return View(interest);
        }
    }
}