using MicrowaveQueue.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicrowaveQueue.Controllers.Interfaces
{
    public interface IQueueService
    {
        OnlineQueue AddToQueue(string userName);
    }
}
