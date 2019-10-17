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

        /// <summary>
        /// Add Queue to first more free Microwave is such exist
        /// </summary>
        public void AddQueueToFreeMw(int queueNum)
        {
            var mw = _context.Microwaves.ToList();

            if (!_context.Microwaves.Where(m => m.IsFree == true).Any())
                return;
            if (queueNum == 0
                || !_context.OnlineQueues
                .Where(q => q.QueueNum == queueNum
                && q.DateTime.Date == DateTime.Now.Date
                && q.Status == Status.Active).Any())
                return;

            var queue = _context.OnlineQueues
                .FirstOrDefault(q => q.QueueNum == queueNum
                && q.DateTime.Date == DateTime.Now.Date);

            using (var trans = _context.Database.BeginTransaction())
            {
                Microwave microwave = _context.Microwaves.FirstOrDefault(m => m.IsFree == true);
                microwave.NowQueue = queueNum;

                _context.SaveChanges();
                trans.Commit();
            }
        }

        /// <summary>
        /// Add Queue to QueueList
        /// then run AddQueueToMw();
        /// </summary>
        /// <param name="userName">UserName</param>
        /// <returns></returns>
        public OnlineQueue AddToQueue(string userName)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == userName);

            var userInQueue = _context.OnlineQueues.Where(e => e.UserId == user.Id).Any();

            var hasNumOnDay =
                (_context.OnlineQueues.Where(e => e.DateTime.Date == DateTime.Now.Date).Any());

            OnlineQueue queue;

            if (!userInQueue)
            {
                using (var trans = _context.Database.BeginTransaction())
                {
                    queue = new OnlineQueue()
                    {
                        DateTime = DateTime.Now,
                        Status = Status.Active,
                        QueueNum = (hasNumOnDay) ?
                        _context.OnlineQueues.Where(e => e.DateTime.Date == DateTime.Now.Date).Max(n => n.QueueNum) + 1
                        : 1,
                        UserId = user.Id
                    };

                    _context.OnlineQueues.Add(queue);

                    _context.SaveChanges();
                    trans.Commit();
                }
            }
            else
            {
                queue = _context.OnlineQueues.FirstOrDefault(q => q.UserId == user.Id);

                if (queue.Status != Status.Complit && queue.DateTime.Date == DateTime.Now.Date)
                    return queue;

                using (var trans = _context.Database.BeginTransaction())
                {

                    queue.DateTime = DateTime.Now;
                    queue.Status = Status.Active;
                    queue.QueueNum = (hasNumOnDay) ?
                            _context.OnlineQueues
                            .Where(e => e.DateTime.Date == DateTime.Now.Date)
                            .Max(n => n.QueueNum) + 1
                            : 1;

                    _context.SaveChanges();
                    trans.Commit();
                }
            }

            if (queue.DateTime.Date == DateTime.Now.Date
                && queue.QueueNum != 0
                && queue.Status == Status.Active)
            {
                AddQueueToFreeMw(queue.QueueNum);
            }

            return queue;
        }

        public bool ChangeStatus(int queueNum, Status newStatus)
        {
            // No Queue
            if (!_context.OnlineQueues.Where(q => q.QueueNum == queueNum).Any())
                return false;

            // Que Status == new status -> true
            if (_context.OnlineQueues.FirstOrDefault(q => q.QueueNum == queueNum).Status == newStatus)
                return true;

            // if from "Complit" to != Complit -> return false
            if (_context.OnlineQueues
                .Where(q => q.QueueNum == queueNum
                    && q.Status == Status.Complit)
                .Any())
            {
                return false;
            }

            // Active -> Pause, Active -> Complit, Pause -> Active, Pause -> Complit
            try
            {
                using (var trans = _context.Database.BeginTransaction())
                {
                    var queue = _context.OnlineQueues.FirstOrDefault(q => q.QueueNum == queueNum);
                    queue.Status = newStatus;

                    _context.SaveChanges();
                    trans.Commit();
                }

                switch (newStatus)
                {
                    case Status.Active:
                        AddQueueToFreeMw(queueNum);
                        break;
                    case Status.Pause:
                        OnPauseQueue(queueNum);
                        break;
                    default:
                        OnComplitQueue(queueNum);
                        break;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Remove Queues from MicroWave with Day before DateTime.Now.Date
        /// and set it's Status = Status.Complit
        /// </summary>
        public void ClearMwOldQueue()
        {
            var listMwWithOldQueues = _context.Microwaves
                .Where(q =>
                    q.NowQueue == _context.OnlineQueues
                                    .FirstOrDefault(s => s.QueueNum == q.NowQueue
                                        && s.DateTime.Date < DateTime.Now.Date).QueueNum).ToList();

            foreach (var item in listMwWithOldQueues)
            {
                using (var trans = _context.Database.BeginTransaction())
                {
                    item.NowQueue = null;

                    _context.SaveChanges();
                    trans.Commit();
                }
            }
        }

        public IEnumerable<OnlineQueue> GetAll()
        {
            return _context.OnlineQueues.ToList();
        }

        public IEnumerable<OnlineQueue> GetAllByStatus(Status status = Status.Active)
        {
            return _context.OnlineQueues.Where(q => q.Status == status).ToList();
        }

        public IEnumerable<OnlineQueue> GetAllByStatus(Status status, DateTime dateTime)
        {
            return _context.OnlineQueues
                .Where(q => q.Status == status
                    && q.DateTime.Date == dateTime.Date)
                .ToList();
        }

        public OnlineQueue GetOnlineQueueByNum(int queueNum)
        {
            return _context.OnlineQueues.FirstOrDefault(q => q.QueueNum == queueNum);
        }

        /// <summary>
        /// Replace from SecondInQueue to FirstInQueue and Free 'SecondInQueue'
        /// And set Queue Status = Complit
        /// </summary>
        public void OnComplitQueue(int queueNum)
        {
            if (!_context.OnlineQueues.Where(q => q.QueueNum == queueNum).Any())
                return;
            if (!_context.OnlineQueues.Where(q => q.QueueNum == queueNum && q.Status == Status.Complit).Any())
                return;

            var microwaves = _context.Microwaves.Where(m => m.NowQueue == queueNum).ToList();
            foreach (var item in microwaves)
            {
                using (var trans = _context.Database.BeginTransaction())
                {
                    item.NowQueue = null;

                    _context.SaveChanges();
                    trans.Commit();
                }
            }

            FillMicrowaves();
        }

        /// <summary>
        /// Set Queue Status to Status.Pause for user.UserName == userName
        /// And remove QueueNum from Microwave if Exist
        /// </summary>
        /// <param name="userName">Name of user</param>
        public void OnPauseQueue(int queueNum)
        {
            if (_context.OnlineQueues.Where(q => q.Status == Status.Pause && q.QueueNum == queueNum).Any())
            {
                var microwaves = _context.Microwaves.Where(m => m.NowQueue == queueNum).ToList();
                foreach (var item in microwaves)
                {
                    using (var trans = _context.Database.BeginTransaction())
                    {
                        item.NowQueue = null;

                        _context.SaveChanges();
                        trans.Commit();
                    }
                }

                FillMicrowaves();
            }
        }

        public void FillMicrowaves()
        {
            ClearMwOldQueue();

            var activeQueue = GetAllByStatus(Status.Active, DateTime.Now.Date);
            foreach (var item in activeQueue)
            {
                if (!IsQueueInMicrowave(item.QueueNum))
                {
                    AddQueueToFreeMw(item.QueueNum);
                }

                if (GetFreeMicrowaves().ToList().Count == 0) break;
            }
        }

        public IEnumerable<Microwave> GetFreeMicrowaves()
        {
            return _context.Microwaves.Where(q => q.IsFree == true);
        }

        public bool IsQueueInMicrowave(int queueNum)
        {
            return _context.Microwaves
                .Where(q => q.NowQueue == queueNum)
                .Any();
        }
    }
}
