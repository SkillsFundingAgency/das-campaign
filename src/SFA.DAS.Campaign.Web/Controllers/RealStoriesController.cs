using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Application.Content;
using SFA.DAS.Campaign.Application.Content.ContentTypes;

namespace SFA.DAS.Campaign.Web.Controllers
{
    public class RealStoriesController : Controller
    {
        private readonly IContentService _contentService;

        public RealStoriesController(IContentService contentService)
        {
            _contentService = contentService;
        }
        
        [Route("/apprentice/real-stories")]
        public async Task<IActionResult> Apprentice()
        {
            var realStories = await _contentService.GetContentByType<RealStory>();
            
            return View(realStories);
        }
        [Route("/employer/real-stories")]
        public IActionResult Employer()
        {
            return View();
        }
    }
}