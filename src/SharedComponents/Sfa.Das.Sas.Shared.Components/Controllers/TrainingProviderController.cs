using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sfa.Das.Sas.ApplicationServices.Queries;
using Sfa.Das.Sas.Shared.Components.ViewComponents.TrainingProvider.Search;
using Sfa.Das.Sas.Shared.Components.ViewModels;

namespace Sfa.Das.Sas.Shared.Components.Controllers
{
    public class TrainingProviderController : Controller
    {
        private readonly IMediator _mediator;

        public TrainingProviderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Search(TrainingProviderSearchViewModel model)
        { 
            return View("TrainingProvider/SearchResults", model);
        }

        public IActionResult Details(TrainingProviderDetailQueryViewModel model)
        {
            return View("TrainingProvider/Details", model);
        }
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> ValidatePostcode(string postcode)
        {
            var validPostcode = await _mediator.Send(new ValidatePostcodeQuery() {Postcode = postcode});
            return Json(validPostcode);
        }
    }
    
}
