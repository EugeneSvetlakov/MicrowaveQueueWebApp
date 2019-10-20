using MicrowaveQueue.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicrowaveQueue.Models
{
    public class UserViewModel
    {
        public User User { get; set; }

        public bool IsAuthenticated { get; set; }

        public bool HasQueue { get; set; } = false;

        public bool InMicrowave { get; set; } = false;

        public OnlineQueue Queue { get; set; }

        public Microwave Microwave { get; set; }
    }
}
