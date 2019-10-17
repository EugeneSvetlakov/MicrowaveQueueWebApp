using MicrowaveQueue.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicrowaveQueue.Controllers.Interfaces
{
    public interface IQueueService
    {
        IEnumerable<OnlineQueue> GetAll();

        IEnumerable<OnlineQueue> GetAllByStatus(Status status = Status.Active);

        IEnumerable<OnlineQueue> GetAllByStatus(Status status, DateTime dateTime);

        OnlineQueue GetOnlineQueueByNum(int queueNum);

        void FillMicrowaves();

        IEnumerable<Microwave> GetFreeMicrowaves();

        bool IsQueueInMicrowave(int queueNum);

        bool ChangeStatus(int queueNum, Status newStatus);
        
        /// <summary>
        /// Add Queue to QueueList
        /// then run AddQueueToMw();
        /// </summary>
        /// <param name="userName">UserName</param>
        /// <returns></returns>
        OnlineQueue AddToQueue(string userName);

        /// <summary>
        /// Set Queue Status to Status.Pause for user.UserName == userName
        /// And remove QueueNum from Microwave if Exist
        /// </summary>
        /// <param name="userName">Name of user</param>
        void OnPauseQueue(int queueNum);

        /// <summary>
        /// Remove Queues from MicroWave with Day before DateTime.Now.Date
        /// and set it's Status = Status.Complit
        /// </summary>
        void ClearMwOldQueue();

        /// <summary>
        /// Replace from SecondInQueue to FirstInQueue and Free 'SecondInQueue'
        /// And set Queue Status = Complit
        /// </summary>
        void OnComplitQueue(int queueNum);

        /// <summary>
        /// Add Queue to first more free Microwave is such exist
        /// </summary>
        void AddQueueToFreeMw(int queueNum);
    }
}
