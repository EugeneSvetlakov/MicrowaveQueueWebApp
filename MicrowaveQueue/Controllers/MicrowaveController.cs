using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MicrowaveQueue.Controllers.Interfaces;
using MicrowaveQueue.Models;

namespace MicrowaveQueue.Controllers
{
    public class MicrowaveController : Controller
    {
        private readonly IMicrowaveService _service;

        public MicrowaveController(IMicrowaveService service)
        {
            _service = service;
        }

        public IActionResult Index(int id)
        {
            MicrowaveViewModel microwaveview = new MicrowaveViewModel();

            var microwave = _service.GetById(id);

            microwaveview =
                new MicrowaveViewModel()
                {
                    Id = microwave.Id,
                    RoomId = microwave.Room.Id,
                    FirstInQueue = microwave.FirstInQueue,
                    SecondInQueue = microwave.SecondInQueue
                };

            return View(microwaveview);
        }
    }
}