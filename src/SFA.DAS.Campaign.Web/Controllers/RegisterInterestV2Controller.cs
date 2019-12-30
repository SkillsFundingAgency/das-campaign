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
    [Route("register-interest-v2")]
    public class RegisterInterestV2Controller : Controller
    {
        private readonly IUserDataCollection _userDataCollection;

        public RegisterInterestV2Controller(IUserDataCollection userDataCollection)
        {
            _userDataCollection = userDataCollection;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            var url = Url.Action("downloads", "register-interest");
            
            return View("../RegisterInterest/IndexV2", new RegisterInterestModel { ReturnUrl = url, Version = 2 });
        }

        [HttpGet("downloads")]
        public IActionResult Downloads()
        {
            return View("EmployerDownloads");
        }

        [HttpPost("Index")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(RegisterInterestModel registerInterest)
        {
          
            if (!ModelState.IsValid)
            {
                return View(registerInterest);
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
            var path = Request.Path.Value.ToLower();

           
            return View("EmployerDownloads", new RegisterInterestConfirmationModel() { FirstName = registerInterest.FirstName, LastName = registerInterest.LastName, Email = registerInterest.Email });
           

        }
    }
}