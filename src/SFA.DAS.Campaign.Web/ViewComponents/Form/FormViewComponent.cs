using System;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Web.Models;
using SFA.DAS.Campaign.Web.Models.Components.Form;

namespace SFA.DAS.Campaign.Web.ViewComponents.Form
{
    public class FormViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(FormType type, object formViewModel, FindApprenticeshipSearchModel findApprenticeshipSearchModel)
        {
            var returnUrl = ViewContext?.HttpContext?.Request?.Path.ToString() ?? "/";
            
            switch (type)
            {
                case FormType.faaUpdateSearch:
                    return View("faa-update-results",findApprenticeshipSearchModel);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
