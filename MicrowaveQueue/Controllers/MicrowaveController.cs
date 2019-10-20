using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MicrowaveQueue.Controllers.Interfaces;
using MicrowaveQueue.Models;

namespace MicrowaveQueue.Controllers
{
    public class MicrowaveController : Controller
    {
        private readonly IMicrowaveService _service;
        private readonly ILogger<MicrowaveController> _logger;

        public MicrowaveController(IMicrowaveService service, ILogger<MicrowaveController> logger)
        {
            _service = service;
            _logger = logger;
        }

        public IActionResult Index(int id)
        {
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