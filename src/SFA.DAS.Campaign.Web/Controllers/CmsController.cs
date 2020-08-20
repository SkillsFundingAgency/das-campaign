using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Content;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Web.Models;

namespace SFA.DAS.Campaign.Web.Controllers
{

    public class CmsController : Controller
    {
        private readonly IContentService _contentService;

        public CmsController(IContentService contentService)
        {
            _contentService = contentService;
        }
        [HttpGet]
        public async Task<IActionResult> Page(string slug)
        {


            var content = await _contentService.GetContent<InfoPage>(slug, ContentService.ContentType.infopage);

            return View("", content);

            
        }

        private ContentService.ContentType GetContentType(string slug)
        {
            return ContentService.ContentType.infopage;
        }

    }
}
