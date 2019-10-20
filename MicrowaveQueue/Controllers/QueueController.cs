using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MicrowaveQueue.Controllers.Interfaces;
using MicrowaveQueue.DAL;
using MicrowaveQueue.Domain.Entities;

namespace MicrowaveQueue.Controllers
{
    [Authorize]
    public class QueueController : Controller
    {
        private readonly IQueueService _service;
        private readonly ILogger<QueueController> _logger;

        public QueueController(IQueueService service, ILogger<QueueController> logger)
        {
            _service = service;
            _logger = logger;
        }

        public IActionResult AddToQueue()
        {
            var userName = User.Identity.Name;

            _logger.LogInformation($"Executing: Action='AddToQueue' on Controller='Queue' in Area='', for User='{userName}'");

            var queue = _service.AddToQueue(userName);

            _logger.LogInformation($"Redirecting To: Action='Index' on Controller='Home' in Area=''");

            return RedirectToAction("Index", "Home");
        }

        public IActionResult PauseQueue(int id)
        {
            _logger.LogInformation($"Executing: Action='PauseQueue' on Controller='Queue' in Area='', Queue Id='{id}'");

            var queue = _service.ChangeStatus(id, Status.Pause);

            _logger.LogInformation($"Redirecting From Action='PauseQueue' To: Action='Index' on Controller='Home' in Area=''");

            return RedirectToAction("Index", "Home");
        }

        public IActionResult ReturnToQueue(int id)
        {
            _logger.LogInformation($"Executing: Action='ReturnToQueue' on Controller='Queue' in Area='', Queue Id='{id}'");

            var queue = _service.ChangeStatus(id, Status.Active);

            _logger.LogInformation($"Redirecting To: Action='Index' on Controller='Home' in Area=''");

            return RedirectToAction("Index", "Home");
        }

        public IActionResult ComplitQueue(int id)
        {
            _logger.LogInformation($"Executing: Action='ComplitQueue' on Controller='Queue' in Area='', Queue Id='{id}'");

            var queue = _service.ChangeStatus(id, Status.Complit);

            _logger.LogInformation($"Redirecting To: Action='Index' on Controller='Home' in Area=''");

            return RedirectToAction("Index", "Home");
        }
    }
}