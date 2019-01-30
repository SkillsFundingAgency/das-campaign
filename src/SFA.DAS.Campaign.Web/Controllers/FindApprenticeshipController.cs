using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SFA.DAS.Campaign.Application.Geocode;
using SFA.DAS.Campaign.Domain.Geocode;
using SFA.DAS.Campaign.Domain.Vacancies;
using SFA.DAS.Campaign.Web.Models;
using SFA.DAS.Campaign.Web.Models.Vacancy;

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

        [HttpGet("SearchResults/{postcode}/{distance}")]
        public async Task<ActionResult> SearchResults(string postcode, int distance)
        {
            var viewModel = new SearchResultsViewModel();

            var latLng = await _geocodeService.GetFromPostCode(postcode);

            var results = await _vacanciesService.GetByPostcode(postcode, Convert.ToInt32(distance));

            viewModel.Results = results.Where(w => w.DistanceInMiles <= distance).Take(10).ToList();
            viewModel.Distance = distance;
            viewModel.Postcode = postcode;
            viewModel.Location.Latitude = latLng.Coordinates.Lat;
            viewModel.Location.Longitude = latLng.Coordinates.Lon;
            viewModel.StaticMapUrl = _mappingService.GetStaticMapsUrl(results.Select(p => p.Location), "680","530");

            return View(viewModel);
        }

        [HttpGet("SearchResults/Data/{postcode}/{distance}")]
        public async Task<ActionResult> SearchResultsData(string postcode, int distance)
        {
            var viewModel = new SearchResultsViewModel();

            var latLng = await _geocodeService.GetFromPostCode(postcode);

            var results = await _vacanciesService.GetByPostcode(postcode, Convert.ToInt32(distance * 1.5));
            
            viewModel.Results = results;
            viewModel.Distance = distance;
            viewModel.Postcode = postcode;
            viewModel.Location.Latitude = latLng.Coordinates.Lat;
            viewModel.Location.Longitude = latLng.Coordinates.Lon;
            viewModel.StaticMapUrl = _mappingService.GetStaticMapsUrl(results.Select(p => p.Location), "680", "530");

            return Json(viewModel);
        }

        public async Task<IActionResult> UpdateSearch(VacancySearchViewModel viewModel)
        {
            return RedirectToAction("SearchResults",
                new {postcode = viewModel.Postcode, distance = viewModel.Distance});
        }
    }
}
