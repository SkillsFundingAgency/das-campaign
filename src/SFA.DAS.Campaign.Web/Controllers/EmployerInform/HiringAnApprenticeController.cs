using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Content;
using SFA.DAS.Campaign.Content.ContentTypes;

namespace SFA.DAS.Campaign.Web.Controllers.EmployerInform
{
    public class HiringAnApprenticeController : Controller
    {
        private readonly IContentService _contentService;

        public HiringAnApprenticeController(IContentService contentService)
        {
            _contentService = contentService;
        }
        
        
        [HttpGet("employer/hiring-an-apprentice")]
        public async Task<IActionResult> Index()
        {
            var content = await _contentService.GetContentByHubAndSlug<InfoPage>(HubTypes.Employer, "hiring-an-apprentice");
            
            return View("~/Views/EmployerInform/HiringAnApprentice.cshtml", content);
        }
    }
}