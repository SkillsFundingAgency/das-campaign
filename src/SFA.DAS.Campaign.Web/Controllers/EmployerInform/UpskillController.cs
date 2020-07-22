using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Content;
using SFA.DAS.Campaign.Content.ContentTypes;

namespace SFA.DAS.Campaign.Web.Controllers.EmployerInform
{
    public class UpskillController : Controller
    {
        private readonly IContentService _contentService;

        public UpskillController(IContentService contentService)
        {
            _contentService = contentService;
        }
        
        [HttpGet("employer/upskill")]
        public async Task<IActionResult> Index()
        {
            var content = await _contentService.GetContentByHubAndSlug<InfoPage>(HubTypes.Employer, "upskill");
            
            return View("~/Views/EmployerInform/Upskill.cshtml", content);
        }
    }
}