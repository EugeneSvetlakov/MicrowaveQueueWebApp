using MicrowaveQueue.Domain.Entities.Base;
using MicrowaveQueue.Domain.Entities.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MicrowaveQueue.Domain.Entities
{
    /// <summary>
    /// Должность работника
    /// </summary>
    [Table("Posts")]
    public class Post : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
    }
}
