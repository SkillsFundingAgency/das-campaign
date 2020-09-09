using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers
{
    public class IndustriesController : Controller
    {
        
        [Route("/employers/browse-by-sector/")]
        public IActionResult EmployersSectors()
        {
            return View("Sectors");
        }

        [Route("/apprentices/browse-by-interests/")]
        public IActionResult ApprenticesInterests()
        {
            return View("Interests");
        }

        [Route("/employers/browse-by-sector/{slug}")]
        public IActionResult Sector(string slug)
        {
            return View($"~/Views/Industries/Employers/{slug}.cshtml");
        }
        
        [Route("/apprentices/browse-by-interests/{slug}")]
        public IActionResult Interest(string slug)
        {
            return View($"~/Views/Industries/Apprentices/{slug}.cshtml");
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