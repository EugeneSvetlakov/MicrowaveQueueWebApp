using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MicrowaveQueue.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicrowaveQueue.DAL
{
    public class MicrowaveDbContext : IdentityDbContext<User>
    {
        public MicrowaveDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Microwave> Microwaves { get; set; }

        public DbSet<OnlineQueue> OnlineQueues { get; set; }
    }
}
