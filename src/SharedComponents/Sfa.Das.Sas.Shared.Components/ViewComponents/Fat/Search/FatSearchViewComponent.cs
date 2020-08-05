using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sfa.Das.Sas.Shared.Components.ViewModels;

namespace Sfa.Das.Sas.Shared.Components.ViewComponents.Fat
{
    public class FatSearchViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(SearchQueryViewModel searchQueryModel,string fatSearchRoute = null, string cssModifier = null, bool inline = false)
        {

            var model = new FatSearchViewModel();

            if (searchQueryModel != null)
            {
                model.Keywords = searchQueryModel.Keywords;
            }

            if (fatSearchRoute != null)
            {
                model.FatSearchRoute = fatSearchRoute;
            }

            if (!inline)
            {
                return View("Default",model);
            }
            else{
                return View("Inline", model);
            }

        }
    }
}
