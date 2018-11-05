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

        public FindApprenticeshipController(IVacanciesService vacanciesService, IGeocodeService geocodeService)
        {
            _vacanciesService = vacanciesService;
            _geocodeService = geocodeService;
        }

        [HttpGet]
        public ActionResult EnterPostcode()
        {
            var viewModel = new VacancySearchViewModel();
            return View(viewModel);
        }

        [HttpGet("SearchResults/{postcode}/{distance}")]
        public async Task<ActionResult> SearchResults(string postcode, int distance)
        {
            var viewModel = new SearchResultsViewModel();

            var latLng = await _geocodeService.GetFromPostCode(postcode);

            var results = await _vacanciesService.GetByPostcode(postcode, Convert.ToInt32(distance * 1.5));

            viewModel.Results = results.Where(w => w.DistanceInMiles <= distance).Take(20).ToList();
            viewModel.JsonResults = JsonConvert.SerializeObject(results);
            viewModel.Distance = distance;
            viewModel.Postcode = postcode;
            viewModel.Location.Latitude = latLng.Coordinates.Lat;
            viewModel.Location.Longitude = latLng.Coordinates.Lon;

            return View(viewModel);
        }

        public async Task<IActionResult> UpdateSearch(VacancySearchViewModel viewModel)
        {
            return RedirectToAction("SearchResults",
                new {postcode = viewModel.Postcode, distance = viewModel.Distance});
        }
    }
}
