using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Application.Geocode;
using SFA.DAS.Campaign.Domain.Vacancies;
using SFA.DAS.Campaign.Web.Constants;
using SFA.DAS.Campaign.Web.Models;

namespace SFA.DAS.Campaign.Web.Controllers
{
    [Route("FindApprenticeship")]
    public class FindApprenticeshipController : Controller
    {
        private IVacanciesRepository _vacanciesService;
        private IGeocodeService _geocodeService;
        private readonly IMappingService _mappingService;

        public FindApprenticeshipController(IVacanciesRepository vacanciesService, IGeocodeService geocodeService, IMappingService mappingService)
        {
            _vacanciesService = vacanciesService;
            _geocodeService = geocodeService;
            _mappingService = mappingService;
        }

        [HttpGet("SearchResults/{route}/{postcode}/{distance}")]
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

        public IActionResult UpdateSearch(FindApprenticeshipSearchModel viewModel)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("SearchResults",
                    new { route = viewModel.Route, postcode = viewModel.Postcode, distance = viewModel.Distance });
            }

            return RedirectToAction("FindAnApprenticeship", "Apprentice");
        }

        private async Task<SearchResultsViewModel> GetSearchResults(string route, string postcode, int distance)
        {
            var viewModel = new SearchResultsViewModel();

            viewModel.Route = route;
            viewModel.Distance = distance;
            viewModel.Postcode = postcode;

            var routeId = Routes.GetRoute(route);

            var searchResults = await _vacanciesService.GetByRoute(routeId, postcode, Convert.ToInt32(distance));
            if (searchResults != null)
            {
                viewModel.TotalResults = searchResults.Results.Count;
                viewModel.Results = searchResults.Results.Where(w => w.DistanceInMiles <= distance).Take(10).ToList();

                viewModel.Location = searchResults.searchLocation;
                viewModel.StaticMapUrl = _mappingService.GetStaticMapsUrl(searchResults.Results.Select(p => p.Location), "680", "530");

                viewModel.Country = searchResults.Country;
            }


            return viewModel;
        }

    }
}
