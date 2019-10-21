using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MicrowaveQueue.Controllers.Interfaces;
using MicrowaveQueue.Models;

namespace MicrowaveQueue.Controllers
{
    public class MicrowaveController : Controller
    {
        private readonly IMicrowaveService _service;
        private readonly ILogger<MicrowaveController> _logger;
        private readonly IStringLocalizer<MicrowaveController> _localizer;

        public MicrowaveController(IMicrowaveService service, 
            ILogger<MicrowaveController> logger,
            IStringLocalizer<MicrowaveController> localizer)
        {
            _service = service;
            _logger = logger;
            _localizer = localizer;
        }

        public IActionResult Index(int id)
        {
            ViewData["Title"] = _localizer["MicrowaveTitle"];
            ViewData["Room"] = _localizer["Room"];
            ViewData["ServedBy"] = _localizer["ServedBy"];


            _logger.LogInformation("Executing: Action='Index' on Controller='Microwave' in Area=''");

            MicrowaveViewModel microwaveview = new MicrowaveViewModel();

            var microwave = _service.GetById(id);

            microwaveview =
                new MicrowaveViewModel()
                {
                    Id = microwave.Id,
                    RoomId = microwave.Room.Id,
                    NowQueue = microwave.NowQueue,
                };

            return View(microwaveview);
        }
    }
}