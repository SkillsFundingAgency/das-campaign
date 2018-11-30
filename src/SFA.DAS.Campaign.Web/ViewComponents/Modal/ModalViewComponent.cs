using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Web.Models;
using SFA.DAS.Campaign.Web.Models.Components.Form;

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

    public class ModalViewModel
    {
        public string Name { get; set; }
        public object ComponentOptions { get; set; }
        public string Id { get; set; }
        public ModalType Type { get; set; }
    }
}
