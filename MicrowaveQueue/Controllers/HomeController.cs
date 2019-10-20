using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MicrowaveQueue.Controllers.Interfaces;
using MicrowaveQueue.Models;

namespace MicrowaveQueue.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQueueService _queueService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IQueueService queueService, ILogger<HomeController> logger)
        {
            _queueService = queueService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Executing: Action='Index' on Controller='Home' in Area=''");

            _queueService.FillMicrowaves();

            return View();
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation("Executing: Action='Privacy' on Controller='Home' in Area=''");

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogInformation("Executing: Action='Error' on Controller='Home' in Area=''");

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
