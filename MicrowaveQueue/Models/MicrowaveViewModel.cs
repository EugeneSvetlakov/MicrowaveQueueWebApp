using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicrowaveQueue.Models
{
    public class MicrowaveViewModel
    {
        public int Id { get; set; }

        public int RoomId { get; set; }

        public int? NowQueue { get; set; }
    }
}
