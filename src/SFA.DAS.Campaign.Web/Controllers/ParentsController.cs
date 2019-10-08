using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Application.DataCollection;
using SFA.DAS.Campaign.Web.Models;

namespace SFA.DAS.Campaign.Web.Controllers
{
    [Route("parents")]
    public class ParentsController : Controller
    {
        [HttpGet]
        [Route("their-career")]
        public IActionResult TheirCareer(string email)
        {
            return View("TheirCareer");
        }

    }
}