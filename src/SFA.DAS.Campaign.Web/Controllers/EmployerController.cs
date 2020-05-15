using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contentful.Core;
using Contentful.Core.Models;
using Contentful.Core.Search;
using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers
{
    [Route("employer")]
    public class EmployerController : Controller
    {
        private readonly IContentfulClient _contentfulClient;

        public EmployerController(IContentfulClient contentfulClient)
        {
            _contentfulClient = contentfulClient;
        }

        [Route("how-much-is-it-going-to-cost")]
        public async Task<IActionResult> HowMuchIsItGoingToCost()
        {
            var builder = QueryBuilder<InfoPage>.New.FieldEquals(i => i.Slug, "how-much-is-it-going-to-cost");
            var infoPageContent = (await _contentfulClient.GetEntriesByType<InfoPage>("infoPage", builder)).FirstOrDefault();
            
            return View(infoPageContent);
        }
        [Route("the-right-apprenticeship")]
        public IActionResult TheRightApprenticeship()
        {
            return View();
        }
        [Route("choose-training-provider")]
        public IActionResult ChooseATrainingProvider()
        {
            return View();
        }
        [Route("hire-an-apprentice")]
        public IActionResult HireAnApprentice()
        {
            return View();
        }
        [Route("preparing-and-monitoring")]
        public IActionResult PreparingAndMonitoring()
        {
            return View();
        }
        [Route("assessment-and-certification")]
        public IActionResult AssessmentAndQualification()
        {
            return View();
        }
        
        [Route("benefits")]
        public IActionResult Benefits()
        {
            return View();
        }

        [Route("find-apprenticeship-training")]
        public IActionResult FindApprenticeshipTraining()
        {
            return View();
        }
    }

    public class InfoPageSection
    {
        public string Title { get; set; }
        public Document Body { get; set; }
    }

    public class InfoPage
    {
        public string Slug { get; set; }
        public string Title { get; set; }
        public List<InfoPageSection> Sections { get; set; }
    }
}