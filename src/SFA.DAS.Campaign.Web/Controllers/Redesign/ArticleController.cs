using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Domain.Content;

namespace SFA.DAS.Campaign.Web.Controllers.Redesign
{
    public class ArticleController : Controller
    {
        private readonly IContentService _contentService;

        public ArticleController(IContentService contentService)
        {
            _contentService = contentService;
        }
        
        [HttpGet("/{hub}/{slug}")]
        public async Task<IActionResult> Index(string hub, string slug)
        {
            var articlePage = await _contentService.GetPage<Article>(slug);
            
            return View($"~/Views/CMS/Article.cshtml", articlePage);
        }
    }
}