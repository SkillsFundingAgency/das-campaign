using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Application.Geocode;
using SFA.DAS.Campaign.Domain.Geocode;
using SFA.DAS.Campaign.Domain.Vacancies;
using SFA.DAS.Campaign.Web.Constants;
using SFA.DAS.Campaign.Web.Models;

namespace SFA.DAS.Campaign.Web.Controllers
{
    [Route("FindApprenticeship")]
    public class FindApprenticeshipController : Controller
    {
        private IVacanciesService _vacanciesService;
        private IGeocodeService _geocodeService;
        private readonly IMappingService _mappingService;

        public FindApprenticeshipController(IVacanciesService vacanciesService, IGeocodeService geocodeService, IMappingService mappingService)
        {
            _vacanciesService = vacanciesService;
            _geocodeService = geocodeService;
            _mappingService = mappingService;
        }

        [HttpGet("SearchResults/{route}/{postcode}/{distance}")]
        public async Task<ActionResult> SearchResults(string route,string postcode, int distance)
        {
            SearchResultsViewModel viewModel = await GetSearchResults(route, postcode, distance);
            return View(viewModel);
        }

        [HttpGet("SearchResults/Data/{route}/{postcode}/{distance}")]
        public async Task<ActionResult> SearchResultsData(string route,string postcode, int distance)
        {
            SearchResultsViewModel viewModel = await GetSearchResults(route, postcode, distance);

            return Json(viewModel);
        }

        public async Task<IActionResult> UpdateSearch(FindApprenticeshipSearchModel viewModel)
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

            var latLng = await _geocodeService.GetFromPostCode(postcode);


            if (latLng.ResponseCode == "OK")
            {
                var routeId = Routes.GetRoute(route);

                var results = await _vacanciesService.GetByRoute(routeId, postcode, Convert.ToInt32(distance));

                viewModel.TotalResults = results.Count;
                viewModel.Results = results.Where(w => w.DistanceInMiles <= distance).Take(10).ToList();
                
                viewModel.Location.Latitude = latLng.Coordinates.Lat;
                viewModel.Location.Longitude = latLng.Coordinates.Lon;
                viewModel.StaticMapUrl = _mappingService.GetStaticMapsUrl(results.Select(p => p.Location), "680", "530");

                
            }
            return viewModel;
        }

    }
}
