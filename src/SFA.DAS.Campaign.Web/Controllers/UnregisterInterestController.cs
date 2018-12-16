using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Domain.DataCollection;

namespace SFA.DAS.Campaign.Web.Controllers
{
    [Route("un-register")]
    public class UnregisterInterestController : Controller
    {
        private readonly IUserDataCollection _userDataCollection;
        private readonly IUserDataCryptographyService _userDataCryptographyService;

        public UnregisterInterestController(IUserDataCollection userDataCollection, IUserDataCryptographyService userDataCryptographyService)
        {
            _userDataCollection = userDataCollection;
            _userDataCryptographyService = userDataCryptographyService;
        }

        [HttpGet]
        [Route("{email}")]
        public IActionResult Index(string email)
        {
            return View("Index",email);
        }

        [HttpGet]
        [Route("thank-you")]
        public IActionResult ThankYou()
        {
            return View();
        }

        [HttpPost]
        [Route("submit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(string encodedEmail)
        {
            var decodedEmail = _userDataCryptographyService.DecodeUserEmail(encodedEmail);

            if (!string.IsNullOrEmpty(decodedEmail))
            {
                await _userDataCollection.RemoveUserData(decodedEmail);
            }

            return RedirectToAction("ThankYou");
        }
    }
}