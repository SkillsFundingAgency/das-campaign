using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFA.DAS.Campaign.Application.Content.Queries;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Web.Models;

namespace SFA.DAS.Campaign.Web.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;
        private readonly IMediator _mediator;

        public ErrorController(ILogger<ErrorController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        
        [Route("error/{id?}")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error(int? id = 500)
        {
            Response.StatusCode = id.Value;
            LogException();

            if (Response.StatusCode == 404)
            {
                return RedirectToAction("PageNotFound");
            }

            ViewBag.Title = $"Error {Response.StatusCode}";
            ViewBag.PageTitle = $"Error {Response.StatusCode}";
            ViewBag.MetaTitle = $"Error {Response.StatusCode}";

            var result = await _mediator.Send(new GetSiteMapQuery());

            return View("Error", result.Page);
        }

        [Route("/page-not-found")]
        public async Task<IActionResult> PageNotFound()
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            var result = await _mediator.Send(new GetSiteMapQuery());
            return View("PageNotFound", result.Page);
        }

        [Route("/error-page")]
        public IActionResult ErrorPage()
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return View("PageNotFound");
        }

        [HttpGet("/show-ip")]
        public IActionResult ShowIp()
        {
            // Extract the X-Forwarded-For IP and Remote IP Address
            var forwardedForIp = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault() ?? "N/A";
            var remoteIpAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "N/A";

            // Log both IPs for troubleshooting
            _logger.LogInformation($"X-Forwarded-For IP: {forwardedForIp}, Remote IP Address: {remoteIpAddress}");

            // Return both IPs to the view
            var model = new IpInfoModel
            {
                ForwardedForIp = forwardedForIp,
                RemoteIpAddress = remoteIpAddress
            };

            return View("ShowIp", model);
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