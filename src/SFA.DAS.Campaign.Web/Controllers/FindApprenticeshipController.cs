using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Application.Vacancies.Queries.GetVacancies;
using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;
using SFA.DAS.Campaign.Domain.Interfaces;
using SFA.DAS.Campaign.Web.Helpers;
using SFA.DAS.Campaign.Web.Models;

namespace SFA.DAS.Campaign.Web.Controllers
{
    [Route("FindApprenticeship")]
    public class FindApprenticeshipController : Controller
    {
        private readonly IMappingService _mappingService;
        private readonly IStandardsRepository _repository;
        private readonly IMediator _mediator;

        public FindApprenticeshipController(IMappingService mappingService, IStandardsRepository repository, IMediator mediator)
        {
            _mappingService = mappingService;
            _repository = repository;
            _mediator = mediator;
        }

        [HttpGet("/apprentices/browse-apprenticeships/{route}/{postcode}/{distance}")]
        public async Task<ActionResult> SearchResults(string route, string postcode, int distance)
        {
            SearchResultsViewModel viewModel = await GetSearchResults(route, postcode, distance);    
            return View(viewModel);
        }

        [HttpGet("SearchResults/Data/{route}/{postcode}/{distance}")]
        public async Task<ActionResult> SearchResultsData(string route, string postcode, int distance)
        {
            SearchResultsViewModel viewModel = await GetSearchResults(route, postcode, distance);

            return Json(viewModel);
        }

        [HttpPost("/apprentices/browse-apprenticeships")]
        public async Task<IActionResult> UpdateSearch(FindApprenticeshipSearchModel viewModel)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("SearchResults",
                    new { route = viewModel.Route, postcode = viewModel.Postcode, distance = viewModel.Distance });
            }

            var staticContent = _mediator.GetModelForStaticContent();
            var routes = _repository.GetRoutes();
            await Task.WhenAll(staticContent, routes);
            
            viewModel.Routes = routes.Result;
            viewModel.Menu = staticContent.Result.Menu;
            viewModel.BannerModels = staticContent.Result.BannerModels;

            return View("~/Views/Apprentice/FindAnApprenticeship.cshtml", viewModel);
        }

        private async Task<SearchResultsViewModel> GetSearchResults(string route, string postcode, int distance)
        {
            var viewModel = new SearchResultsViewModel
            {
                Route = route, Distance = distance, Postcode = postcode
            };

            var searchTask = _mediator.Send(new GetVacanciesQuery
            {
                Distance = distance,
                Postcode = postcode,
                Route = route.Replace("-"," ")
            }); 
            var staticContent =  _mediator.GetModelForStaticContent();

            await Task.WhenAll(searchTask, staticContent);
            var searchResults = searchTask.Result;
            if (searchResults != null)
            {
                viewModel = searchResults;

                Parallel.ForEach(viewModel.Results.Where(c=>c.Location!=null),
                    vacancy =>
                    {
                        vacancy.StaticMapUrl =
                            _mappingService.GetStaticMapsUrl(vacancy.Location.Latitude, vacancy.Location.Longitude);
                    });
               
                viewModel.Menu = staticContent.Result.Menu;
                viewModel.BannerModels = staticContent.Result.BannerModels;
            }

            return viewModel;
        }

    }
}
