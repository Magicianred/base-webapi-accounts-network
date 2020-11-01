using Magicianred.Accounts.DAL.EF.MsSql.Data;
using Magicianred.Accounts.Domain.Interfaces.Models;
using Magicianred.Accounts.Domain.Interfaces.Repositories;
using Magicianred.Accounts.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magicianred.Accounts.DAL.EF.MsSql.Repositories
{
    public class AccountRoleRepository : IAccountRoleRepository
    {
        private readonly AppDbContext _context;

        public AccountRoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IAccountRole>> GetAll()
        {
            return await _context.AccountRoles.ToListAsync();
        }

        public async Task AddAsync(int accountId, int roleId)
        {
            var arList = new List<IRole>
            {
                new Role() { Id = roleId }
            };
            var accountRoles = arList.ToArray();
            await this.AddAsync(accountId, accountRoles);
        }

        public async Task AddAsync(int accountId, IRole[] accountRoles)
        {
            var roleNames = accountRoles.Select(r => r.ToString()).ToList();
            var roles = await _context.Roles.Where(r => roleNames.Contains(r.Name)).ToListAsync();

            foreach (var role in roles)
            {
                _context.Add(new AccountRole { AccountId = accountId, RoleId = role.Id });
            }
        }

        public async Task AddAsync(IAccount account, IRole[] accountRoles)
        {
            await this.AddAsync(account.Id, accountRoles);
        }

        public async Task<IEnumerable<IAccountRole>> FindByAccountIdAsync(int accountId)
        {
            var accountRolesData = await _context.AccountRoles
                        .Where(ar => ar.AccountId == accountId)
                        .ToListAsync();

            var accountRoles = accountRolesData?.ToList<IAccountRole>();
            return accountRoles;
        }
    }
}
