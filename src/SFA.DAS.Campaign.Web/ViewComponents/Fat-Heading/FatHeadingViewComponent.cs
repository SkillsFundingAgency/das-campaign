using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.Shared.Components.Orchestrators;
using Sfa.Das.Sas.Shared.Components.ViewModels;

namespace SFA.DAS.Campaign.Web.ViewComponents
{
    public class FatHeadingViewComponent : ViewComponent
    {
        private readonly IFatOrchestrator _fatOrchestrator;

        public FatHeadingViewComponent(IFatOrchestrator fatOrchestrator)
        {
            _fatOrchestrator = fatOrchestrator;
        }

        public IViewComponentResult Invoke(SearchQueryViewModel queryModel)
        {
           var model = _fatOrchestrator.GetSearchResults(queryModel);

            return View("Default", model);
        }
    }
}
