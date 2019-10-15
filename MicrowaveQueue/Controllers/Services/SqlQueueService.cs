using MicrowaveQueue.Controllers.Interfaces;
using MicrowaveQueue.DAL;
using MicrowaveQueue.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicrowaveQueue.Controllers.Services
{
    public class SqlQueueService : IQueueService
    {
        private readonly MicrowaveDbContext _context;

        public SqlQueueService(MicrowaveDbContext context)
        {
            _context = context;
        }

        public OnlineQueue AddToQueue(string userName)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == userName);

            var userInQueue = _context.OnlineQueues.Where(e => e.UserId == user.Id).Any();

            var hasNumOnDay =
                (_context.OnlineQueues.Where(e => e.DateTime.Date == DateTime.Now.Date).Any());

            if (!userInQueue)
            {
                using (var trans = _context.Database.BeginTransaction())
                {
                    var queue = new OnlineQueue()
                    {
                        DateTime = DateTime.Now,
                        InPause = false,
                        QueueNum = (hasNumOnDay) ?
                        _context.OnlineQueues.Where(e => e.DateTime.Date == DateTime.Now.Date).Max(n => n.QueueNum) + 1
                        : 1,
                        UserId = user.Id
                    };

                    _context.OnlineQueues.Add(queue);

                    _context.SaveChanges();
                    trans.Commit();

                    return queue;
                }
            }
            else
            {
                using (var trans = _context.Database.BeginTransaction())
                {
                    var queue = _context.OnlineQueues.FirstOrDefault(q => q.UserId == user.Id);

                    queue.DateTime = DateTime.Now;
                    queue.QueueNum = (hasNumOnDay) ?
                            _context.OnlineQueues.Where(e => e.DateTime.Date == DateTime.Now.Date).Max(n => n.QueueNum) + 1
                            : 1;
                    _context.SaveChanges();
                    trans.Commit();

                    return queue;
                }
            }
        }
    }
}
