using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SFA.DAS.Campaign.Application.DataCollection;
using SFA.DAS.Campaign.Web.Constants;
using SFA.DAS.Campaign.Web.Models;

namespace SFA.DAS.Campaign.Web.Controllers
{
    [Route("register-interest")]
    public class RegisterInterestController : Controller
    {
        private readonly IUserDataCollection _userDataCollection;

        public RegisterInterestController(IUserDataCollection userDataCollection)
        {
            _userDataCollection = userDataCollection;
        }

        [HttpGet]
        [HttpGet("{route}")]
        [HttpGet("{route}/{version}")]
        public IActionResult Index(RouteType route = RouteType.None, int version = 1 )
        {
            var url = Request.Headers["Referer"].ToString();

            
            string controllerName = "Home";
            string actionName = "Index";

            if (url == string.Empty
                || url.Contains(ControllerContext.ActionDescriptor.ControllerName, StringComparison.CurrentCultureIgnoreCase))
            {
                url = Url.Action(actionName, controllerName);
            }
            else
            {
                var uri = new Uri(url);

                controllerName = uri.Segments.Skip(1).Take(1).SingleOrDefault() == null ? "Home" : uri.Segments[1].Replace("/", "");
                actionName = uri.Segments.Skip(2).Take(1).SingleOrDefault() == null ? "Index" : uri.Segments[2].Replace("/", "");

                if (uri.Segments.Length == 6)
                {
                    actionName += $"/{string.Join("", uri.Segments.Skip(3).Take(3))}";
                }

                url = HttpUtility.UrlDecode(Url.Action(actionName, controllerName)) + uri.Query;

            }
            return View($"IndexV{version}", new RegisterInterestModel(url, version, route ) );
        }

        [HttpPost]
        [HttpPost("{route}")]
        [HttpPost("{route}/{version}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index (RegisterInterestModel registerInterest)
        {
            if (!ModelState.IsValid)
            {
                return View($"IndexV{registerInterest.Version}",registerInterest);
            }
          
            try
            {
                await _userDataCollection.StoreUserData(new UserData
                {
                    FirstName = registerInterest.FirstName,
                    LastName = registerInterest.LastName,
                    Email = registerInterest.Email,
                    CookieId = !string.IsNullOrEmpty(HttpContext.Request.Cookies["_ga"]) ? HttpContext.Request.Cookies["_ga"] : "not-available", 
                    RouteId = ((int)registerInterest.Route).ToString(),
                    Consent = registerInterest.AcceptTandCs
                });
            }
            catch (ValidationException e)
            {
                foreach (var member in e.ValidationResult.MemberNames)
                {
                    ModelState.AddModelError(member.Split('|')[0], member.Split('|')[1]);
                }

                return View(registerInterest);
            }

            if (registerInterest.Route == RouteType.Employer)
            {
                return RedirectToAction("downloads", registerInterest);
            }

            return Redirect($"{registerInterest.ReturnUrl}#{ModalIdConsts.RegisterThanksId}");
        }

        [HttpGet("downloads")]
        public IActionResult Downloads(RegisterInterestModel registerInterest)
        {
            RegisterInterestModel model = new RegisterInterestModel();

            if (registerInterest.FirstName != null)
            {
                model = registerInterest;
            }

            return View("EmployerDownloads", model);
        }
    }
}