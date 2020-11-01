using Magicianred.Accounts.DAL.EF.MsSql.Data;
using Magicianred.Accounts.Domain.Interfaces.Models;
using Magicianred.Accounts.Domain.Interfaces.Repositories;
using Magicianred.Accounts.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magicianred.Accounts.DAL.EF.MsSql.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;

        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IAccount>> GetAll()
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task AddAsync(IAccount account) //, IRole[] accountRoles)
        {
            _context.Accounts.Add((Account)account);
        }

        public async Task<IAccount> FindByEmailAsync(string email)
        {
            var account = await _context.Accounts.SingleOrDefaultAsync(a => a.Email == email);
            return account;
        }
    }
}