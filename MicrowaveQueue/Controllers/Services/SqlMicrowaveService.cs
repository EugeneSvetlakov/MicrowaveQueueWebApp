using Microsoft.EntityFrameworkCore;
using MicrowaveQueue.Controllers.Interfaces;
using MicrowaveQueue.DAL;
using MicrowaveQueue.Domain.Entities;
using MicrowaveQueue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicrowaveQueue.Controllers.Services
{
    public class SqlMicrowaveService : IMicrowaveService
    {
        private readonly MicrowaveDbContext _context;

        public SqlMicrowaveService(MicrowaveDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Microwave> GetAll()
        {
            return _context.Microwaves
                .Include(r=>r.Room)
                .ToList();
        }

        public Microwave GetById(int id)
        {
            return _context.Microwaves
                .Include(r => r.Room)
                .FirstOrDefault(m => m.Id == id);
        }
    }
}
