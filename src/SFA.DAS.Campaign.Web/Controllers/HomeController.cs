using System.Collections.Generic;
using System.Threading.Tasks;
using Contentful.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SFA.DAS.Campaign.Web.Configuration;
using SFA.DAS.Campaign.Web.Models.CMS;

namespace SFA.DAS.Campaign.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContentfulOptionsManager _manager;
        private readonly IContentfulClient _client;
        public HomeController(IContentfulOptionsManager manager, IContentfulClient client)
        {
            _manager = manager;
            _client = client;
        }
        [HttpGet]
        public IActionResult Index()
        {
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

        [Route("cookie-details")]
        public IActionResult CookieDetails()
        {
            return View();
        }

        [Route("sitemap")]
        public IActionResult Sitemap()
        {
            return View();
        }
        [Route("countries")]
        public IActionResult Countries()
        {
            return View("Countries");
        }
        [Route("accessibility")]
        public IActionResult Accessibility()
        {
            return View("Accessibility");
        }
        [Route("thecalling")]
        public IActionResult TheCalling()
        {
            return View("TheCalling");
        }

       
    }
}
