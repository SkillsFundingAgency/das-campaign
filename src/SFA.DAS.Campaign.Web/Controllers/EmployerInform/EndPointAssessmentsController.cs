using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Content;
using SFA.DAS.Campaign.Content.ContentTypes;

namespace SFA.DAS.Campaign.Web.Controllers.EmployerInform
{
    public class EndPointAssessmentsController : Controller
    {
        private readonly IContentService _contentService;

        public EndPointAssessmentsController(IContentService contentService)
        {
            _contentService = contentService;
        }
        
        [HttpGet("employer/end-point-assessments")]
        public async Task<IActionResult> Index()
        {
            var content = await _contentService.GetContentByHubAndSlug<InfoPage>(HubTypes.Employer, "end-point-assessments");
            
            return View("~/Views/EmployerInform/EndPointAssessments.cshtml", content);
        }
    }
}