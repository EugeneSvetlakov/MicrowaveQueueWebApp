using MicrowaveQueue.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MicrowaveQueue.Domain.Entities
{
    [Table("Microwaves")]
    public class Microwave: BaseEntity
    {
        public int? FirstInQueue { get; set; }

        public int? SecondInQueue { get; set; }

        [DisplayName("Комната")]
        public virtual Room Room { get; set; }
    }
}
