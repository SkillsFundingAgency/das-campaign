using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Web.Helpers;

namespace SFA.DAS.Campaign.Web.Controllers.EmployerInspire
{
    public class YourApprenticeshipAdviceController : Controller
    {
        private readonly ISessionService _sessionService;

        public YourApprenticeshipAdviceController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }
    }
}