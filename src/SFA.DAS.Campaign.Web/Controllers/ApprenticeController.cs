using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;
using SFA.DAS.Campaign.Web.Models;

namespace SFA.DAS.Campaign.Web.Controllers
{
    public class ApprenticeController : Controller
    {
        private readonly IStandardsRepository _repository;

        public ApprenticeController (IStandardsRepository repository)
        {
            _repository = repository;
        }
        
        [Route("/apprentices/browse-apprenticeships")]
        public async Task<IActionResult> FindAnApprenticeship()
        {
            var routes = await _repository.GetRoutes();
            
           return View(new FindApprenticeshipSearchModel
           {
               Routes = routes
           });
        }
    }
}