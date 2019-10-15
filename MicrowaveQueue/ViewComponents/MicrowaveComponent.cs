using Microsoft.AspNetCore.Mvc;
using MicrowaveQueue.Controllers.Interfaces;
using MicrowaveQueue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicrowaveQueue.ViewComponents
{
    public class MicrowaveComponent : ViewComponent
    {
        private readonly IMicrowaveService _service;

        public MicrowaveComponent(IMicrowaveService service)
        {
            _service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<MicrowaveViewModel> microwaveviews = new List<MicrowaveViewModel>();

            var microwaves = _service.GetAll();

            foreach (var item in microwaves)
            {
                microwaveviews.Add(
                    new MicrowaveViewModel()
                    {
                        Id = item.Id,
                        RoomId = item.Room.Id,
                        FirstInQueue = item.FirstInQueue,
                        SecondInQueue = item.SecondInQueue
                    });
            }

            return View(microwaveviews);
        }
    }
}
