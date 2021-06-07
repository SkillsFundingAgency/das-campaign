using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SFA.DAS.Campaign.Domain.Content;

namespace SFA.DAS.Campaign.Web.Controllers.Redesign
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreviewController : ControllerBase
    {
        private readonly IContentService _contentService;

        public PreviewController(IContentService contentService)
        {
            _contentService = contentService;
        }
        public async Task<IActionResult> GetPagePreviewAsync(string entryId, CancellationToken cancellationToken = default)
        {
            
            return Ok();
        }
    }
}
