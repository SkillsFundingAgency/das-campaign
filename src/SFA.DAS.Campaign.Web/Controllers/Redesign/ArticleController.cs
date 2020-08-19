using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers.Redesign
{
    public class ArticleController : Controller
    {
        [HttpGet("/{hub}/{article}")]
        public IActionResult Index(string hub, string article)
        {
            return View($"~/Views/Articles/{hub}/{article}.cshtml");
        }
    }
}