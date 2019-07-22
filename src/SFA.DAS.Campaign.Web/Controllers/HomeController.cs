using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SFA.DAS.Campaign.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) => _logger = logger;

        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogTrace("Test Trace");
            _logger.LogDebug("Test Debug");
            _logger.LogInformation("Test Information");
            _logger.LogWarning("Test Warning");
            _logger.LogError("Test Error");
            return View("Index");
        }

        [Route("privacy")]
        public IActionResult Privacy()
        {
            return View();
        }
        
        [Route("cookies")]
        public IActionResult Cookies()
        {
            return View();
        }

        [Route("sitemap")]
        public IActionResult Sitemap()
        {
            return View();
        }

        [Route("error/{errorCode}")]
        public IActionResult Error(string errorCode)
        {
            return View("Error", errorCode);
        }

      
    }
}
