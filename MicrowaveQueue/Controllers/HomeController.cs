using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MicrowaveQueue.Controllers.Interfaces;
using MicrowaveQueue.Models;

namespace MicrowaveQueue.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQueueService _queueService;
        private readonly ILogger<HomeController> _logger;
        private readonly IStringLocalizer<HomeController> _localizer;

        public HomeController(IQueueService queueService, 
            ILogger<HomeController> logger, 
            IStringLocalizer<HomeController> localizer)
        {
            _queueService = queueService;
            _logger = logger;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = _localizer["IndexTitle"];

            _logger.LogInformation("Executing: Action='Index' on Controller='Home' in Area=''");

            _queueService.FillMicrowaves();

            return View();
        }

        public IActionResult Privacy()
        {
            ViewData["Title"] = _localizer["PrivacyTitle"];
            ViewData["PrivacyText"] = _localizer["PrivacyText"];


            _logger.LogInformation("Executing: Action='Privacy' on Controller='Home' in Area=''");

            return View();
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogInformation("Executing: Action='Error' on Controller='Home' in Area=''");

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
