using MicrowaveQueue.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MicrowaveQueue.Domain.Entities
{
    [Table("OnlineQueues")]
    public class OnlineQueue
    {
        private DateTime? dateCreated = null;

        [Key]
        public string UserId { get; set; }

        public DateTime DateTime {
            get
            {
                return this.dateCreated.HasValue
                   ? this.dateCreated.Value
                   : DateTime.Now;
            }

            set { this.dateCreated = value; }
        }

        public int? QueueNum { get; set; }

        [DefaultValue(false)]
        public bool InPause { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
