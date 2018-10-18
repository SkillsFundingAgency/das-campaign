using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;

namespace SFA.DAS.Campaign.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStandardsService _standardsService;

        public HomeController(IStandardsService standardsService)
        {
            _standardsService = standardsService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await _standardsService.GetBySearchTerm("Baker");
            return View("Index");
        }

    }
}
