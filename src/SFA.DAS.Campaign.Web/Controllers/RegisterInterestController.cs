using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SFA.DAS.Campaign.Application.DataCollection;
using SFA.DAS.Campaign.Web.Helpers;
using SFA.DAS.Campaign.Web.Models;

namespace SFA.DAS.Campaign.Web.Controllers
{
    [Route("register-interest")]
    public class RegisterInterestController : Controller
    {
        private readonly IUserDataCollection _userDataCollection;
        private readonly IMediator _mediator;

        public RegisterInterestController(IUserDataCollection userDataCollection, IMediator mediator)
        {
            _userDataCollection = userDataCollection;
            _mediator = mediator;
        }

        // [HttpGet]
        // [HttpGet("/employers/sign-up")]
        // public async Task<IActionResult> Index(RouteType route = RouteType.Employer, int version = 1)
        // {
        //     var url = Request.Headers["Referer"].ToString();
        //
        //     string controllerName = "Home";
        //     string actionName = "Index";
        //
        //     if (url == string.Empty
        //         || url.Contains(ControllerContext.ActionDescriptor.ControllerName, StringComparison.CurrentCultureIgnoreCase))
        //     {
        //         url = Url.Action(actionName, controllerName);
        //     }
        //     else
        //     {
        //         var uri = new Uri(url);
        //
        //         controllerName = uri.Segments.Skip(1).Take(1).SingleOrDefault() == null ? "Home" : uri.Segments[1].Replace("/", "");
        //         actionName = uri.Segments.Skip(2).Take(1).SingleOrDefault() == null ? "Index" : uri.Segments[2].Replace("/", "");
        //
        //         if (uri.Segments.Length == 6)
        //         {
        //             actionName += $"/{string.Join("", uri.Segments.Skip(3).Take(3))}";
        //         }
        //
        //         url = HttpUtility.UrlDecode(Url.Action(actionName, controllerName)) + uri.Query;
        //
        //     }
        //
        //     var staticContent = await _mediator.GetModelForStaticContent();
        //     return View("Index", new RegisterInterestModel(url, version, route, staticContent.Menu, staticContent.BannerModels));
        // }
        //
        // [HttpPost]
        // [HttpPost("/employers/sign-up")]
        // [EnableRateLimiting("fixed")]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Index(RegisterInterestModel registerInterest)
        // {
        //     var staticContent = await _mediator.GetModelForStaticContent();
        //     registerInterest.Menu = staticContent.Menu;
        //     registerInterest.BannerModels = staticContent.BannerModels;
        //
        //     if (!ModelState.IsValid)
        //     {
        //         registerInterest.ShowRouteQuestion = this.RouteData.Values.ContainsKey("route") == false;
        //
        //         return View("Index", registerInterest);
        //     }
        //
        //     try
        //     {
        //         await _userDataCollection.StoreUserData(new UserData
        //         {
        //             FirstName = registerInterest.FirstName,
        //             LastName = registerInterest.LastName,
        //             Email = registerInterest.Email,
        //             UkEmployerSize = registerInterest.SizeOfYourCompany,
        //             PrimaryIndustry = registerInterest.Industry,
        //             PrimaryLocation = registerInterest.Location,
        //             AppsgovSignUpDate = DateTime.Now,
        //             PersonOrigin = "apprenticeships.gov.uk",
        //             CookieId = !string.IsNullOrEmpty(HttpContext.Request.Cookies["_ga"]) ? HttpContext.Request.Cookies["_ga"] : "not-available",
        //             RouteId = ((int)registerInterest.Route).ToString(),
        //             Consent = true,
        //             IncludeInUR = registerInterest.IncludeInUR
        //         });
        //     }
        //     catch (ValidationException e)
        //     {
        //         foreach (var member in e.ValidationResult.MemberNames)
        //         {
        //             ModelState.AddModelError(member.Split('|')[0], member.Split('|')[1]);
        //         }
        //
        //         return View(registerInterest);
        //     }
        //
        //     return RedirectToAction("ThankYouForRegistering");
        // }

        [Route("/employers/thank-you-for-signing-up")]
        public async Task<IActionResult> ThankYouForRegistering()
        {
            var staticContent = await _mediator.GetModelForStaticContent();

            return View("ThankYouForRegistering", staticContent);
        }
    }
}