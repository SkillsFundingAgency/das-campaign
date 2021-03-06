﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Application.Geocode;
using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;
using SFA.DAS.Campaign.Domain.Vacancies;
using SFA.DAS.Campaign.Web.Models;

namespace SFA.DAS.Campaign.Web.Controllers
{
    [Route("FindApprenticeship")]
    public class FindApprenticeshipController : Controller
    {
        private readonly IVacanciesRepository _vacanciesService;
        private readonly IMappingService _mappingService;
        private readonly IStandardsRepository _repository;

        public FindApprenticeshipController(IVacanciesRepository vacanciesService, IMappingService mappingService, IStandardsRepository repository)
        {
            _vacanciesService = vacanciesService;
            _mappingService = mappingService;
            _repository = repository;
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

            viewModel.Routes = await _repository.GetRoutes();
            return View("~/Views/Apprentice/FindAnApprenticeship.cshtml", viewModel);
        }

        private async Task<SearchResultsViewModel> GetSearchResults(string route, string postcode, int distance)
        {
            var viewModel = new SearchResultsViewModel();

            viewModel.Route = route;
            viewModel.Distance = distance;
            viewModel.Postcode = postcode;

            var routeId = route.Replace("-"," ");

            var routes = _repository.GetRoutes();
            var searchTask = _vacanciesService.GetByRoute(routeId, postcode, Convert.ToInt32(distance));

            await Task.WhenAll(routes, searchTask);
            var searchResults = searchTask.Result;
            if (searchResults != null)
            {
                viewModel.TotalResults = searchResults.Results.Count;
                viewModel.Results = searchResults.Results.Where(w => w.DistanceInMiles <= distance).Take(100).ToList();

                viewModel.Location = searchResults.searchLocation;
                viewModel.StaticMapUrl = _mappingService.GetStaticMapsUrl(searchResults.Results.Select(p => p.Location), "680", "530");

                viewModel.Country = searchResults.Country;
                viewModel.Routes = routes.Result;
            }


            return viewModel;
        }

    }
}
