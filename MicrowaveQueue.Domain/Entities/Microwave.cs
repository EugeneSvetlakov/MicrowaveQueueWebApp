using MicrowaveQueue.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MicrowaveQueue.Domain.Entities
{
    [Table("Microwaves")]
    public class Microwave : BaseEntity
    {
        public int? NowQueue { get; set; }

        [DisplayName("Room")]
        public virtual Room Room { get; set; }

        public bool IsFree
        {
            get
            {
                return !this.NowQueue.HasValue;
            }
        }
    }
}
