using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace MicrowaveQueue.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;
        private readonly IStringLocalizer<ErrorController> _localizer;

        public ErrorController(ILogger<ErrorController> logger,
            IStringLocalizer<ErrorController> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

        [Route("Error/{statuscode}")]
        public IActionResult HttpStatusCodeHandler(int statuscode)
        {
            ViewData["Title"] = _localizer["NotFoundTitle"];
            ViewData["RedirectToHomeBtn"] = _localizer["RedirectToHomeBtn"];

            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            _logger.LogWarning($"{statuscode} Error Occured.");

            switch (statuscode)
            {
                case 404:
                    _logger.LogWarning($"Error: {statuscode} On Path: {statusCodeResult.OriginalPath}" 
                        + $" and QuerryString: {statusCodeResult.OriginalQueryString}");
                    ViewData["ErrorMessage"] = _localizer["ErrorMessage404"];
                    break;
            }                

            return View("NotFound");
        }

        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            ViewData["ErrorH2"] = _localizer["ErrorH2"];
            ViewData["ErrorH3"] = _localizer["ErrorH3"];

            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            _logger.LogError($"The Path {exceptionDetails.Path} threw an exception {exceptionDetails.Error}");

            return View("Error");
        }
    }
}