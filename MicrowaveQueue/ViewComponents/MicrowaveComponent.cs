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
        private readonly IMicrowaveService _microwaveServiceservice;

        public MicrowaveComponent(IMicrowaveService microwaveServiceservice)
        {
            _microwaveServiceservice = microwaveServiceservice;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            List<MicrowaveViewModel> microwaveviews = new List<MicrowaveViewModel>();

            var microwaves = _microwaveServiceservice.GetAll();

            foreach (var item in microwaves)
            {
                microwaveviews.Add(
                    new MicrowaveViewModel()
                    {
                        Id = item.Id,
                        RoomId = item.Room.Id,
                        NowQueue = item.NowQueue,
                    });
            }

            return View(microwaveviews);
        }
    }
}
