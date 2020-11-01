using Magicianred.Accounts.Domain.Enums;
using Magicianred.Accounts.Domain.Factories;
using Magicianred.Accounts.Domain.Interfaces.Handlers;
using Magicianred.Accounts.Domain.Interfaces.Models;
using Magicianred.Accounts.Domain.Interfaces.Repositories;
using Magicianred.Accounts.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Magicianred.Accounts.DAL.Dapper.Data
{
    public class DatabaseSeed
    {
        /// <summary>
        /// TO TEST
        /// </summary>
        /// <param name="connectionFactory"></param>
        /// <param name="accountRepository"></param>
        /// <param name="roleRepository"></param>
        /// <param name="accountRoleRepository"></param>
        /// <param name="passwordHandler"></param>
        public static void Seed(IServiceScope scope)
        {
            var services = scope.ServiceProvider;
            var connectionFactory = services.GetService<IDatabaseConnectionFactory>();
            var accountRepository = services.GetService<IAccountRepository>();
            var roleRepository = services.GetService<IRoleRepository>();
            var accountRoleRepository = services.GetService<IAccountRoleRepository>();
            var passwordHandler = services.GetService<IPasswordHandler>();


            var rolesList = new List<IRole>();
            var rolesResult = roleRepository.GetAll().Result;
            if (rolesResult != null)
            {
                rolesList = rolesResult.ToList();
            }
            if (rolesList.Count() == 0)
            {
                var roles = new List<Role>
                {
                    new Role { Name = ApplicationRoleEnum.Common.ToString() },
                    new Role { Name = ApplicationRoleEnum.Administrator.ToString() }
                };

                foreach (var role in roles)
                {
                    roleRepository.AddAsync(role).Wait();
                }
            }

            var accountList = new List<IAccount>();
            var accountsResult = accountRepository.GetAll().Result;
            if (accountsResult != null)
            {
                accountList = accountsResult.ToList();
            }
            if (accountList.Count() == 0)
            {
                var accounts = new List<Account>
                {
                    new Account { 
                        Email = "admin@admin.com", 
                        Password = passwordHandler.HashPassword("12345678"),
                        CreateDate = DateTime.Now
                    },
                    new Account { 
                        Email = "common@common.com", 
                        Password = passwordHandler.HashPassword("12345678"),
                        CreateDate = DateTime.Now
                    },
                };

                foreach (var account in accounts)
                {
                    accountRepository.AddAsync(account).Wait();
                }
            }

            var accountRolesList = new List<IAccountRole>();
            var accountRolesResult = accountRoleRepository.GetAll().Result;
            if (accountRolesResult != null)
            {
                accountRolesList = accountRolesResult.ToList();
            }
            if (accountRolesList.Count() == 0)
            {
                var accountRoles = new List<AccountRole>
                {new AccountRole
                    {
                        AccountId = accountList[0].Id,
                        RoleId = rolesList.SingleOrDefault(r => r.Name == ApplicationRoleEnum.Administrator.ToString()).Id
                    },
                    new AccountRole
                    {
                        AccountId = accountList[1].Id,
                        RoleId = rolesList.SingleOrDefault(r => r.Name == ApplicationRoleEnum.Common.ToString()).Id
                    },
                };

                foreach (var accountRole in accountRoles)
                {
                    accountRoleRepository.AddAsync(accountRole.AccountId, accountRole.RoleId).Wait();
                }
            }
        }
    }
}