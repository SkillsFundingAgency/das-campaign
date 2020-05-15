using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contentful.Core;
using Contentful.Core.Models;
using Contentful.Core.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace SFA.DAS.Campaign.Web.Controllers
{
    [Route("industries")]

    public class IndustriesController : Controller
    {
        private readonly IContentfulClient _contentfulClient;

        public IndustriesController(IContentfulClient contentfulClient)
        {
            _contentfulClient = contentfulClient;
        }
        
        public async Task<IActionResult> Index()
        {
            var interests = await _contentfulClient.GetEntriesByType<Interest>("interest");
            
            return View(interests);
        }

        [Route("{slug}")]
        public async Task<IActionResult> Industry(string slug)
        {
            var builder = QueryBuilder<Interest>.New.FieldEquals(i => i.Slug, slug);
            var interest = (await _contentfulClient.GetEntriesByType<Interest>("interest", builder)).FirstOrDefault();

            return View(interest);
        }
        
        
        // [Route("agriculture-environment-animal-care")]
        // public IActionResult AgricultureEnvironmentAnimalCare()
        // {
        //     return View();
        // }
        // [Route("business-administration")]
        // public IActionResult BusinessAdministration()
        // {
        //     return View();
        // }
        // [Route("care-services")]
        // public IActionResult CareServices()
        // {
        //     return View();
        // }
        // [Route("catering-hospitality")]
        // public IActionResult CateringHospitality()
        // {
        //     return View();
        // }
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

    public class Interest
    {
        public string Slug { get; set; }
        public string Title { get; set; }
        public Document Summary { get; set; }
        public string ThumbnailUrl { get; set; }
        public Document Body { get; set; }
        public List<string> Includes { get; set; }
        public Asset Image { get; set; }
    }
}