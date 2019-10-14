using MicrowaveQueue.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MicrowaveQueue.Domain.Entities
{
    [Table("OnlineQueues")]
    public class OnlineQueue : BaseEntity
    {
        public bool InPause { get; set; }

        public int MicrowaveId { get; set; }

        [ForeignKey("MicrowaveId")]
        public virtual Microwave Microwave { get; set; }

        public virtual User User { get; set; }
    }
}
