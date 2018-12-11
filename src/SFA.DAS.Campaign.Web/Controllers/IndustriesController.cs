using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers
{
    [Route("industries")]

    public class IndustriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Route("agriculture-environment-animal-care")]
        public IActionResult AgricultureEnvironmentAnimalCare()
        {
            return View();
        }
        [Route("business-administration")]
        public IActionResult BusinessAdministration()
        {
            return View();
        }
        [Route("care-services")]
        public IActionResult CareServices()
        {
            return View();
        }
        [Route("catering-hospitality")]
        public IActionResult CateringHospitality()
        {
            return View();
        }
        [Route("construction")]
        public IActionResult Construction()
        {
            return View();
        }
        [Route("creative-design")]
        public IActionResult CreativeDesign()
        {
            return View();
        }
        [Route("digital")]
        public IActionResult Digital()
        {
            return View();
        }
        [Route("education-childcare")]
        public IActionResult EducationChildcare()
        {
            return View();
        }
        [Route("engineering-manufacturing")]
        public IActionResult EngineeringManufacturing()
        {
            return View();
        }
        [Route("hair-beauty")]
        public IActionResult HairBeauty()
        {
            return View();
        }
        [Route("health-science")]
        public IActionResult HealthScience()
        {
            return View();
        }
        [Route("legal-finance-accounting")]
        public IActionResult LegalFinanceAccounting()
        {
            return View();
        }
        [Route("protective-services")]
        public IActionResult ProtectiveServices()
        {
            return View();
        }
        [Route("sales-marketing-procurement")]
        public IActionResult SalesMarketingProcurement()
        {
            return View();
        }
        [Route("transport-logistics")]
        public IActionResult TransportLogistics()
        {
            return View();
        }

    }
}