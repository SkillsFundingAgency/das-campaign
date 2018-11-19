﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;
using SFA.DAS.Campaign.Domain.DataCollection;
using SFA.DAS.Campaign.Models.DataCollection;

namespace SFA.DAS.Campaign.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStandardsService _standardsService;
        private readonly IUserDataCollection _collection;

        public HomeController(IStandardsService standardsService, IUserDataCollection collection)
        {
            _standardsService = standardsService;
            _collection = collection;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //await _collection.StoreUserData(new UserData
            //{
            //    Email = "test@test.com",
            //    FirstName = "Test",
            //    RouteId = "1",
            //    CookieId = "2",
            //    LastName = "Tester"
            //});
            //await _standardsService.GetBySearchTerm("Baker");
            return View("Index");
        }

    }
}
