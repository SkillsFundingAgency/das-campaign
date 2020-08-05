using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sfa.Das.Sas.Shared.Components.ViewComponents.TrainingProvider.Search;

namespace Sfa.Das.Sas.Shared.Components.ViewComponents.Fat
{
    public class TrainingProviderSearchViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string apprenticeshipId,string searchRoute = null, string cssModifier = null, bool inline = false)
        {

            var model = new TrainingProviderSearchViewModel();

            if (apprenticeshipId != null)
            {
                model.ApprenticeshipId = apprenticeshipId;
            }

            if (searchRoute != null)
            {
                model.SearchRoute = searchRoute;
            }
                return View("~/Views/Shared/Components/TrainingProvider/Search/Default.cshtml",model);
        }
    }
}
