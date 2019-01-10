using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Domain.DataCollection;
using SFA.DAS.Campaign.Models.DataCollection;
using SFA.DAS.Campaign.Web.Constants;
using SFA.DAS.Campaign.Web.Models;

namespace SFA.DAS.Campaign.Web.Controllers
{
    public class RegisterInterestController : Controller
    {
        private readonly IUserDataCollection _userDataCollection;

        public RegisterInterestController(IUserDataCollection userDataCollection)
        {
            _userDataCollection = userDataCollection;
        }

        [HttpPost]
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

            return Redirect($"{registerInterest.ReturnUrl}#{ModalIdConsts.RegisterThanksId}");
            
        }
    }
}