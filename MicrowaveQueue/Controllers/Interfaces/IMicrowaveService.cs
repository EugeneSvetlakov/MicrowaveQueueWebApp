using MicrowaveQueue.Domain.Entities;
using MicrowaveQueue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicrowaveQueue.Controllers.Interfaces
{
    public interface IMicrowaveService
    {
        IEnumerable<Microwave> GetAll();

        Microwave GetById(int id);

        MicrowaveViewModel TransformMicrowave();
    }
}
