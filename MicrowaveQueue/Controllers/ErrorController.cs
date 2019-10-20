using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MicrowaveQueue.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [Route("Error/{statuscode}")]
        public IActionResult HttpStatusCodeHandler(int statuscode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            _logger.LogWarning($"{statuscode} Error Occured.");

            switch (statuscode)
            {
                case 404:
                    _logger.LogWarning($"Error: {statuscode} On Path: {statusCodeResult.OriginalPath}" 
                        + $" and QuerryString: {statusCodeResult.OriginalQueryString}");
                    ViewBag.ErrorMessage = "Sorry, the resource you recuested could not be found";
                    break;
            }                

            return View("NotFound");
        }

        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            _logger.LogError($"The Path {exceptionDetails.Path} threw an exception {exceptionDetails.Error}");

            ViewBag.ExceptionPath = (exceptionDetails == null) ? "" : exceptionDetails.Path;
            ViewBag.ExceptionMessage = (exceptionDetails == null) ? "" : exceptionDetails.Error.Message;

            return View("Error");
        }
    }
}