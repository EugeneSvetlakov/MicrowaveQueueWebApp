using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MicrowaveQueue.Controllers.Interfaces;
using MicrowaveQueue.DAL;
using MicrowaveQueue.Domain.Entities;

namespace MicrowaveQueue.Controllers
{
    public class QueueController : Controller
    {
        private readonly IQueueService _service;

        public QueueController(IQueueService service)
        {
            _service = service;
        }

        public IActionResult AddToQueue()
        {
            var userName = User.Identity.Name;

            var queue = _service.AddToQueue(userName);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult PauseQueue(int id)
        {
            var queue = _service.ChangeStatus(id, Status.Pause);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult ReturnToQueue(int id)
        {
            var queue = _service.ChangeStatus(id, Status.Active);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult ComplitQueue(int id)
        {
            var queue = _service.ChangeStatus(id, Status.Complit);

            return RedirectToAction("Index", "Home");
        }
    }
}