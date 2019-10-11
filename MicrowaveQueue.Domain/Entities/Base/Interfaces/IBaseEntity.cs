using System;
using System.Collections.Generic;
using System.Text;

namespace MicrowaveQueue.Domain.Entities.Base.Interfaces
{
    public interface IBaseEntity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        int Id { get; set; }
    }
}
