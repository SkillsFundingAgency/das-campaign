using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.ViewComponents.Modal
{
    public class ModalViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string id,ModalType type,string name,object componentOptions)
        {
            var modalVM = new ModalViewModel()
            {
                Name = name,
                ComponentOptions = componentOptions == null ? null : componentOptions,
                Id = id,
                Type = type
            };


            return View("Modal", modalVM);
        }
    }
   
}
