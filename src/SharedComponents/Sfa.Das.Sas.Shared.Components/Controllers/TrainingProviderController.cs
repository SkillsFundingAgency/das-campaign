using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sfa.Das.Sas.ApplicationServices.Queries;
using Sfa.Das.Sas.Shared.Components.ViewModels;
using Sfa.Das.Sas.Shared.Components.ViewModels.TrainingProvider.Search;

namespace Sfa.Das.Sas.Shared.Components.Controllers
{
    public class TrainingProviderController : Controller
    {
        private readonly IMediator _mediator;

        public TrainingProviderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("/trainingprovider/search")]
        public IActionResult Search(TrainingProviderSearchViewModel model)
        { 
            return View("TrainingProvider/SearchResults", model);
        }

        [Route("/trainingprovider/details")]
        public IActionResult Details(TrainingProviderDetailQueryViewModel model)
        {
            return View("TrainingProvider/Details", model);
        }
        
        [Route("/trainingprovider/validate-postcode")]
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> ValidatePostcode(string postcode)
        {
            var validPostcode = await _mediator.Send(new ValidatePostcodeQuery() {Postcode = postcode});
            return Json(validPostcode);
        }
    }
    
}
