using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Domain.DataCollection;
using SFA.DAS.Campaign.Web.Models;

namespace SFA.DAS.Campaign.Web.Controllers
{
    [Route("email-preferences")]
    public class EmailPreferencesController : Controller
    {
        private readonly IUserDataCollection _userDataCollection;
        private readonly IUserDataCryptographyService _userDataCryptographyService;

        public EmailPreferencesController(IUserDataCollection userDataCollection, IUserDataCryptographyService userDataCryptographyService)
        {
            _userDataCollection = userDataCollection;
            _userDataCryptographyService = userDataCryptographyService;
        }

        [HttpGet]
        [Route("{email}")]
        public IActionResult Index(string email)
        {
            return View("Index",new EmailPreferences{EncodedEmail = email, ReceiveEmails = true});
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
        public async Task<IActionResult> Submit(EmailPreferences model)
        {
            var decodedEmail = _userDataCryptographyService.DecodeUserEmail(model.EncodedEmail);

            if (!string.IsNullOrEmpty(decodedEmail))
            {
                await _userDataCollection.RemoveUserData(decodedEmail,model.ReceiveEmails);
            }

            return RedirectToAction("ThankYou");
        }
    }
}