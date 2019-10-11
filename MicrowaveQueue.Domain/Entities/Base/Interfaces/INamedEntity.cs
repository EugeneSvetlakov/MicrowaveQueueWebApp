using System;
using System.Collections.Generic;
using System.Text;

namespace MicrowaveQueue.Domain.Entities.Base.Interfaces
{
    public interface INamedEntity : IBaseEntity
    {
        /// <summary>
        /// Имя, наименование
        /// </summary>
        string Name { get; set; }
    }
}
