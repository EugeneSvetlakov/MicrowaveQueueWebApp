using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MicrowaveQueue.Controllers.Interfaces;
using MicrowaveQueue.Models;

namespace MicrowaveQueue.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQueueService _queueService;

        public HomeController(IQueueService queueService)
        {
            _queueService = queueService;
        }

        public IActionResult Index()
        {
            _queueService.FillMicrowaves();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
