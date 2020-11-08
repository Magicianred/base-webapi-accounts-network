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

            // add roles
            if (context.Roles.Count() == 0)
            {

                var roles = new List<Role>
                {
                new Role { Name = ApplicationRoleEnum.Common.ToString() },
                new Role { Name = ApplicationRoleEnum.Administrator.ToString() }
                };

                context.Roles.AddRange(roles);
                context.SaveChanges();
            }

            // add accounts
            if (context.Accounts.Count() == 0)
            {
                var accountAdmin = new Account
                {
                    Email = "admin@admin.com",
                    Password = passwordHandler.HashPassword("12345678"),
                    CreateDate = DateTime.Now
                };
                var accountCommon = new Account
                {
                    Email = "common@common.com",
                    Password = passwordHandler.HashPassword("12345678"),
                    CreateDate = DateTime.Now
                };
                var accounts = new List<Account>  {
                    accountAdmin, accountCommon
            };

                // add roles in accounts
                accounts[0].AccountRoles = new List<AccountRole>
                {
                    new AccountRole
                    {
                        AccountId = accountAdmin.Id,
                        RoleId = context.Roles.SingleOrDefault(r => r.Name == ApplicationRoleEnum.Administrator.ToString()).Id
                    }
                };

                accounts[1].AccountRoles = new List<AccountRole>
                {
                    new AccountRole
                    {
                        AccountId = accountCommon.Id,
                        RoleId = context.Roles.SingleOrDefault(r => r.Name == ApplicationRoleEnum.Common.ToString()).Id
                    }
                };

                context.Accounts.AddRange(accounts);
                context.SaveChanges();

                // add users
                var userCommon = new User
                {
                    Username = ApplicationRoleEnum.Common.ToString(),
                    Name = ApplicationRoleEnum.Common.ToString(),
                    Surname = ApplicationRoleEnum.Common.ToString(),
                    Description = ApplicationRoleEnum.Common.ToString(),
                    Title = ApplicationRoleEnum.Common.ToString(),
                    TypeId = (int)EntityTypesEnum.USER,
                    CreateDate = DateTime.Now
                };
                var userAdmin = new User
                {
                    Username = ApplicationRoleEnum.Administrator.ToString(),
                    Name = ApplicationRoleEnum.Administrator.ToString(),
                    Surname = ApplicationRoleEnum.Administrator.ToString(),
                    Description = ApplicationRoleEnum.Administrator.ToString(),
                    Title = ApplicationRoleEnum.Administrator.ToString(),
                    TypeId = (int)EntityTypesEnum.USER,
                    CreateDate = DateTime.Now
                };

                var users = new List<User>
                {
                    userCommon, userAdmin
                };

                context.Users.AddRange(users);
                context.SaveChanges();

                var userAccounts = new List<UserAccount>()
                {
                    new UserAccount()
                    {
                        UserId = userAdmin.Id,
                        AccountId = accountAdmin.Id
                    },
                    new UserAccount()
                    {
                        UserId = userCommon.Id,
                        AccountId = accountCommon.Id
                    }
                };
                context.UserAccounts.AddRange(userAccounts);
                context.SaveChanges();
            }
        
        }
    }
}