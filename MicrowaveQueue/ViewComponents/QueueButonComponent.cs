using Microsoft.AspNetCore.Mvc;
using MicrowaveQueue.Controllers.Interfaces;
using MicrowaveQueue.DAL;
using MicrowaveQueue.Domain.Entities;
using MicrowaveQueue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicrowaveQueue.ViewComponents
{
    public class QueueButonComponent : ViewComponent
    {
        private readonly MicrowaveDbContext _context;

        public QueueButonComponent(MicrowaveDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            UserViewModel userViewModel = new UserViewModel();

            userViewModel.IsAuthenticated = User.Identity.IsAuthenticated;

            if (User.Identity.IsAuthenticated)
            {
                User user = _context.Users.FirstOrDefault(q => q.UserName == User.Identity.Name);
                userViewModel.User = user;

                userViewModel.HasQueue = _context.OnlineQueues
                    .Where(q => q.UserId == user.Id 
                        && q.DateTime.Date == DateTime.Now.Date 
                        && q.Status != Status.Complit)
                    .Any();

                if (userViewModel.HasQueue)
                {
                    OnlineQueue queue = _context.OnlineQueues
                        .FirstOrDefault(q => q.UserId == user.Id);
                    userViewModel.Queue = queue;
                    userViewModel.InMicrowave = _context.Microwaves
                        .Where(q => q.NowQueue == queue.QueueNum)
                        .Any();
                    if (userViewModel.InMicrowave)
                    {
                        userViewModel.Microwave = _context.Microwaves
                            .FirstOrDefault(q => q.NowQueue == queue.QueueNum);
                    }
                }
            }

            return View(userViewModel);
        }
    }
}
