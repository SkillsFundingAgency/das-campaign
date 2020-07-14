using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers
{
    public class ContentPublishedController : Controller
    {
        [HttpPost("/contentpublished")]
        public IActionResult Published()
        {
            string body = new StreamReader(HttpContext.Request.Body).ReadToEnd();
            
            return Ok();
        }
    }
}