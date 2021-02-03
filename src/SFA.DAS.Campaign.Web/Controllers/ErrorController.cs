using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFA.DAS.Campaign.Web.Models;

namespace DfE.EmployerFavourites.Web.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }
        
        [Route("error/{id?}")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? id = 500)
        {
            Response.StatusCode = id.Value;
            
            LogException();

            if (Response.StatusCode == 404)
            {
                return RedirectToAction("PageNotFound");
            }
            
            return View("Error", new ErrorViewModel { StatusCode = Response.StatusCode, RequestId = HttpContext.TraceIdentifier });
        }

        [Route("/page-not-found")]
        public IActionResult PageNotFound()
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return View("PageNotFound");
        }

        private void LogException()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionFeature != null)
            {
                string routeWhereExceptionOccurred = exceptionFeature.Path;
                var exception = exceptionFeature.Error;

                switch (exception)
                {
                    case AggregateException ex:
                        var flattenedExceptions = ex.Flatten();
                        _logger.LogError(flattenedExceptions, "Aggregate exception on path: {route}", routeWhereExceptionOccurred);

                        exception = flattenedExceptions.InnerExceptions.FirstOrDefault();
                        break;
                    case Exception ex:
                        Response.StatusCode = (int)HttpStatusCode.NotFound;
                        _logger.LogDebug(ex, "Entity not found");
                        return;
                    default:
                        break;
                }

                _logger.LogError(exception, "Unhandled exception on path: {route}", routeWhereExceptionOccurred);
            }
        }
    }
}