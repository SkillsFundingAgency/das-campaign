using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Application.Content;
using SFA.DAS.Campaign.Application.Content.ContentTypes;
using SFA.DAS.Campaign.Application.DataCollection;
using SFA.DAS.Campaign.Web.Models;

namespace SFA.DAS.Campaign.Web.Controllers
{
    public class ParentsController : Controller
    {
        private readonly IContentService _contentService;

        public ParentsController(IContentService contentService)
        {
            _contentService = contentService;
        }
        
        [HttpGet("/parents/their-career")] 
        public async Task<IActionResult> TheirCareer()
        {
            var content = await _contentService.GetContentByHubAndSlug<InfoPage>(HubTypes.Parents, "their-career");
   
            return View("TheirCareer", content);
        }
    }
}