using Magicianred.Accounts.Domain.Enums;
using Magicianred.Accounts.Domain.Interfaces.Handlers;
using Magicianred.Accounts.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Magicianred.Accounts.DAL.EF.MsSql.Data
{
    public class DatabaseSeed
    {
        public static void Seed(IServiceScope scope)
        {
            var services = scope.ServiceProvider;
            var context = services.GetService<AppDbContext>();
            var passwordHandler = services.GetService<IPasswordHandler>();

            context.Database.EnsureCreated();

            if (context.Roles.Count() == 0)
            {

                var roles = new List<Role>
                {
                new Role { Id = 1, Name = ApplicationRoleEnum.Common.ToString() },
                new Role { Id = 2, Name = ApplicationRoleEnum.Administrator.ToString() }
                };

                context.Roles.AddRange(roles);
                context.SaveChanges();
            }

            if (context.Accounts.Count() == 0)
            {
                var accounts = new List<Account>
                {
                    new Account {
                        Id = 1,
                        Email = "admin@admin.com",
                        Password = passwordHandler.HashPassword("12345678"),
                        CreateDate = DateTime.Now
                    },
                    new Account { 
                        Id = 2, 
                        Email = "common@common.com", 
                        Password = passwordHandler.HashPassword("12345678"),
                        CreateDate = DateTime.Now
                    },
                };

                accounts[0].AccountRoles = new List<AccountRole>
                {
                    new AccountRole
                    {
                        AccountId = accounts[0].Id,
                        RoleId = context.Roles.SingleOrDefault(r => r.Name == ApplicationRoleEnum.Administrator.ToString()).Id
                    }
                };

                accounts[1].AccountRoles = new List<AccountRole>
                {
                    new AccountRole
                    {
                        AccountId = accounts[1].Id,
                        RoleId = context.Roles.SingleOrDefault(r => r.Name == ApplicationRoleEnum.Common.ToString()).Id
                    }
                };

                context.Accounts.AddRange(accounts);
                context.SaveChanges();
            }
        }
    }
}