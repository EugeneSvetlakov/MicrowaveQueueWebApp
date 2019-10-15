using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Text;
using MicrowaveQueue.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Threading;

namespace MicrowaveQueue.DAL
{
    public class DbInitializer
    {
        public static void Initialize(MicrowaveDbContext context)
        {
            // Проверка что база есть
            context.Database.EnsureCreated();

            // Проверка что база имеет набор Комнат
            if (!context.Rooms.Any())
            {
                var rooms = new List<Room>
                {
                    new Room
                    {
                        Id = 1,
                        Name = "Чайная1"
                        
                    },
                    new Room
                    {
                        Id = 2,
                        Name = "Чайная2"
                    }
                };

                using (var trans = context.Database.BeginTransaction())
                {
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Rooms] ON");

                    foreach (var item in rooms)
                    {
                        context.Rooms.Add(item);
                    }

                    context.SaveChanges();
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Rooms] OFF");
                    trans.Commit();
                }
            }

            // Проверка что база имеет набор Микроволновок
            if (!context.Microwaves.Any())
            {
                var microwaves = new List<Microwave>
                {
                    new Microwave
                    {
                        Id = 1,
                        Room =  context.Rooms.FirstOrDefault(r=> r.Id == 1)
                    },
                    new Microwave
                    {
                        Id = 2,
                        Room = context.Rooms.FirstOrDefault(r=> r.Id == 2)
                    },
                    new Microwave
                    {
                        Id = 3,
                        Room = context.Rooms.FirstOrDefault(r=> r.Id == 1)
                    },
                    new Microwave
                    {
                        Id = 4,
                        Room = context.Rooms.FirstOrDefault(r=> r.Id == 2)
                    }
                };

                using (var trans = context.Database.BeginTransaction())
                {
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Microwaves] ON");

                    foreach (var item in microwaves)
                    {
                        context.Microwaves.Add(item);
                    }

                    context.SaveChanges();
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Microwaves] OFF");
                    trans.Commit();
                }
            }
        }

        public static void InitializeUsers(IServiceProvider services)
        {
            var roleManager = services.GetService<RoleManager<IdentityRole>>();

            EnsureRole(roleManager, "Users");
            EnsureRole(roleManager, "Administrators");

            EnsureRoleToUser(services, "Admin", "Administrators", "admin123");
        }

        private static void EnsureRoleToUser(IServiceProvider services, string userName, string roleName, string pass)
        {
            var userManager = services.GetService<UserManager<User>>();
            var users = services.GetService<IUserStore<User>>();

            if (users.FindByNameAsync(userName, CancellationToken.None).Result != null)
            {
                return;
            }

            var adminUser = new User
            {
                UserName = userName,
            };

            if (userManager.CreateAsync(adminUser, pass).Result.Succeeded) // добавление пользователя в БД
                userManager.AddToRoleAsync(adminUser, roleName).Wait(); // назначение роли
        }

        private static void EnsureRole(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!roleManager.RoleExistsAsync(roleName).Result)
                roleManager.CreateAsync(new IdentityRole(roleName)).Wait();
        }
    }
}
