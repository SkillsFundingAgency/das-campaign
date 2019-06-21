using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sfa.Das.Sas.Shared.Components.ViewComponents.TrainingProvider.Search;

namespace SFA.DAS.Campaign.Web.ViewComponents
{
    public class TrainingProviderHeadingViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(TrainingProviderSearchViewModel queryModel)
        {
            return View("Default", queryModel);
        }
    }
}
