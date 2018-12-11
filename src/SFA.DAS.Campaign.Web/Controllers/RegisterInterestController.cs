using System;
using System.Collections.Generic;
using System.Linq;
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
            _userDataCollection = userDataCollection ;
        }

        [HttpPost]
        public async Task<IActionResult> Index(RegisterInterestModel registerInterest)
        {
            if (ModelState.IsValid)
            {
                var userData = new UserData()
                {
                    FirstName = registerInterest.FirstName,
                    LastName = registerInterest.LastName,
                    Email = registerInterest.EmailAddress,
                    CookieId = registerInterest.ReturnUrl,
                    RouteId = registerInterest.ReturnUrl
                };

                await _userDataCollection.StoreUserData(userData);

                return Redirect(registerInterest.ReturnUrl + $"#{ModalIdConsts.RegisterThanksId}");
            }


            return RedirectToAction("Index","Home");
        }
    }
}