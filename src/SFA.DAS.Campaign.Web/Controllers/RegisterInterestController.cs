using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("index/{version}")]
        public IActionResult Index(int version = 1)
        {


            var url = Request.Headers["Referer"].ToString();

            if (url == string.Empty
                || url.Contains(ControllerContext.ActionDescriptor.ControllerName, StringComparison.CurrentCultureIgnoreCase))
            {
                url = Url.Action("Index", "Home");
            }
            else
            {
                var uri = new Uri(url);

                var pathandquery = uri.PathAndQuery;
                var controllerName = uri.Segments.Skip(1).Take(1).SingleOrDefault() == null ? "Home" : uri.Segments[1].Replace("/", "");
                var actionName = uri.Segments.Skip(2).Take(1).SingleOrDefault() == null ? "Index" : uri.Segments[2].Replace("/", "");

                var oldurl = Url.Action(actionName, controllerName) + uri.Query;

                url = uri.PathAndQuery;
            }

            return View($"IndexV{version}", new RegisterInterestModel { ReturnUrl = url, Version = version});
        }

        [HttpPost("index/{version}")]
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
                    RouteId = registerInterest.Route,
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

            if (registerInterest.Route == "2")
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