using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Web.Models;
using SFA.DAS.Campaign.Web.Models.Components.Form;

namespace SFA.DAS.Campaign.Web.ViewComponents.Form
{
    public class FormViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(FormType type, object formViewModel)
        {
            
            var returnUrl = ViewContext?.HttpContext?.Request?.Path.ToString() ?? "/";
            
            switch (type)
            {
                case FormType.RegisterInterest:
                    return View("RegisterInterest", formViewModel ?? new RegisterInterestModel(){ReturnUrl = returnUrl});
                    break;
                case FormType.CookieSettings:
                    return View("cookieSettings");
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
       
        
        }
    }
}
