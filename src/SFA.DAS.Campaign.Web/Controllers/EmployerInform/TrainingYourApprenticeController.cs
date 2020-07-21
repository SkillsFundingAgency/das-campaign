using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Application.Content;
using SFA.DAS.Campaign.Application.Content.ContentTypes;

namespace SFA.DAS.Campaign.Web.Controllers.EmployerInform
{
    public class TrainingYourApprenticeController : Controller
    {
        private readonly IContentService _contentService;

        public TrainingYourApprenticeController(IContentService contentService)
        {
            _contentService = contentService;
        }
        
        [HttpGet("/employer/training-your-apprentice")]
        public async Task<IActionResult> Index()
        {
            var content = await _contentService.GetContentByHubAndSlug<InfoPage>(HubTypes.Employer, "training-your-apprentice");
            
            return View("~/Views/EmployerInform/TrainingYourApprentice.cshtml", content);
        }
    }
}