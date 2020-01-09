﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SFA.DAS.Campaign.Application.DataCollection;
using SFA.DAS.Campaign.Web.Constants;
using SFA.DAS.Campaign.Web.Models;

namespace SFA.DAS.Campaign.Web.Controllers
{
    [Route("register-interest")]
    public class RegisterInterestController : Controller
    {
        private readonly IUserDataCollection _userDataCollection;

        public RegisterInterestController(IUserDataCollection userDataCollection)
        {
            _userDataCollection = userDataCollection;
        }

        [HttpGet("v1")]
        public IActionResult Index()
        {
            var url = Request.Headers["Referer"].ToString();

            if (url == string.Empty 
                || url.Contains(ControllerContext.ActionDescriptor.ControllerName,StringComparison.CurrentCultureIgnoreCase))
            {
                url = Url.Action("Index","Home");
            }
            else
            {
                var uri = new Uri(url);
                var controllerName = uri.Segments.Skip(1).Take(1).SingleOrDefault() == null ? "Home" : uri.Segments[1].Replace("/","");
                var actionName = uri.Segments.Skip(2).Take(1).SingleOrDefault() == null ? "Index" : uri.Segments[2].Replace("/","");

                url = Url.Action(actionName, controllerName);
            }

            return View("Index", new RegisterInterestModel{ReturnUrl = url, Version = 1});
        }

        [HttpGet("v2")]
        public IActionResult IndexV2()
        {
            var url = Request.Headers["Referer"].ToString();

            if (url == string.Empty
                || url.Contains(ControllerContext.ActionDescriptor.ControllerName, StringComparison.CurrentCultureIgnoreCase))
            {
                url = Url.Action("Index", "Home");
            }
            else
            {
                var uri = new Uri(url);
                var controllerName = uri.Segments.Skip(1).Take(1).SingleOrDefault() == null ? "Home" : uri.Segments[1].Replace("/", "");
                var actionName = uri.Segments.Skip(2).Take(1).SingleOrDefault() == null ? "Index" : uri.Segments[2].Replace("/", "");

                url = Url.Action(actionName, controllerName);
            }

            return View("IndexV2", new RegisterInterestModel { ReturnUrl = url, Version = 2 });
        }

        [HttpGet("downloads")]
        public IActionResult Downloads()
        {
            var json = (string)TempData["confirmationModel"];

            RegisterInterestModel model = new RegisterInterestModel();

            if (json != null)
            {
                model = JsonConvert.DeserializeObject<RegisterInterestModel>(json);
            }

            return View("EmployerDownloads", model);
        }

        [HttpPost("v1")]
        [HttpPost("v2")]
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

            if (Request.Path.Value.ToLower() == "/register-interest/v2")
            {
                TempData["confirmationModel"] = JsonConvert.SerializeObject(registerInterest);
              
                return RedirectToAction("downloads");
            }

            return Redirect($"{registerInterest.ReturnUrl}#{ModalIdConsts.RegisterThanksId}");
        }
    }
}